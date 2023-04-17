using EW.Audio;
using UnityEngine;

public class ChangeSoundBankTrigger : TriggerBase
{
    [SerializeField] private SoundBankData _soundBankData;

    protected override void Exit()
    {
       
    }

    protected override void Execute()
    {
        AudioService.SetSoundBank(_soundBankData);
    }

}
