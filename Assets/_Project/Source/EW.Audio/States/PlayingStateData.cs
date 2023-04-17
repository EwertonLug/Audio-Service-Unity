using UnityEngine;
using UnityEngine.Audio;

namespace EW.Audio
{
    [CreateAssetMenu(menuName = "AudioService/new Playing State")]
    public class PlayingStateData : StateBase
    {
        [SerializeField] private float _volume;

        private const string ExposedMasterVolParam = "MasterVolume";
        public override void Modify(AudioMixer audioMixer)
        {
            audioMixer.SetFloat(ExposedMasterVolParam, Mathf.Log(_volume) * 20);

        }
    }
}
