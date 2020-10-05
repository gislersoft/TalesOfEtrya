using UnityEngine;

public class EvaluationGameManager : MonoBehaviour {
    
    #region Singleton

    public static EvaluationGameManager instance;

    private void Awake()
    {
        instance = this;

        selectedWords = GameManager.SelectedWords;
        score = GameManager.Score;
        selectedSprites = GameManager.SelectedSprites;
    }

    #endregion

    public delegate void OnScoreModified(int score);
    public OnScoreModified onScoreModifiedCallback;

    public delegate void OnLetterInput(string currentWord);
    public OnLetterInput onLetterInputCallback;

    public delegate void OnWordCorrect(int score, int position);
    public OnWordCorrect onWordChecked;

    public delegate void OnSpriteChange(Sprite targetSprite);
    public OnSpriteChange onSpriteChangeCallback;

    public delegate void OnGameEnded();
    public OnGameEnded onGameEndedCallback;

    public delegate void OnWatchingCountdownFinished();
    public OnWatchingCountdownFinished onWatchingCountdownFinishedCallback;

    public delegate void OnDrawingCountdownFinished();
    public OnDrawingCountdownFinished onDrawingCountdownFinishedCallback;

    public string[] selectedWords;
    
    public Sprite[] selectedSprites;

    [Header("Scoring and time limits")]
    public int wordWeight = 100;
    [Range(5f, 60f)]
    public float limitTimeWatchingWord = 10f;
    [SerializeField]
    private float watchingWordCountdown;
    [HideInInspector]
    public bool isWatchingWord = false;

    [Range(20f, 60f)]
    public float limitTimeDrawingWord = 10f;
    [SerializeField]
    private float drawingWordCountdown;
    [HideInInspector]
    public bool isDrawingWord = false;

    string currentWord;
    int targetWordIndex;

    [HideInInspector]
    public bool isInEvaluationStage = false;
    bool hasSelectedTopics = false;
    bool isGameOver = false;

    readonly string[] abc = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    SJCInventory inventory;
    int wordScore;
    string _inputString;

    public bool WordCorrect = true;

    [SerializeField]
    int score = 0;

    private void Start()
    {
        inventory = SJCInventory.instance;

        onWordChecked += WordWasCorrect;
        onWordChecked += RemoveWordFromList;
        onWordChecked += RemoveImageFromList;
        //TouchInputManager.instance.onLetterRecognizedCallback += GetInputString;

        watchingWordCountdown = limitTimeWatchingWord;
        drawingWordCountdown = limitTimeDrawingWord;

        _inputString = "";
    }

