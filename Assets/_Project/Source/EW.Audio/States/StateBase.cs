using UnityEngine;
using UnityEngine.Audio;

namespace EW.Audio
{
    public abstract class StateBase : ScriptableObject
    {
        public abstract void Modify(AudioMixer audioMixer);
    }
}
