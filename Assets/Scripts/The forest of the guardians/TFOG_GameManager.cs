using UnityEngine;

public class TFOG_GameManager : MonoBehaviour {

    public bool capFPS;

    public static TFOG_GameManager Instance;
    
    public bool gameStarted;
    public bool isGameOver;

    [Header("Aradis")]
    public Animator aradisAnimator;
    private int gameStartedHash;

    private void Awake()
    {
        Instance = this;

        if (capFPS)
        {
            ApplicationSettings.SetTargetFrameRate();
        }
    }

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(10, 70, 100, 100), (1 / Time.deltaTime).ToString());
    //}

    private void Start()
    {
        gameStarted = false;  
        if(aradisAnimator == null)
        {
            aradisAnimator = TFOGPlayerManager.instance.Aradis.GetComponent<Animator>();
        }
        gameStartedHash = Animator.StringToHash("AradisGameStarted");
        isGameOver = false;
    }

    public void StartGame()
    {
        gameStarted = true;
        isGameOver = false;
        aradisAnimator.SetBool(gameStartedHash, gameStarted);
        
    }

    public void EndGame(){
        gameStarted = false;
        isGameOver = true;
        aradisAnimator.SetBool(gameStartedHash, false);
    }


}
