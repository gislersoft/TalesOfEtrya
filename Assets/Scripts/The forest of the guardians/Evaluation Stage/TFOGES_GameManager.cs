using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TFOGES_GameManager : MonoBehaviour {

    public static TFOGES_GameManager Instance;

    private void Awake()
    {
        Instance = this;
        //ApplicationSettings.SetTargetFrameRate();
    }

    [Header("Game management")]
    public bool isGameOver;
    public int coins;
    public int score;
    public TextMeshProUGUI currentWordText;
    public TextMeshProUGUI coinsText;


    [Header("Topic management")]
    public Topic topic;
    public int possibleWordMaxCount = 3;
    public List<string> possibleWords;
    public string targetWord;
    public int targetWordLength;
    public string currentWord;
    public int indexOfWord;
    public Image targetImage;

    private const string lowBar = "_";
    private const string startingText = "Spell the word!";
    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(10, 70, 100, 100), (1 / Time.deltaTime).ToString());
    //}
    void Start()
    {
        currentWordText.text = startingText;
        coins = 0;
        score = 0;
        isGameOver = false;
        if (SJCInventory.instance != null){
            coins = SJCInventory.instance.coinsAcquired;
        }
        topic.AllWordsToUpperCase();
        SelectStartingLetters();
        UpdateCoins();
    }

    public void SelectStartingLetters()
    {
        var wordsArray = topic.words.ToArray();
        
        if(possibleWordMaxCount == 1)
        {
            possibleWords = new List<string>() {
                wordsArray[Random.Range(0, wordsArray.Length)]
            };
        }
        else
        {
            possibleWords = Array_Utils<string>.RandomSample(wordsArray, possibleWordMaxCount).ToList();
        }

        indexOfWord = Random.Range(0, possibleWordMaxCount);
        targetWord = possibleWords[indexOfWord];
        targetWordLength = targetWord.Length;

        int indexOfImage = topic.words.IndexOf(targetWord);
        UpdateImage(topic.images[indexOfImage]);
    }

    public void UpdateImage(Sprite sprite)
    {
        targetImage.sprite = sprite;
        targetImage.preserveAspect = true;
    }
    public void UpdateCoins()
    {
        coinsText.text = coins.ToString();
    }

    public void AddLetterToCurrentWord(string letter)
    {
        if (isGameOver)
            return;

        currentWord += letter;

        int lowerBars = targetWordLength - currentWord.Length;

        string lowerBarsText = "";
        for (int i = 0; i < lowerBars; i++)
        {
            lowerBarsText += lowBar;
        }

        currentWordText.text = currentWord + lowerBarsText;

        if (lowerBars == 0)
        {
            CheckWord();
        }
        coins -= 5;
        UpdateCoins();
    }

    public void ChangeCurrentWord()
    {
        indexOfWord = Random.Range(0, possibleWords.Count);

        targetWord = possibleWords[indexOfWord];
        targetWordLength = targetWord.Length;
        int indexOfImage = topic.words.IndexOf(targetWord);

        UpdateImage(topic.images[indexOfImage]);
        currentWord = "";
        
        currentWordText.text = startingText;

    }

    public void RemoveTargetWord(int index)
    {
        possibleWords.RemoveAt(index);
    }

    public void CheckWord()
    {
        if(coins <= 0)
        {
            isGameOver = true;

        }

        if (IsWordCorrect())
        {

            coins += 10 * targetWordLength; //Add streak
            RemoveTargetWord(indexOfWord);

            if(possibleWords.Count == 0)
            {
                Win();
                isGameOver = true;
            }
            else
            {
                ChangeCurrentWord();
            }
        }
        else
        {
            ChangeCurrentWord();
        }
    }

    public bool IsWordCorrect()
    {
        if(currentWord == targetWord)
        {
            score += 50;
            return true;
        }

        return false;
    }

    public void Win()
    {
        TFOGUIManager.Instance.ShowWinPanel(score);
    }

    public void Lose() {
        TFOGUIManager.Instance.ShowLosePanel();
    }
}
