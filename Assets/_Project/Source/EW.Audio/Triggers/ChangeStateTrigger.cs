using EW.Audio;
using UnityEngine;

public class ChangeStateTrigger : TriggerBase
{
    [SerializeField] private StateType _stateType;

    protected override void Exit()
    {
        AudioService.SetState(StateType.Playing);
    }

    protected override void Execute()
    {
        AudioService.SetState(_stateType);
    }
}
