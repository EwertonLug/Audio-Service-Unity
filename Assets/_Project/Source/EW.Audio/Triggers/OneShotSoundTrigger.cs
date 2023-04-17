using UnityEngine;

namespace EW.Audio
{
    public class OneShotSoundTrigger : TriggerBase
    {
        [SerializeField] private SoundType _soundType;

        protected override void Exit()
        {

        }

        protected override void Execute()
        {
            AudioService.PlayOneShot(_soundType);
        }

        public void PlayAudio()
        {
            Execute();
        }

    }
}
