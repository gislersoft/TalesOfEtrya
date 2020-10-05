using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    /*
        The manager has to:
            Read the words
            Divide the words into letters
            Assign letters to tombstones
            Assign random letter so letterless tombstones

        */
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnScoreModified(int score);
    public OnScoreModified onScoreModifiedCallback;

    [Header("Letter Management")]
    public List<Letter> lettersModelsList = new List<Letter>();
    public List<Transform> lettersHoldersPositions = new List<Transform>();
    public GameObject tombstonePrefab;

    static int score = 0;
    readonly char[] abc = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    char[] letters;
    int wordsLength = 0;
    int selectedIndex = 0;

    [HideInInspector]
    public static string[] selectedWords;
    [HideInInspector]
    public static Sprite[] selectedSprites;

    [Space(10)]
    [Header("Topics")]
    public List<Topic> topics;

    private void Start()
    {
        PurpsEnemyStats.onEnemyKilledCallback += EnemyKilled;
        LetterPickUp.onLetterPickeUpCallback += LetterPickedUp;

        selectedIndex = Random.Range(0, topics.Count);

        topics[selectedIndex].AllWordsToUpperCase();

        //Selection of words
        selectedWords = topics[selectedIndex].words.ToArray();
        selectedSprites = topics[selectedIndex].images.ToArray();
        CountLettersInWords(selectedWords);
        DivideWords(selectedWords);
        AssignLettersToTombstones();
    }

    #region Words management
    void CountLettersInWords(string[] words)
    {
        int wordsArrayLength = words.Length;
        for(int i = 0; i < wordsLength; i++)
        {
            wordsLength += words[i].Length;
        }

        letters = new char[wordsLength];
    }

    void DivideWords(string[] words)
    {
        char[] dividedWord;
        int wordsArrayLength = words.Length;
        int charArrayLength = 0;

        int posToAdd = 0;
        for (int i = 0; i < wordsLength; i++)
        {
            dividedWord = words[i].ToCharArray();
            charArrayLength = dividedWord.Length;
            for(int j = 0; j < charArrayLength; j++)
            {
                letters[posToAdd] = dividedWord[j];
                posToAdd++;
            }
        }
    }

    void AssignLettersToTombstones()
    {
        int randomPos;
        int[] selectedPositions = new int[letters.Length];
        foreach (Transform tombstone in lettersHoldersPositions)
        {
            if (!AllPositionsUsed(selectedPositions))
            {
                randomPos = Random.Range(0, letters.Length);
                while (PositionAlreadySelected(selectedPositions, randomPos))
                {
                    randomPos = Random.Range(0, letters.Length);
                }
                
                //At this point, the position should be available
                selectedPositions[randomPos] = 1; //Mark the position as used
                //Debug.Log(randomPos + " " + letters[randomPos]);
                //Now we start modifying the prefab to get the desired letter above the tombstone
                InstantiatePrefabs(tombstone, letters[randomPos]);

            }
            else
            {
                //Fill the remaining tombstones with random letters
                randomPos = Random.Range(0, abc.Length);
                InstantiatePrefabs(tombstone, abc[randomPos]);
            }
        }
    }

    bool AllPositionsUsed(int[] usedPositions)
    {
        int counter = 0;
        for(int i = 0; i < usedPositions.Length; i++)
        {
            if(usedPositions[i] == 1)
            {
                counter++;
            }
        }

        return (counter == usedPositions.Length) ? true : false;
    }

    bool PositionAlreadySelected(int[] usedPositions, int desiredPosition)
    {
        //0 for unused
        //1 for used
        return (usedPositions[desiredPosition] == 1) ? true  : false;
    }

    void InstantiatePrefabs(Transform tombstoneTransform, char selectedLetter)
    {
        Vector3 offset = new Vector3(0f, 1.70f, 0f);
        GameObject tombstone = Instantiate(tombstonePrefab, tombstoneTransform.position, tombstoneTransform.rotation, tombstoneTransform);

        switch (selectedLetter)
        {
            #region Instantiation switch
            case 'A':
                
                Instantiate(
                    lettersModelsList[(int)Letters.A].letterHolder, 
                    tombstone.transform.position + offset, 
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'B':
                Instantiate(
                    lettersModelsList[(int)Letters.B].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'C':
                Instantiate(
                    lettersModelsList[(int)Letters.C].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'D':
                Instantiate(
                    lettersModelsList[(int)Letters.D].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'E':
                Instantiate(
                    lettersModelsList[(int)Letters.E].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'F':
                Instantiate(
                    lettersModelsList[(int)Letters.F].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;

            case 'G':
                Instantiate(
                    lettersModelsList[(int)Letters.G].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'H':
                Instantiate(
                    lettersModelsList[(int)Letters.H].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'I':
                Instantiate(
                    lettersModelsList[(int)Letters.I].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'J':
                Instantiate(
                    lettersModelsList[(int)Letters.J].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'K':
                Instantiate(
                    lettersModelsList[(int)Letters.K].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'L':
                Instantiate(
                    lettersModelsList[(int)Letters.L].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'M':
                Instantiate(
                    lettersModelsList[(int)Letters.M].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'N':
                Instantiate(
                    lettersModelsList[(int)Letters.N].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'O':
                Instantiate(
                    lettersModelsList[(int)Letters.O].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'P':
                Instantiate(
                    lettersModelsList[(int)Letters.P].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'Q':
                Instantiate(
                    lettersModelsList[(int)Letters.Q].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'R':
                Instantiate(
                    lettersModelsList[(int)Letters.R].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'S':
                Instantiate(
                    lettersModelsList[(int)Letters.S].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'T':
                Instantiate(
                    lettersModelsList[(int)Letters.T].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'U':
                Instantiate(
                    lettersModelsList[(int)Letters.U].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'V':
                Instantiate(
                    lettersModelsList[(int)Letters.V].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'W':
                Instantiate(
                    lettersModelsList[(int)Letters.W].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'X':
                Instantiate(
                    lettersModelsList[(int)Letters.X].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'Y':
                Instantiate(
                    lettersModelsList[(int)Letters.Y].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
            case 'Z':
                Instantiate(
                    lettersModelsList[(int)Letters.Z].letterHolder,
                    tombstone.transform.position + offset,
                    tombstone.transform.rotation,
                    tombstone.transform);
                break;
                #endregion
        }
    }
    #endregion

    #region Score management

    void EnemyKilled(int addToScore)
    {
        score += addToScore;

        if (score < 0)
        {
            score = 0;
        }

        if (onScoreModifiedCallback != null)
        {
            onScoreModifiedCallback.Invoke(score);
        }
    }

    void LetterPickedUp(int addToScore)
    {
        score += addToScore;

        if (score < 0)
        {
            score = 0;
        }

        if (onScoreModifiedCallback != null)
        {
            onScoreModifiedCallback.Invoke(score);
        }
    }

    #endregion


    public static int Score
    {
        get
        {
            return score;
        }
    }

    public static string[] SelectedWords
    {
        get
        {
            return selectedWords;
        }
    }

    public static Sprite[] SelectedSprites
    {
        get
        {
            return selectedSprites;
        }
    }
}
