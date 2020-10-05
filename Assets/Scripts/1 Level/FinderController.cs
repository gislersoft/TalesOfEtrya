using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class FinderController : MonoBehaviour
{
    public delegate void OnScoreModified(int score);
    public OnScoreModified onScoreModifiedCallback;

    public delegate void OnTimerModified(double timer);
    public OnTimerModified onTimerModifiedCallback;

    public delegate void OnClueModified(String clue);
    public OnClueModified onClueModifiedCallback;

    #region Singleton
    public static FinderController instance;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        instance = this;
    }

    #endregion

    public List<GameObject> collectables;

    public int scorePerSucess;
    public int secondsPerSucess;
    public int initialTime;

    public bool timeTrial = false;

    bool gameFinished = false;

    //public GameObject firstClue;
    //public GameObject lastClue;

    

    //Private atributes
    double timeLeft;
    int score;
    string actualClue;

    SpritesAndText spritesAndText;
    InGameUIManagerTimerClue inGameUIManagerTimerClue;
    ObjectiveManager objectivesManager;

    int clueIndex = 0;
    int collectablesLength = 0;

    private static FinderController finderController;
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 30;
        CollectableObjectScript.instance.onCollectablePickedCallback += CollectablePickedUp;

        inGameUIManagerTimerClue = InGameUIManagerTimerClue.Instance();
        objectivesManager = ObjectiveManager.Instance();

        timeLeft = initialTime;
        score = 0;

        collectablesLength = collectables.Count;

        //Debug.Log(collectablesLength);
        collectables = OrderCollectables(collectables);

        //Debug.Log(collectablesLength);

        objectivesManager.SetCollectables(collectables);
        objectivesManager.AddObjectives();

        DeactivateCollectables();
        UpdateGUI();
    }

    public static FinderController Instance()
    {
        if (!finderController)
        {
            finderController = FindObjectOfType(typeof(FinderController)) as FinderController;
            if (!finderController)
                Debug.LogError("There needs to be one active DisplayManager script on a GameObject in your scene.");
        }

        return finderController;
    }

    public bool GetGameStatus() {
        return gameFinished;
    }


    // Update is called once per frame
    void Update()
    {
        if(timeTrial)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimeGUI();
            }
            else
            {
                StopTimeTrial();
            }
        }
    }

    //Desactiva todos los recolectables
    void DeactivateCollectables()
    {
        for (int i = 0; i < collectables.Count; i++)
        {
            collectables[i].SetActive(false);
        }
    }

    void ActivateCollectables()
    {
        if (clueIndex < collectablesLength)
        {
            //Debug.Log("Activating collectable " + randomIndex);
            collectables[clueIndex].SetActive(true);
            //actualClue = collectables[randomIndex].GetComponent<Text>().text;
            //Debug.Log(randomIndex);
        }
        else if (clueIndex >= collectablesLength)
        {
            //WinState();
            StopTimeTrial();
            gameFinished = false;
            Debug.Log("Ya no hay mas objetos a recolectar");
        }
    }

    public List<GameObject> Shuffle<GameObject>(List<GameObject> list)
    {
        int randomIndex;
        List<GameObject> arr = list;
        for (int i = 0; i < arr.Count; i++)
        {
            GameObject temp = arr[i];
            randomIndex = UnityEngine.Random.Range(i, arr.Count);
            arr[i] = arr[randomIndex];
            arr[randomIndex] = temp;
        }
        return arr;
    }

    public List<GameObject> OrderCollectables(List<GameObject> input)
    {
        List<GameObject> arr = input;

        List<GameObject> arrFirst = new List<GameObject>();
        List<GameObject> arrRandom = new List<GameObject>();
        List<GameObject> arrLast = new List<GameObject>();

        List<GameObject> orderList = new List<GameObject>();

        for (int i = 0; i < arr.Count; i++)
        {
            spritesAndText = arr[i].GetComponent<SpritesAndText>();
            if (spritesAndText.firstClue)
            {
                arrFirst.Add(arr[i]);
            }else if (spritesAndText.lastClue)
            {
                arrLast.Add(arr[i]);
            }
            else if ((!spritesAndText.firstClue && !spritesAndText.lastClue) || (spritesAndText.firstClue && spritesAndText.lastClue))
            {
                arrRandom.Add(arr[i]);
            }
        }

        arrRandom = Shuffle(arrRandom);
        orderList = arrFirst.Concat(arrRandom).Concat(arrLast).ToList();
        return orderList;
    }

    public void CollectablePickedUp()
    {
        clueIndex++;
        Debug.Log("activating: " + clueIndex);
        score += scorePerSucess;
        timeLeft += secondsPerSucess;
        ActivateCollectables();
        objectivesManager.CheckObjective();
        UpdateGUI();
    }

    //Actualiza la interfaz grafica
    void UpdateGUI()
    {
        if (onScoreModifiedCallback != null)
        {
            onScoreModifiedCallback.Invoke(score);
        }

        if (onClueModifiedCallback != null)
        {
            onClueModifiedCallback.Invoke(actualClue);
        }
    }

    void UpdateTimeGUI()
    {
        if (onTimerModifiedCallback != null)
        {
            onTimerModifiedCallback.Invoke(timeLeft);
        }
    }

    public void StartTimeTrial()
    {
        timeTrial = true;
        timeLeft += Time.deltaTime;
        ActivateCollectables();
        UpdateGUI();
    }

    public void StopTimeTrial()
    {
        timeTrial = false;
    }

    private void PauseGame()
    {
        inGameUIManagerTimerClue.StopClueSearching();
        StopTimeTrial();
        //pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
}
   

