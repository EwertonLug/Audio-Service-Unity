using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace EW.Audio
{
    public class AudioSourcePooler : MonoBehaviour
    {

        private IObjectPool<AudioSource> _pool;

        public void Initialize(int poolerSize)
        {
            _pool = new ObjectPool<AudioSource>(Create, OnGet, OnRelease, OnDestroyPiece, defaultCapacity: poolerSize);
        }

        public AudioSource Get()
        {
            AudioSource audioSource = _pool.Get();
            return audioSource;
        }

        public void Release(AudioSource audioSource)
        {
            if (audioSource.gameObject.activeSelf)
            {
                _pool.Release(audioSource);
            }
            else
            {
                Debug.LogWarning("Trying to release an object that has already been released to the poo");
            }
        }

        private void OnDestroyPiece(AudioSource audioSource)
        {
            DestroyImmediate(audioSource.gameObject);
        }

        private void OnRelease(AudioSource audioSource)
        {
            
            audioSource.gameObject.SetActive(false);
            
        }

        private void OnGet(AudioSource audioSource)
        {
            audioSource.gameObject.SetActive(true);
        }

        private AudioSource Create()
        {

            AudioSource instance = new GameObject("AudioSourceSlot").AddComponent<AudioSource>();
            instance.transform.SetParent(transform);
            return instance;
        }

    }
}
