using UnityEngine;
using VNCreator.VNCreator.Data;

namespace VNCreator.VNCreator.Behaviors
{
    [RequireComponent(typeof(AudioSource))]
    public class VnCreatorSfxSource : MonoBehaviour
    {
        private AudioSource _source;

        public static VnCreatorSfxSource instance;

        private void Awake()
        {
            instance = this;
            _source = GetComponent<AudioSource>();
            _source.playOnAwake = false;
            _source.volume = GameOptions.sfxVolume;
        }

        public void Play(AudioClip clip)
        {
            _source.clip = clip;
            _source.Play();
        }
    }
}
