using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace TMPro
{
    public class SettingsMenu : MonoBehaviour
    {

        public AudioMixer audioMixer;
        public TMP_Dropdown resDropdown;
        public TMP_Dropdown qualityDropdown;
        Resolution[] resolutions;

        private void Start()
        {
            InitResDropdown();
            InitQualityDropdown();
        }

        void InitResDropdown()
        {
            resolutions = Screen.resolutions;

            resDropdown.ClearOptions();

            List<string> options = new List<string>();

            string option = "";
            int currentResIndex = 0;
            int currentIndex = 0;
            foreach (Resolution res in resolutions)
            {
                option = res.width.ToString() + "x" + res.height.ToString();
                options.Add(option);
                option = "";

                if (res.width == Screen.currentResolution.width &&
                    res.height == Screen.currentResolution.height)
                {
                    currentResIndex = currentIndex;
                }
                currentIndex++;
            }

            resDropdown.AddOptions(options);
            resDropdown.value = currentResIndex;
            resDropdown.RefreshShownValue();
        }

        void InitQualityDropdown()
        {
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("Volume", volume);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void SetResolution(int resIndex)
        {
            Resolution resolution = resolutions[resIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}

