using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty
{
    A1,
    A2,
    B1,
    B2
}

[System.Serializable]
[CreateAssetMenu(fileName = "Topic", menuName = "Topics/Topic")]
public class Topic : ScriptableObject
{
    [Header("Topic properties")]
    public string topicName;
    public Difficulty difficulty;

    [Header("Words in topic")]
    public List<string> words = new List<string>();
    public List<Sprite> images = new List<Sprite>();

    [HideInInspector]
    public int delta = 0;

    [HideInInspector]
    public Vector3 weights = new Vector3(0f, 0f, 0f);
    [HideInInspector]
    public float bias = 0f;

    public void AllWordsToUpperCase()
    {
        int wordCount = words.Count;
        for (int i = 0; i < wordCount; i++)
        {
            words[i] = words[i].ToUpper();
        }
    }
}