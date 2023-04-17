using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace EW.Audio
{
    [CreateAssetMenu(menuName = "AudioService/Create new SoundBank")]
    public class SoundBankData : ScriptableObject
    {
        [SerializeField] private AudioMixer _audioMixer;
        [Header("3D Audio Settings")]
        [SerializeField] private float _spatialBlend = 1f;
        [SerializeField] private float _minDistance = .2f;
        [SerializeField] private float _maxDistance = 30;
        [SerializeField] private float _dopplerLevel = 0;

        [SerializeField] private List<Sound> _sounds;
        [SerializeField] private List<State> _states;
        [SerializeField] private List<Effect> _effects;
        [SerializeField] private int _defaultPoolerCount;

        public IReadOnlyList<Sound> Sounds => _sounds;
        public IReadOnlyList<State> States => _states;
        public IReadOnlyList<Effect> Effects => _effects;

        public int DefaultPoolerCount => _defaultPoolerCount;
        public AudioMixer AudioMixer => _audioMixer;

        public float SpatialBlend => _spatialBlend;
        public float MinDistance => _minDistance; 
        public float MaxDistance => _maxDistance; 
        public float DopplerLevel => _dopplerLevel; 
    }
}
