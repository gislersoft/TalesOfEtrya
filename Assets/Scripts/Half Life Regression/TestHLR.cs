using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestHLR : MonoBehaviour {

    public HalfLifeRegressionModel hlrModel;
    public int numberOfExamples;


    [Header("Evaluate all topics UI assets")]
    public TextMeshProUGUI textArea;


    [Header("Evaluate a particular topic")]
    public TMP_Dropdown topicDropdown;
    public TextMeshProUGUI weights;
    public TextMeshProUGUI bias;
    public TextMeshProUGUI prediction;

    public TextMeshProUGUI totalInput;
    public TextMeshProUGUI correctInput;


    void Start () {
        hlrModel = GetComponent<HalfLifeRegressionModel>();
        numberOfExamples = hlrModel.topics.Count;

        var topics = hlrModel.topics;
        topicDropdown.ClearOptions();
        List<string> topics2add = new List<string>();
        foreach(Topic topic in topics)
        {
            topics2add.Add(topic.topicName);

            topic.delta = Random.Range(0, 10);
        }
        topicDropdown.AddOptions(topics2add);

	}
	
	public void AccessedMinigame()
    {
        var predictions = hlrModel.EvaluateTopics(GenerateRandomData());
        string allTopics = "";
        foreach (Topic topic in hlrModel.topics)
        {
            float predValue;
            predictions.TryGetValue(topic, out predValue);

            allTopics += "Topic: " + topic.topicName + " Delta: " + topic.delta.ToString() + " Prediction: " + predValue.ToString() + "\n";
        }

        textArea.SetText(allTopics);
    }

    public Vector3[] GenerateRandomData()
    {
        Vector3[] randomData = new Vector3[numberOfExamples];

        for(int i = 0; i < randomData.Length; i++)
        {
            int total = Random.Range(1, 21);
            int correct = Random.Range(0, total + 1);
            randomData[i] = new Vector3(total, correct, total - correct);
            print(randomData[i]);
        }
        
        return randomData;
    }

    public void EvaluateLasSession()
    {
        string topicName = topicDropdown.captionText.text;
        int topicPos = 0;
        Topic topic2evaluate = null;
        for (int i  = 0; i < hlrModel.topics.Count; i++)
        {
            if (topicName == hlrModel.topics[i].topicName)
            {
                topic2evaluate = hlrModel.topics[i];
                topicPos = i;
            }       
        }
        int total = 0;
        if (!int.TryParse(totalInput.text, out total))
            return;

        int correct = 0;
        if (!int.TryParse(correctInput.text, out correct))
            return;

        int incorrect = total - correct;

        if (topic2evaluate != null)
        {
            hlrModel.UpdateModel(topic2evaluate, new Vector3(total, correct, incorrect));

            ViewEachTopicValues(topicPos);
        }        
    }

    public void ViewEachTopicValues(int topicPos)
    {
        Topic topic2evaluate = hlrModel.topics[topicPos];
        
        weights.SetText("X: " + topic2evaluate.weights.x.ToString() + "\nY: " + topic2evaluate.weights.y.ToString() + "\nZ: " + topic2evaluate.weights.z.ToString());
        bias.SetText(topic2evaluate.bias.ToString());
    } 
}
