using System;
using UnityEngine;
using UnityEngine.UI;


namespace TMPro
{
    public class InGameUIManagerTimerClue : MonoBehaviour
    {
        #region Singleton
        public static InGameUIManagerTimerClue instance;

        private void Awake()
        {
            instance = this;
        }

        #endregion

        public Image healthSlider;
        public GameObject currentHealthHolder;
        public GameObject pauseMenu;
        public GameObject settingsMenu;
        public GameObject score;
        public GameObject timer;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI timerText;

        PlayerManager playerManager;
        PlayerStats playerStats;
        bool isInInventory = false;
        bool isPaused = false;
        bool isInSettings = false;

        private static InGameUIManagerTimerClue inGameUIManagerTimerClue;

        void Start()
        {
            Application.targetFrameRate = 30;
            playerManager = PlayerManager.instance;
            playerStats = PlayerStats.instance;

            FinderController.instance.onScoreModifiedCallback += UpdateScore;
            FinderController.instance.onTimerModifiedCallback += UpdateTimer;
            FinderController.instance.onClueModifiedCallback += UpdateClue;

            //WeaponManager.instance.onWeaponUsedCallback += UpdateAmmo;

            score.SetActive(false);
            timer.SetActive( false );
            //clueTextPanel.SetActive(false);
        }

        public static InGameUIManagerTimerClue Instance()
        {
            if (!inGameUIManagerTimerClue)
            {
                inGameUIManagerTimerClue = FindObjectOfType(typeof(InGameUIManagerTimerClue)) as InGameUIManagerTimerClue;
                if (!inGameUIManagerTimerClue)
                    Debug.LogError("There needs to be one active DisplayManager script on a GameObject in your scene.");
            }

            return inGameUIManagerTimerClue;
        }

        void Update()
        {

            //currentHealthHolder.GetComponent<TextMeshProUGUI>().SetText(healthSlider.fillAmount.ToString());

            if (Input.GetKeyDown(KeyCode.Escape) && !isInInventory)
            {
                if (isPaused)
                {
                    OnGamePause(1f);
                    if (isInSettings)
                    {
                        CloseAllMenus();
                    }
                }
                else
                {
                    OnGamePause(0f);
                }

            }

            if (Input.GetKeyDown(KeyCode.I) && !isPaused)
            {
                //inventory.SetActive(!inventory.activeSelf);
                isInInventory = !isInInventory;
                Cursor.visible = isInInventory;
            }

        }

        void UpdateScore(int score)
        {
            scoreText.SetText(score.ToString());
        }

        void UpdateAmmo(int currentAmmo, int totalAmmo)
        {
            //currentAmmoText.SetText(currentAmmo.ToString());
            //totalAmmoText.SetText(totalAmmo.ToString());
        }

        void UpdateTimer(double timer)
        {
            int myBlubb = (int)timer;
            timerText.SetText(myBlubb.ToString());
        }

        void UpdateClue(string clue)
        {
            //clueText.SetText(clue);
        }

        public void StarClueSearching()
        {
            score.SetActive(true);
            timer.SetActive(true);
            //clueTextPanel.SetActive(true);
            FinderController.instance.StartTimeTrial();
            
        }

        public void StopClueSearching()
        {
            score.SetActive(false);
            timer.SetActive(false);
            //clueTextPanel.SetActive(false);
            FinderController.instance.StopTimeTrial();
        }

        public void ShowMap()
        {
            //backPackPanel.SetActive(true);
        }

        public void UpdateHealth(float currentHealth, float maxHealth) {
            //Debug.Log( currentHealth + " " + maxHealth + " " + (currentHealth / maxHealth ));
            healthSlider.fillAmount = (currentHealth / maxHealth);
        }

        #region Menu options

        void OnGamePause(float _timeScale)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            isPaused = !isPaused;
            if (_timeScale == 1f)
            {
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
            }

            Time.timeScale = _timeScale;
        }

        public void ResumeGame()
        {
            OnGamePause(1f);
        }

        public void OpenSettingsMenu()
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(true);
            isInSettings = true;
        }

        public void CloseSettingsMenu()
        {
            pauseMenu.SetActive(true);
            settingsMenu.SetActive(false);
            isInSettings = false;
        }

        public void QuitGame()
        {
            //This method should return to the main game.
            //For now, it will close the application

            Time.timeScale = 1f;
            Application.Quit();
        }

        public void CloseAllMenus()
        {
            settingsMenu.SetActive(false);
            isInSettings = false;
            pauseMenu.SetActive(false);
            isPaused = false;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        #endregion
    }
}


