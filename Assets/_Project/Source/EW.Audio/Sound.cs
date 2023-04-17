using UnityEngine;

namespace EW.Audio
{
    [System.Serializable]
    public class Sound
    {
        public SoundType Type;
        [Range(0,1)]public float LocalVolume = 1f;
        public AudioClip Clip;
    }
}