    void Update()
    {
#if MOBILE_INPUT
        if (isInEvaluationStage)
        {
            if (isWatchingWord && !isGameOver)
            {
                watchingWordCountdown -= Time.deltaTime;

                if(watchingWordCountdown < 0f)
                {
                    isWatchingWord = false;
                    isDrawingWord = true;
                    watchingWordCountdown = limitTimeWatchingWord;

                    if(onWatchingCountdownFinishedCallback != null)
                    {
                        onWatchingCountdownFinishedCallback.Invoke();
                    }
                }
            }

            if (isDrawingWord && !isGameOver && drawingWordCountdown >= 0f)
            {
                drawingWordCountdown -= Time.deltaTime;
                //The letter input is coming from touch input manager
                
                if(_inputString != "")
                {
                    if (AreLettersAvailable(_inputString))
                    {
                        currentWord += _inputString;
                        UpdateLettersFreqs(_inputString);

                        if (onLetterInputCallback != null)
                        {
                            onLetterInputCallback.Invoke(currentWord);
                        }
                    }

                    if (WordCorrect)// IsCurrentWordCorrect())
                    {
                        int position = GetPositionOfCorrectWord();
                        if (onWordChecked != null)
                        {
                            wordScore = currentWord.Length * wordWeight;// + wordStreak * wordStreakWeight;
                            onWordChecked.Invoke(wordScore, position);
                        }

                        if (!IsGameOver())
                        {
                            SetNewTargetWord();
                        }

                        FinishEvaluationStage();
                    }
                    _inputString = "";

                }

                if(drawingWordCountdown < 0f)
                {
                    if (onWordChecked != null)
                    {
                        wordScore = currentWord.Length * wordWeight;// + wordStreak * wordStreakWeight;
                        onWordChecked.Invoke(-wordScore, -1);
                    }

                    FinishEvaluationStage();
                }
            }

            if (_inputString != "" && !isGameOver)
                DiscardImpossibleWords();
        }
#else
        if (isInEvaluationStage)
        {
            _inputString = Input.inputString;
            if (_inputString != "" && !isGameOver)
            {
                if (AreLettersAvailable(_inputString.Trim()))
                {
                    currentWord += _inputString.Trim().ToUpper();
                    UpdateLettersFreqs(_inputString.Trim());

                    if (onLetterInputCallback != null)
                    {
                        onLetterInputCallback.Invoke(currentWord);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Return) && !isGameOver)
            {
                if (IsCurrentWordCorrect())
                {
                    int position = GetPositionOfCorrectWord();
                    if (onWordChecked != null)
                    {
                        wordScore = currentWord.Length * wordWeight;// + wordStreak * wordStreakWeight;
                        onWordChecked.Invoke(wordScore, position);
                    }

                    if (!IsGameOver())
                    {
                        SetNewTargetWord();
                    }
                }
                else
                {
                    if (onWordChecked != null)
                    {
                        wordScore = currentWord.Length * wordWeight;// + wordStreak * wordStreakWeight;
                        onWordChecked.Invoke(-wordScore, -1);
                    }
                }

                currentWord = "";

                if (onLetterInputCallback != null)
                {
                    onLetterInputCallback.Invoke(currentWord);
                }

                FinishEvaluationStage();
            }

            if (_inputString != "" && !isGameOver)
                DiscardImpossibleWords();
        }
#endif

    }


    private void GetInputString(string inputString)
    {
        _inputString = inputString.Trim().ToUpper();
    }

    public void GetTopicSelected()
    {
        selectedWords = GameManager.selectedWords;
        score = GameManager.Score;
        selectedSprites = GameManager.selectedSprites;
    }

    public void StartEvaluationStage()
    {
        if (!hasSelectedTopics)
        {
            selectedWords = GameManager.selectedWords;
            score = GameManager.Score;
            selectedSprites = GameManager.selectedSprites;
        }
        if (!isGameOver)
        {
            currentWord = "";
            SetNewTargetWord();
            EvaluationUIManager.instance.ShowEvaluationUI();
            isInEvaluationStage = true;
            isWatchingWord = true;
            isDrawingWord = false;
        }
    }

    public void FinishEvaluationStage()
    {
        currentWord = "";
        _inputString = "";
        drawingWordCountdown = limitTimeDrawingWord;

        isInEvaluationStage = false;
        isWatchingWord = false;
        isDrawingWord = false;

        if (onLetterInputCallback != null)
        {
            onLetterInputCallback.Invoke(currentWord);
        }

        if (IsGameOver())
        {
            isGameOver = true;
            if (onGameEndedCallback != null)
            {
                onGameEndedCallback.Invoke();
            }
        }

        if(onDrawingCountdownFinishedCallback != null)
        {
            onDrawingCountdownFinishedCallback.Invoke();
        }
    }

    bool IsGameOver()
    {
        //Check if there are more words available
        for (int i = 0; i < selectedWords.Length; i++)
        {
            if(selectedWords[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    void DiscardImpossibleWords()
    {
        int j = 0;
        //Discard words that can not be made because no more letters aare available for that word
        for (int i = 0; i < selectedWords.Length; i++)
        {
            if (selectedWords[i] != null)
            {
                char[] lettersInWord = selectedWords[i].ToCharArray();
                int positionOfLetterInAbc;
                for (j = 0; j < lettersInWord.Length; j++)
                {
                    positionOfLetterInAbc = GetPositionOfKeyPressed(lettersInWord[j].ToString());
                    if (inventory.lettersAcquired[positionOfLetterInAbc] == 0)
                    {
                        RemoveWordFromList(0, i);
                        RemoveImageFromList(0, i);

                        if (i == targetWordIndex)
                        {
                            if (!IsGameOver())
                            {
                                SetNewTargetWord();
                            }
                        }
                    }
                }
            }
        }
    }

    void SetNewTargetWord()
    {
        targetWordIndex = Random.Range(0, selectedWords.Length);
     
        while (selectedWords[targetWordIndex] == null)
        {
            targetWordIndex = Random.Range(0, selectedWords.Length);
        }

        if(onSpriteChangeCallback != null)
        {
            onSpriteChangeCallback.Invoke(selectedSprites[targetWordIndex]);
        }
    }

    void WordWasCorrect(int addToScore, int position)
    {
        score += addToScore;

        if(score < 0)
        {
            score = 0;
        }

        if (onScoreModifiedCallback != null)
        {
            onScoreModifiedCallback.Invoke(score);
        }
    }

    void RemoveWordFromList(int score, int position)
    {
        if (position != -1)
            selectedWords[position] = null;
    }

    void RemoveImageFromList(int score, int position)
    {
        if (position != -1)
            selectedSprites[position] = null;
    }

    void UpdateLettersFreqs(string pressedKey)
    {
        if (AreLettersAvailable(pressedKey))
        {
            int positionOfKeyPressed = GetPositionOfKeyPressed(pressedKey);
            inventory.lettersAcquired[positionOfKeyPressed] = inventory.lettersAcquired[positionOfKeyPressed] - 1;
        }
        else
        {
            //Show message: no more letters available
            return;
        }
    }

    bool AreLettersAvailable(string pressedKey)
    {
        int positionOfKeyPressed = GetPositionOfKeyPressed(pressedKey);
        if (positionOfKeyPressed == -1)
            return false;

        if (inventory.lettersAcquired[positionOfKeyPressed] > 0)
            return true;

        return false;
    }

    int GetPositionOfKeyPressed(string pressedKey)
    {
        int positionOfKeyPressed = -1;
        string pressedKeyUpper = pressedKey.ToUpper();
        for (int i = 0; i < abc.Length; i++)
        {
            if (abc[i] == pressedKeyUpper)
            {
                positionOfKeyPressed = i;
                break;
            }
        }
        return positionOfKeyPressed;
    }

    bool IsCurrentWordCorrect()
    {
        if (selectedWords[targetWordIndex] == currentWord)
        {
            return true;
        }
        
        return false;
    }

    int GetPositionOfCorrectWord()
    {
        return targetWordIndex;
    }

    public int GetScore()
    {
        return score;
    }

    public Sprite GetSprite()
    {
        return selectedSprites[targetWordIndex];
    }

}
