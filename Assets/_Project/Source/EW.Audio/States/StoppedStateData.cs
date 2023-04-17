using UnityEngine;
using UnityEngine.Audio;

namespace EW.Audio
{
    [CreateAssetMenu(menuName = "AudioService/new Stopped State")]
    public class StoppedStateData : StateBase
    {
        [SerializeField] private float _volume;

        private const string ExposedMasterVolParam = "MasterVolume";
        public override void Modify(AudioMixer audioMixer)
        {
            audioMixer.SetFloat(ExposedMasterVolParam, Mathf.Log(_volume) * 20);

        }
    }
}
