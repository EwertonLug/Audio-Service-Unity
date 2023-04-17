using EW.Audio;
using NaughtyAttributes;
using UnityEngine;

public class LoopableSoundTrigger : TriggerBase
{
    [SerializeField] private SoundType _musicType;
    [SerializeField] private bool _isMusic;
    [SerializeField] private bool _is3D;

    [SerializeField, HideIf(nameof(_isMusic))] 

    private bool _stopOnDisable;
    private AudioHandler _handler;
    protected override void Execute()
    {
        if (_is3D)
        {
            _handler = AudioService.PlayLoopable(_musicType, _isMusic, _is3D);

        }
        else
        {
          
           _handler = AudioService.PlayLoopable(_musicType, _isMusic, _is3D);

        }
    }

    private void Update()
    {
        if(_handler.Transform != null)
        {
            if (_is3D)
            {
                _handler.Transform.position = transform.position;
            }

        }
    }

    private void OnDisable()
    {
        if (_stopOnDisable && !_isMusic)
        {
            AudioService.StopLoopable(_handler);
        }
    }

    protected override void Exit()
    {
        if (!_isMusic)
        {
            AudioService.StopLoopable(_handler);
        }
    }
}
