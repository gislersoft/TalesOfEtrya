using System.Collections.Generic;
using UnityEngine;

public class HalfLifeRegressionModel : MonoBehaviour {

    public List<Topic> topics = new List<Topic>();
    private Dictionary<Topic, float> prediction_percentages = new Dictionary<Topic, float>();

    [Header("HLR Hyperparameters")]
    [Range(0f, 1f)]
    public const float ALPHA = 0.01f;
    [Range(0f, 1f)]
    public const float LAMBDA = 0.1f;
    [Range(0f, 1f)]
    public const float LEARNING_RATE = 0.001f;

    private void Start()
    {
        prediction_percentages.Clear();
    }

    public float HalfLife(Topic topic, Vector3 practiceResults)
    {
        float k = Vector3.Dot(topic.weights, practiceResults) + topic.bias;

        return Mathf.Pow(2.0f, k);
    }


    public Dictionary<string, float> Predict(Topic topic, Vector3 practiceResults)
    {
        float h_hat = HalfLife(topic, practiceResults);
        float p_hat = Mathf.Pow(2f, (-topic.delta / h_hat));

        Dictionary<string, float> predictions = new Dictionary<string, float>();

        predictions.Add("p_hat", p_hat);
        predictions.Add("h_hat", h_hat);

        return predictions;
    }

    public float Loss(Topic topic, Dictionary<string, float> predictions, Vector3 practiceResults)
    {
        float p_diff = CalculatePDiff(topic, predictions, practiceResults);
        float h_diff = CalculateHDiff(topic, predictions, practiceResults);

        float weight = LAMBDA * topic.weights.sqrMagnitude;

        return p_diff + h_diff + weight;
    }

    private float CalculatePDiff(Topic topic, Dictionary<string, float> predictions, Vector3 practiceResults)
    {
        //practiceResults.x = total 
        //practiceResults.y = correct
        //practiceResults.z = incorrect
        float pr_proportion = practiceResults.y / practiceResults.x;

        float p_hat;
        float p_diff = 0;
        if (predictions.TryGetValue("p_hat", out p_hat))
        {
            p_diff = Mathf.Pow(pr_proportion - p_hat, 2);
        }

        return p_diff;
    }

    private float CalculateHDiff(Topic topic, Dictionary<string, float> predictions, Vector3 practiceResults)
    {
        //practiceResults.x = total 
        //practiceResults.y = correct
        //practiceResults.z = incorrect
        float pr_proportion = practiceResults.y / practiceResults.x;

        float h_hat;
        float h_diff = 0;
        float h = -topic.delta / (Mathf.Log(pr_proportion, 2));
        if (predictions.TryGetValue("h_hat", out h_hat))
        {
            h_diff = ALPHA * Mathf.Pow(h - h_hat, 2);
        }

        return h_diff;
    }

    public void UpdateWeights(Topic topic, Dictionary<string, float> predictions, Vector3 practiceResults)
    {
        //practiceResults.x = total 
        //practiceResults.y = correct
        //practiceResults.z = incorrect
        float pr_proportion = practiceResults.y / practiceResults.x;

        float p_hat;
        float h_hat;
        if (predictions.TryGetValue("p_hat", out p_hat) && predictions.TryGetValue("h_hat", out h_hat))
        {
            float dlp_dw = 2f * (p_hat - pr_proportion) * Mathf.Pow(Mathf.Log(2), 2) * p_hat * (topic.delta / h_hat) * pr_proportion;

            float h = -topic.delta / (Mathf.Log(pr_proportion, 2));
            float dlh_dw = 2f * ALPHA * (h_hat + h) * Mathf.Log(2) * h_hat * pr_proportion;

            float dwk_dw = 2 * LAMBDA * topic.weights.magnitude; 

            float dl_dw = dlp_dw + dlh_dw + dwk_dw;

            Vector3 gradientVector = new Vector3(LEARNING_RATE * dl_dw, LEARNING_RATE * dl_dw, LEARNING_RATE * dl_dw);
            
            topic.weights -= gradientVector;
        }
    }

    public void UpdateModel(Topic topic, Vector3 practiceResults)
    {
        
        var predictions = Predict(topic, practiceResults);
        var loss_per_topic = Loss(topic, predictions, practiceResults);

        UpdateWeights(topic, predictions, practiceResults);

        float prediction;
        if (predictions.TryGetValue("p_hat", out prediction))
        {
            if (prediction_percentages.ContainsKey(topic))
            {
                prediction_percentages.Remove(topic);
                prediction_percentages.Add(topic, prediction);
            }
        }
    }

    public Dictionary<Topic, float> EvaluateTopics(Vector3[] lastPracticeResultsPerTopic) //Of a particular player
    {
        int i = 0;
        Dictionary<Topic, float> predictionsPerTopic = new Dictionary<Topic, float>();
        foreach(Topic topic in topics)
        {
            var prediction = Predict(topic, lastPracticeResultsPerTopic[i]);
            float predictedProbability = 0f;

            if (prediction.TryGetValue("p_hat", out predictedProbability))
            {
                predictionsPerTopic.Add(topic, predictedProbability);
            }
        }

        return predictionsPerTopic;
    }

    public void ShowTopicsInUI(Dictionary<Topic, float> predictionsPerTopic)
    {
        //Go to ui and show the topics that need to be remembered first
    }


}
