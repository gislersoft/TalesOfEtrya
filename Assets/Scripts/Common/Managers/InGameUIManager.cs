using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InGameUIManager : MonoBehaviour {

	public Slider healthSlider;
	public GameObject currentHealthHolder;
    public GameObject inventory;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public TextMeshProUGUI scoreText;

    public float lastTime = 0f;

    [HideInInspector]
    protected PlayerManager playerManager;
    [HideInInspector]
    protected PlayerStats playerStats;
    [HideInInspector]
    protected bool isInInventory = false;
    [HideInInspector]
    protected bool isPaused = false;
    [HideInInspector]
    protected bool isInSettings = false;
    [HideInInspector]
    protected TextMeshProUGUI healthText;

    protected virtual void Start(){
		playerManager = PlayerManager.instance;

        GameManager.instance.onScoreModifiedCallback += UpdateScore;
        EvaluationGameManager.instance.onScoreModifiedCallback += UpdateScore;

        healthText = currentHealthHolder.GetComponent<TextMeshProUGUI>();
        playerStats = playerManager.player.GetComponent<PlayerStats>();
    }

    protected virtual void Update(){
		if(playerStats != null){
			healthSlider.value = playerStats.CurrentHealth;
		}

        healthText.SetText(healthSlider.value.ToString());

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
            inventory.SetActive(!inventory.activeSelf);
            isInInventory = !isInInventory;
            //Cursor.visible = isInInventory;
        }

    }

    public void UpdateScore(int score)
    {
        scoreText.SetText("Score: " + score.ToString());
    }

    #region Menu options

    public void OnGamePause(float _timeScale)
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        isPaused = !isPaused;
        //if (_timeScale == 1f)
        //{
        //    Cursor.visible = false;
        //}else
        //{
        //    Cursor.visible = true;
        //}

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
        //Cursor.visible = false;
        Time.timeScale = 1f;
    }
    #endregion
}


