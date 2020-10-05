using UnityEngine;

public class TopicManager : MonoBehaviour {

    public static TopicManager instance;
    public Topic[] topics;
    [SerializeField]
    private Topic currentTopic;
    [SerializeField]
    private int selectedTopic;
    [SerializeField]
    private int currentWord = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(topics.Length == 0)
        {
            return;
        }

        selectedTopic = Random.Range(0, topics.Length);
        currentTopic = topics[selectedTopic];
    }

    public string GetCurrentWord()
    {
        return currentTopic.words[currentWord];
    }

    public void NextWord()
    {
        currentWord = (currentWord + 1) % currentTopic.words.Count;
    }

}
