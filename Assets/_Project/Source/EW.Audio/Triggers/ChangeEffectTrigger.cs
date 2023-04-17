using UnityEngine;

namespace EW.Audio
{
    public class ChangeEffectTrigger : TriggerBase
    {
        [SerializeField] private EffectType _effectType;

        protected override void Exit()
        {
            AudioService.SetEffect(EffectType.None);
        }

        protected override void Execute()
        {
            AudioService.SetEffect(_effectType);
        }
    }
}
