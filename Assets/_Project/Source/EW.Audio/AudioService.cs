using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using EW.Patterns;

namespace EW.Audio
{
    public class AudioService : MonoBehaviour, IAudioService 
    {
        private AudioSourcePooler _audioPooler;
        private Dictionary<SoundType, Sound> _audioClips = new Dictionary<SoundType, Sound>();
        private Dictionary<StateType, StateBase> _states = new Dictionary<StateType, StateBase>();
        private Dictionary<EffectType, EffectBase> _effects = new Dictionary<EffectType, EffectBase>();

        private AudioSource _reservedMusicAudioSource;
        private List<AudioSource> _playingSfxLoopable = new List<AudioSource>();
        private AudioMixer _mixer;
        private SoundBankData _currentSoundBank;
        private const string AudioMixerGroupSfx = "Sfx";
        private const string AudioMixerGroupMusic = "Music";

        public AudioMixer Mixer => _mixer;

        public void SetEffect(EffectType effectType)
        {
            if (_effects.TryGetValue(effectType, out EffectBase effect))
            {
                effect.Modify(_mixer);
            }
        }
        public void SetState(StateType stateType)
        {
            if (_states.TryGetValue(stateType, out StateBase state))
            {
                state.Modify(_mixer);
            }

        }

        public async void PlayOneShot(SoundType soundType)
        {
            AudioSource avaliableAudioSource = _audioPooler.Get();

            if (_audioClips.TryGetValue(soundType, out Sound sound))
            {

                avaliableAudioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups(AudioMixerGroupSfx)[0];
                avaliableAudioSource.loop = false;
                avaliableAudioSource.clip = sound.Clip;
                avaliableAudioSource.volume = sound.LocalVolume;
                avaliableAudioSource.spatialBlend = 0f;
                avaliableAudioSource.Play();
                await Task.Delay(TimeSpan.FromSeconds(sound.Clip.length + .1f));
                avaliableAudioSource.Stop();
                _audioPooler.Release(avaliableAudioSource);
            }
            else
            {
                throw new NullReferenceException($"The sound: {soundType}, not found");
            }

        }

        public AudioHandler PlayLoopable(SoundType soundType, bool isMusic, bool is3D)
        {
            AudioSource avaliableAudioSource = null;

            if (_audioClips.TryGetValue(soundType, out Sound sound))
            {
                if (isMusic)
                {

                    if (_reservedMusicAudioSource != null && _reservedMusicAudioSource.isPlaying)
                    {
                        avaliableAudioSource = _reservedMusicAudioSource;
                    }
                    else
                    {
                        avaliableAudioSource = _audioPooler.Get();
                        _reservedMusicAudioSource = avaliableAudioSource;
                    }

                    _reservedMusicAudioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups(AudioMixerGroupMusic)[0];
                    _reservedMusicAudioSource.clip = sound.Clip;

                    DOVirtual.Float(_reservedMusicAudioSource.volume, 0, .5f, v => _reservedMusicAudioSource.volume = v).onComplete += () =>
                    {
                        if (!_reservedMusicAudioSource.isPlaying)
                        {
                            _reservedMusicAudioSource.Play();
                        }
                        DOVirtual.Float(_reservedMusicAudioSource.volume, sound.LocalVolume, .5f, v => _reservedMusicAudioSource.volume = v);
                    };

                }
                else
                {
                   
                    avaliableAudioSource = _audioPooler.Get();
                    avaliableAudioSource.clip = sound.Clip;
                    avaliableAudioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups(AudioMixerGroupSfx)[0];
                    _playingSfxLoopable.Add(avaliableAudioSource);
                    avaliableAudioSource.Play();
                }

                if (is3D)
                {
                    avaliableAudioSource.spatialBlend = _currentSoundBank.SpatialBlend;
                    avaliableAudioSource.minDistance = _currentSoundBank.MinDistance;
                    avaliableAudioSource.maxDistance = _currentSoundBank.MaxDistance;
                    avaliableAudioSource.dopplerLevel = _currentSoundBank.DopplerLevel;
                }
                else
                {
                    avaliableAudioSource.spatialBlend = 0f;

                }

                avaliableAudioSource.loop = true;
                avaliableAudioSource.volume = sound.LocalVolume;
                AudioHandler audioHandler;

                audioHandler.InstanceId = avaliableAudioSource.GetInstanceID();
                audioHandler.Transform = avaliableAudioSource.transform;

                return audioHandler;
            }
            else
            {
                throw new NullReferenceException($"The sound: {soundType}, not found");
            }
        }

        public void SetSoundBank(SoundBankData soundBank)
        {
            _currentSoundBank = soundBank;
            _audioClips.Clear();
            _states.Clear();
            _effects.Clear();
            foreach (Sound sound in soundBank.Sounds)
            {
                _audioClips[sound.Type] = sound;
            }

            foreach (State state in soundBank.States)
            {
                _states[state.Type] = state.StateData;
            }
            foreach (Effect effect in soundBank.Effects)
            {
                _effects[effect.Type] = effect.EffectData;
            }

            GameObject audioPooler = new GameObject("AudioPooler");
            _mixer = soundBank.AudioMixer;
            _audioPooler = audioPooler.AddComponent<AudioSourcePooler>();
            _audioPooler.transform.SetParent(transform);
            _audioPooler.Initialize(soundBank.DefaultPoolerCount);
        }



        public void StopLoopable(AudioHandler audioHandler, Action onStopped = null)
        {
            AudioSource audioSource = _playingSfxLoopable.Find(x => x.GetInstanceID() == audioHandler.InstanceId);

            if (_reservedMusicAudioSource != null)
            {
                audioSource = _reservedMusicAudioSource.GetInstanceID() == audioHandler.InstanceId ? _reservedMusicAudioSource : audioSource;
            }

            if (audioSource != null)
            {
                audioSource.Stop();
                _audioPooler.Release(audioSource);
                onStopped?.Invoke();
                audioHandler.Transform = null;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Create()
        {

            GameObject audioService = new GameObject();
            audioService.name = nameof(AudioService);
            AudioService audioServiceComponent = audioService.AddComponent<AudioService>();
            ServiceLocator.Current.Register<IAudioService>(audioServiceComponent);
            DontDestroyOnLoad(audioService);
        }

    }
}
