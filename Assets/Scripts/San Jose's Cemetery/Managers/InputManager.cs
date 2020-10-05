using UnityEngine;

public class InputManager : MonoBehaviour {

    #region Singleton

    public static InputManager instance;

    private void Awake()
    {
        instance = this;
        
    }

    #endregion

    public delegate void OnLetterInput(string currentWord);
    public OnLetterInput onLetterInputCallback;

    public delegate void OnWordCorrect(int score, int position);
    public OnWordCorrect onWordCorrectCallback;

    public int wordWeight = 100;

    string currentWord;
    string[] abc = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    SJCInventory inventory;
    string[] selectedWords;
    int wordScore;
    
    private void Start()
    {
        currentWord = "";
        inventory = SJCInventory.instance;
    }

    // Update is called once per frame
    void Update () {
        if(Input.inputString != "")
        {
            if (LettersAvailable(Input.inputString.Trim()))
            {
                currentWord += Input.inputString.Trim().ToUpper();
                UpdateLettersFreqs(Input.inputString.Trim());

                if (onLetterInputCallback != null)
                {
                    onLetterInputCallback.Invoke(currentWord);
                }
            }
        }

        

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (IsCurrentWordCorrect())
            {
                int position = GetPositionOfCorrectWord();
                if (onWordCorrectCallback != null)
                {
                    wordScore = currentWord.Length * wordWeight;// + wordStreak * wordStreakWeight;
                    onWordCorrectCallback.Invoke(wordScore, position);
                }
            }
            else
            {
                if (onWordCorrectCallback != null)
                {
                    wordScore = currentWord.Length * wordWeight;// + wordStreak * wordStreakWeight;
                    onWordCorrectCallback.Invoke(-wordScore, -1);
                }
            }

            currentWord = "";

            if (onLetterInputCallback != null)
            {
                onLetterInputCallback.Invoke(currentWord);
            }
        }
        	
	}

    void UpdateLettersFreqs(string pressedKey)
    {
        if (LettersAvailable(pressedKey))
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

    bool LettersAvailable(string pressedKey)
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
        selectedWords = EvaluationGameManager.instance.selectedWords;
        print(selectedWords.Length);
        foreach (string word in selectedWords)
        {
            if(word == currentWord)
            {
                return true;
            }
        }
        return false;
    }

    int GetPositionOfCorrectWord()
    {
        selectedWords = EvaluationGameManager.instance.selectedWords;
        for(int i = 0; i < selectedWords.Length; i++)
        {
            if (selectedWords[i] == currentWord)
            {
                return i;
            }
        }
        return -1;
    }
}
