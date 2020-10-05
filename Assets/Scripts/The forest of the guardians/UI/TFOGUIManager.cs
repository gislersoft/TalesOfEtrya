using UnityEngine;
using TMPro;
public class TFOGUIManager : BaseUIManager {

    public static TFOGUIManager Instance;
    #region Time
    [Header("Time")]
    public TextMeshProUGUI timeText;
    #endregion

    #region Win & Lose
    [Header("Win")]
    public GameObject winPanel;
    public TextMeshProUGUI winScoreText;
    [Header("Lose")]
    public GameObject losePanel;
    public TextMeshProUGUI loseScoreText;
    #endregion

    [Header("Animation")]
    public Animator animator;
    bool animationPlayed;
    int showWinHash;
    int showLoseHash;
    int hideWinHash;
    int hideLoseHash;

    private new void Start()
    {
        if(pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);

        if(settingsMenuPanel != null)
            settingsMenuPanel.SetActive(false);

        if(winPanel != null)
            winPanel.SetActive(false);

        if(losePanel != null)
            losePanel.SetActive(false);

        showWinHash = Animator.StringToHash("Show Win");
        showLoseHash = Animator.StringToHash("Show Lose");
        hideWinHash = Animator.StringToHash("Hide win");
        hideLoseHash = Animator.StringToHash("Hide lose");
        animationPlayed = false;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateTime(float currentTime)
    {
        timeText.text = currentTime.ToString();
    }

    public void ShowWinPanel(int score)
    {
        if(animationPlayed == false)
        {
			animationPlayed = true;

			if (UI_SoundManager.Instance != null) {
				UI_SoundManager.Instance.WindowAppear ();
			}

            winPanel.SetActive(true);
            animator.SetTrigger(showWinHash);
            winScoreText.text = score.ToString();
        }
        
    }

    public void ShowLosePanel()
    {
        if(animationPlayed == false)
        {

			animationPlayed = true;

			if (UI_SoundManager.Instance != null) {
				UI_SoundManager.Instance.WindowAppear ();
			}
            losePanel.SetActive(true);
            animator.SetTrigger(showLoseHash);
            loseScoreText.text = "0";
        }
    }

    public void HideWinPanel()
    {
        animator.SetTrigger(hideWinHash);
        if (UI_SoundManager.Instance != null)
        {
            UI_SoundManager.Instance.WindowHide();
        }
    }

    public void HideLosePanel()
    {
        animator.SetTrigger(hideLoseHash);
        if (UI_SoundManager.Instance != null)
        {
            UI_SoundManager.Instance.WindowHide();
        }
    }
}
