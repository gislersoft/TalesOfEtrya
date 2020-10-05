using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFOGES_LetterPool : MonoBehaviour {

    public delegate void OnWordPressed(string letter);
    public OnWordPressed onWordPressedCallback;

    [Header("Letters")]
    public Dictionary<string, GameObject[]> lettersDict;
    public Dictionary<string, int> poolIndices;
    public Letter[] letters;
    public int maxPoolSize = 50;

    //public Transform lettersParent;

    //void Start () {
    //    lettersDict = new Dictionary<string, GameObject[]>(26);

    //    poolIndices = new Dictionary<string, int>(letters.Length);

    //    GameObject[] list = new GameObject[maxPoolSize];
    //    for (int i = 0; i < letters.Length; i++)
    //    {
    //        for (int j = 0; j < maxPoolSize; j++)
    //        {
    //            list[j] = Instantiate(letters[i].letterHolder, Vector3.zero, Quaternion.identity, lettersParent);
    //        }

    //        lettersDict.Add(letters[i].name, list);
    //        poolIndices.Add(letters[i].name, 0);
    //    }
    //}

    public void SpawnLetter(string letter)
    {
        //Spawn the letter
        //Increase the value in the index pool
        TFOGES_GameManager.Instance.AddLetterToCurrentWord(letter);
        //if (onWordPressedCallback != null)
        //{
        //    onWordPressedCallback.Invoke(letter);
        //}
        //else
        //{
        //    TFOGES_GameManager.Instance.AddLetterToCurrentWord(letter);
        //}
    }
}
