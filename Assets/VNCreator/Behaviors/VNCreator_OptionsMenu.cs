using UnityEngine;
using UnityEngine.UI;
using VNCreator.VNCreator.Data;

namespace VNCreator.VNCreator.Behaviors
{
    public class VnCreatorOptionsMenu : MonoBehaviour
    {
        public Slider musicVolumeSlider;
        public Slider sfxVolumeSlider;
        public Slider readSpeedSlider;
        public Toggle instantTextToggle;
        public Button backButton;

        [Header("Menu Objects")]
        public GameObject optionsMenu;
        public GameObject mainMenu;

        private void Start()
        {
            GameOptions.InitializeOptions();

            if(musicVolumeSlider != null)
            {
                musicVolumeSlider.value = GameOptions.musicVolume;
                musicVolumeSlider.onValueChanged.AddListener(GameOptions.SetMusicVolume);
            }
            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = GameOptions.sfxVolume;
                sfxVolumeSlider.onValueChanged.AddListener(GameOptions.SetSfxVolume);
            }
            if (readSpeedSlider != null)
            {
                readSpeedSlider.value = GameOptions.readSpeed;
                readSpeedSlider.onValueChanged.AddListener(GameOptions.SetReadingSpeed);
            }
            if (instantTextToggle != null)
            {
                instantTextToggle.isOn = GameOptions.isInstantText;
                instantTextToggle.onValueChanged.AddListener(GameOptions.SetInstantText);
            }

            backButton.onClick.AddListener(Back);
        }

        private void Back()
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }
}
