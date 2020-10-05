using UnityEngine;

public class TFGGameManager : MonoBehaviour {

    public static TFGGameManager instance;
    public bool capFPS;
    private void Awake()
    {
        instance = this;
        if (capFPS)
        {
            ApplicationSettings.SetTargetFrameRate();
        }
    }

    Animator playerAnimator;
    int wonTheGameStateHash;
    
    //public float maxGameTime;
    public int score;
    public bool isGameOver = false;

    TFOGUIManager uiManager;

    private void Start()
    {
        Time.timeScale = 1;

        playerAnimator = PlayerManager.instance.player.GetComponent<Animator>();
        wonTheGameStateHash = Animator.StringToHash("wonGame");
        uiManager = TFOGUIManager.Instance;
        score = 0;
        uiManager.UpdateScore(score);
        isGameOver = false;
        
    }

    public void WonTheGame()
    {
        playerAnimator.SetBool(wonTheGameStateHash, true);
        FinishRunningGame();
        PlayerManager.instance.player.GetComponent<TFOG_CharacterMovement>().StopMovement(true);
        TFOGUIManager.Instance.ShowWinPanel(score);
    }

    public void LoseTheGame()
    {
        FinishRunningGame();
        PlayerManager.instance.player.GetComponent<TFOG_CharacterMovement>().StopMovement(false);
        uiManager.ShowLosePanel();
    }

    public void FinishRunningGame()
    {
        isGameOver = true;
    }

    public void IncreaseScore(int score)
    {
        if (!isGameOver)
        {
            this.score += score;

            uiManager.UpdateScore(this.score);
        }
            
    }

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(10, 10, 100, 100), (1 / Time.deltaTime).ToString());
    //}

    
}
