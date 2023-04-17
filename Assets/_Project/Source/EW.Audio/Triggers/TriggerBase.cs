using EW.Patterns;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace EW.Audio
{
    public abstract class TriggerBase : MonoBehaviour
    {
        [SerializeField] private TriggerType _triggerType;

        [SerializeField, ShowIf(nameof(_triggerType), TriggerType.OnButtonClick)]
        private Button _buttonTrigger;
        [SerializeField, ShowIf(nameof(_triggerType), TriggerType.OnTriggerColliderEnter)]
        private string _playerLayer;
        [SerializeField, ShowIf(nameof(_triggerType), TriggerType.OnTriggerColliderEnter)]
        private bool _stopOnTriggerExit;

        protected IAudioService AudioService { get; private set; }
        protected TriggerType Trigger { get; private set; }
        protected string PlayerLayer { get; private set; }

        protected abstract void Execute();
        protected abstract void Exit();

        private void Awake()
        {
            AudioService = ServiceLocator.Current.Get<IAudioService>();
            Trigger = _triggerType;
            PlayerLayer = _playerLayer;
        }

        private void Start()
        {
            
            if (_triggerType == TriggerType.OnStart)
            {
                Execute();
            }

            if (_triggerType == TriggerType.OnButtonClick)
            {
                _buttonTrigger.onClick.AddListener(OnButtonClick);
            }
        }

        private void OnButtonClick()
        {
            if (_triggerType == TriggerType.OnButtonClick)
            {
                Execute();
            }
        }

        private void OnEnable()
        {
            if (_triggerType == TriggerType.OnEnable)
            {
                Execute();
            }
        }

        private void OnDisable()
        {
            if (_triggerType == TriggerType.OnDisable)
            {
                Execute();
            }

        }

        private void OnDestroy()
        {
            if (_triggerType == TriggerType.OnDestroy)
            {
                Execute();
            }
            if (_triggerType == TriggerType.OnButtonClick)
            {
                _buttonTrigger.onClick.RemoveListener(OnButtonClick);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_triggerType == TriggerType.OnTriggerColliderEnter)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer(_playerLayer))
                {
                    Execute();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Trigger == TriggerType.OnTriggerColliderEnter && _stopOnTriggerExit)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer(PlayerLayer))
                {
                    Exit();
                }
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_triggerType == TriggerType.OnTriggerColliderEnter)
            {

                BoxCollider boxCollider = GetComponent<BoxCollider>();

                if (boxCollider != null)
                {
                    Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
                }
            }

        }
#endif

        public enum TriggerType
        {
            OnStart,
            OnEnable,
            OnDisable,
            OnDestroy,
            OnButtonClick,
            OnTriggerColliderEnter
        }
    }
}
