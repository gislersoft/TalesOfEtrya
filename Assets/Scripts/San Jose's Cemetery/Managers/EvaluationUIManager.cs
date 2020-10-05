using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class EvaluationUIManager : MonoBehaviour
{

    #region Singleton

    public static EvaluationUIManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    [Header("Panel letters")]
    public TextMeshProUGUI leftPanel;
    public TextMeshProUGUI centerPanel;
    public TextMeshProUGUI rightPanel;

    [Header("UI texts")]
    public TextMeshProUGUI currentWordText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("Images")]
    public Image image;

    [Header("Screens")]
    public GameObject resultsScreen;
    public GameObject evaluationScreen;
    public GameObject drawingScreen;

    readonly string[] abc = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    EvaluationGameManager evaluationGameManager;
    SJCInventory inventory;

    private void Start()
    {
        inventory = SJCInventory.instance;

        EvaluationGameManager.instance.onLetterInputCallback += UpdateUI;
        EvaluationGameManager.instance.onScoreModifiedCallback += UpdateScore;
        EvaluationGameManager.instance.onSpriteChangeCallback += UpdateImage;

        EvaluationGameManager.instance.onGameEndedCallback += ShowResultsScreen;
        EvaluationGameManager.instance.onWatchingCountdownFinishedCallback += ShowDrawingUI;

        evaluationGameManager = EvaluationGameManager.instance;
    }

    //private void Update()
    //{
    //    timerText = evaluationGameManager.limitTimeDrawingWord + 
    //}

    public void ShowEvaluationUI()
    {
        evaluationScreen.SetActive(true);
        drawingScreen.SetActive(false);
        OrganizeLettersInPanels();
        UpdateScore(EvaluationGameManager.instance.GetScore());
        UpdateImage(EvaluationGameManager.instance.GetSprite());
    }

    public void ShowDrawingUI()
    {
        evaluationScreen.SetActive(false);
        drawingScreen.SetActive(true);
    }

    void OrganizeLettersInPanels()
    {
        int positionCounter = 0;
        int letterPosition = 0;
        string leftText = "";
        string centerText = "";
        string rightText = "";

        foreach(int letterFreq in inventory.lettersAcquired)
        {
            //build the string
            if (positionCounter == 0)
            {
                leftText += abc[letterPosition] + "x" + letterFreq.ToString() + "\n";
            }
            else if (positionCounter == 1)
            {
                centerText += abc[letterPosition] + "x" + letterFreq.ToString() + "\n";
            }
            else if (positionCounter == 2)
            {
                rightText += abc[letterPosition] + "x" + letterFreq.ToString() + "\n";
            }

            //increment the counter values
            positionCounter++;
            letterPosition++;

            //check for position counter value for reset to 0
            if (positionCounter > 2)
                positionCounter = 0;
        }

        leftPanel.SetText(leftText);
        centerPanel.SetText(centerText);
        rightPanel.SetText(rightText);

    }

    void UpdateImage(Sprite targetSprite)
    {
        image.sprite = targetSprite;
    }

    void UpdateUI(string currentWord)
    {
        UpdateCurrentWordText(currentWord);
        OrganizeLettersInPanels();
    }

    void UpdateCurrentWordText(string currentWord)
    {
        currentWordText.SetText(currentWord);
    }

    void UpdateScore(int score)
    {
        scoreText.SetText("Score: " + score.ToString());
    }

    public void ShowResultsScreen()
    {
        evaluationScreen.SetActive(false);
        resultsScreen.SetActive(true);

        finalScoreText.SetText("Score: " + EvaluationGameManager.instance.GetScore().ToString());
    }

}


