using System;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour {
    public static TransitionManager instance;

    [Header("Mobile Input Images")]
    public Image movementJoystickHolder;
    public Image cameraJoystickHolder;
    public Image jumpButton;
    public Image movementJoystickPad;
    public Image cameraJoystickPad;
    [Header("In game UI")]
    public GameObject inGameUI;
    [Header("Evaluation UI")]
    public GameObject evaluationUI;

    EvaluationGameManager evaluationGameManager;

    [Range(0.1f, 1f)]
    public float minSlowDownOfGame = 0.5f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        evaluationGameManager = EvaluationGameManager.instance;
        evaluationGameManager.onDrawingCountdownFinishedCallback += ExitEvaluationStage;
    }

    public void EnterEvaluationStage()
    {
        EnableEvaluationElements();
        ActivateEvaluationManager();
    }

    

    public void EnterHittingStage()
    {
        SpeedDownGame();
    }

    public void ExitHittingStage()
    {
        SpeedUpGame();
    }

    public void ExitEvaluationStage()
    {
        HideEvaluationUI();
        SpeedUpGame();
    }

    void SpeedDownGame()
    {
        Time.timeScale = minSlowDownOfGame;// Mathf.Lerp(currentTimeScale, minSlowDownOfGame, Time.deltaTime);
    }

    void SpeedUpGame()
    {
        Time.timeScale = 1f;//Mathf.Lerp(currentTimeScale, 1f, Time.deltaTime);
    }

    void EnableEvaluationElements()
    {
        movementJoystickHolder.enabled = false;
        cameraJoystickHolder.enabled = false;
        movementJoystickPad.enabled = false;
        cameraJoystickPad.enabled = false;

        jumpButton.enabled = false;

        inGameUI.SetActive(false);
        evaluationUI.SetActive(true);
    }

    void HideEvaluationUI()
    {
        movementJoystickHolder.enabled = true;
        cameraJoystickHolder.enabled = true;
        movementJoystickPad.enabled = true;
        cameraJoystickPad.enabled = true;

        jumpButton.enabled = true;

        inGameUI.SetActive(true);
        evaluationUI.SetActive(false);
    }

    void ActivateEvaluationManager()
    {
        evaluationGameManager.StartEvaluationStage();
    }
}
