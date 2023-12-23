using UnityEngine;

namespace VNCreator.VNCreator.Data
{
    public static class GameOptions
    {
        public static float musicVolume = 0.5f;
        public static float sfxVolume = 0.5f;
        public static float readSpeed = 0.5f;
        public static bool isInstantText;

        public static void InitializeOptions()
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
                musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            if (PlayerPrefs.HasKey("SfxVolume"))
                sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
            if (PlayerPrefs.HasKey("ReadSpeed"))
                readSpeed = PlayerPrefs.GetFloat("ReadSpeed");
            if (PlayerPrefs.HasKey("InstantText"))
                isInstantText = PlayerPrefs.GetInt("InstantText") == 1;
        }

        public static void SetMusicVolume(float index)
        {
            musicVolume = index;
            PlayerPrefs.SetFloat("MusicVolume", index);
        }

        public static void SetSfxVolume(float index)
        {
            sfxVolume = index;
            PlayerPrefs.SetFloat("SfxVolume", index);
        }

        public static void SetReadingSpeed(float index)
        {
            readSpeed = index;
            PlayerPrefs.SetFloat("ReadSpeed", index);
        }

        public static void SetInstantText(bool index)
        {
            isInstantText = index;
            PlayerPrefs.SetInt("InstantText", index ? 1 : 0);
        }
    }
}
