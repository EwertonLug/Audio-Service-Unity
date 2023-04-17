using UnityEngine;
using UnityEngine.Audio;

namespace EW.Audio
{
    [CreateAssetMenu(menuName = "AudioService/Snapshot Effect")]
    public class SnapshotEffectData : EffectBase
    {
        [SerializeField] private float _timeToReache;
        [SerializeField] private AudioMixerSnapshot _snapshot;

        public override void Modify(AudioMixer audioMixer)
        {

            _snapshot.TransitionTo(_timeToReache);
        }
    }
}
