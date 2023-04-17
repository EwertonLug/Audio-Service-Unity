using System;
using UnityEngine.Audio;
using UnityEngine.Scripting;

namespace EW.Audio
{
    [RequireImplementors]
    public interface IAudioService
    {
        public AudioMixer Mixer { get; }
        void SetSoundBank(SoundBankData soundBank);
        AudioHandler PlayLoopable(SoundType soundType, bool isMusic, bool is3D);
        void PlayOneShot(SoundType soundType);
        void StopLoopable(AudioHandler audioHandler, Action onStopped = null);
        void SetEffect(EffectType effectType);
        void SetState(StateType stateType);
    }
}
