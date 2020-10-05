using UnityEngine;

public class WordManager : MonoBehaviour {
    public static WordManager instance;

    [HideInInspector]
    public Transform[] letterHolders;
    public delegate void SpawnLettersInMap();
    public SpawnLettersInMap spawnLettersInMapCallback;

    private void Awake()
    {
        instance = this;

        spawnLettersInMapCallback = () =>
        {
            SpawnLetterBehavior[] spawnLetter;
            for (int i = 0; i < letterHolders.Length; i++)
            {
                spawnLetter = letterHolders[i].GetComponentsInChildren<SpawnLetterBehavior>();
                for (int j = 0; j < spawnLetter.Length; j++)
                {
                    StartCoroutine(spawnLetter[j].SpanwTrailOfLetters());
                }
            }
        };
    }

    public void Start()
    {
        spawnLettersInMapCallback = () =>
        {
            SpawnLetterBehavior[] spawnLetter;
            for (int i = 0; i < letterHolders.Length; i++)
            {
                spawnLetter = letterHolders[i].GetComponentsInChildren<SpawnLetterBehavior>();
                for (int j = 0; j < spawnLetter.Length; j++)
                {
                    StartCoroutine(spawnLetter[j].SpanwTrailOfLetters());
                }
            }
        };
    }

    public void GetLetterHolders(GameObject[] holders)
    {
        letterHolders = new Transform[holders.Length];
        for (int i = 0; i < holders.Length; i++)
        {
            letterHolders[i] = holders[i].GetComponent<Transform>();
        }

        spawnLettersInMapCallback();
    }





}
