using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class BaseUIManager : MonoBehaviour {

    [Header("Score")]
    public TextMeshProUGUI scoreText;

    [Header("Pause menu")]
    public GameObject pauseMenuPanel;

    [Header("Health bar")]
    public Image healthBar;

    [Header("Setting menu")]
    public GameObject settingsMenuPanel;

    [Header("Volume menu")]
    public AudioMixer audioMixer;

    public void Start()
    {
        if(pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);

        if(settingsMenuPanel != null)
            settingsMenuPanel.SetActive(false);

        if(scoreText != null)
            scoreText.text = "0";

        if(healthBar != null)
            healthBar.fillAmount = 1f;
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    #region Pause menu
    public void OpenPauseMenu()
    {
        if (settingsMenuPanel.activeSelf)
        {
            settingsMenuPanel.SetActive(false);
        }

        pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);

        if (pauseMenuPanel.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ClosePauseMenu()
    {
        pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
        Time.timeScale = 1;
    }
    #endregion

    #region Score
    public void UpdateScore(int value)
    {
        scoreText.text = value.ToString();
    }
    #endregion

    #region Health bar
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = (currentHealth / maxHealth);
    }
    #endregion

    #region Settings menu
    public void OpenSettingsMenu()
    {
        if (pauseMenuPanel.activeSelf)
        {
            pauseMenuPanel.SetActive(false);
        }

        settingsMenuPanel.SetActive(!settingsMenuPanel.activeSelf);

        if (settingsMenuPanel.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void CloseSettingsMenu()
    {
        settingsMenuPanel.SetActive(!settingsMenuPanel.activeSelf);
        Time.timeScale = 1;
    }
    #endregion

    #region Volume menu
    public void ChangeVolume(float value)
    {
        audioMixer.SetFloat("Volume", value);
    }
    #endregion
}
