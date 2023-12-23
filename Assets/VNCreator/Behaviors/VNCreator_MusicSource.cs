using UnityEngine;
using VNCreator.VNCreator.Data;

namespace VNCreator.VNCreator.Behaviors
{
    [RequireComponent(typeof(AudioSource))]
    public class VnCreatorMusicSource : MonoBehaviour
    {
        private AudioSource _source;

        public static VnCreatorMusicSource instance;

        private void Awake()
        {
            instance = this;
            _source = GetComponent<AudioSource>();
            _source.playOnAwake = false;
            _source.loop = true;
            _source.volume = GameOptions.musicVolume;
        }

        public void Play(AudioClip clip)
        {
            _source.clip = clip;
            _source.Play();
        }
    }
}
