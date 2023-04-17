using UnityEngine;
using UnityEngine.Audio;

namespace EW.Audio
{
    public abstract class EffectBase : ScriptableObject
    {
        public abstract void Modify(AudioMixer audioMixer);
    }
}
