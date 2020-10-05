using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;

public class PerformanceTest : MonoBehaviour {

    StreamWriter streamWriter;
    public string testName;
    public bool testing;
    public float maxTestTime = 30;
    [SerializeField]
    private float remainingTestTime;
    List<float> frameTimes;
    string label;
    
    // Use this for initialization
	void Start () {
        
        testing = false;
        frameTimes = new List<float>();
        remainingTestTime = maxTestTime;
        label = "Not started";
	}
	


	// Update is called once per frame
	void Update () {
        if (testing)
        {
            frameTimes.Add(Time.deltaTime);
            remainingTestTime -= Time.deltaTime;

            if (remainingTestTime < 0f)
            {
                Stop();
            }
        }


	}

    private void OnGUI()
    {
        GUI.Label(new Rect(10, Screen.height - 50, 1000, 1000), label);
        if(GUI.Button(new Rect(10, Screen.height - 100, 50, 50), "Start"))
        {
            testing = true;
            label = "Started";
        }

    }

    void Stop()
    {
        testing = false;
        try
        {
            streamWriter = new StreamWriter(Application.persistentDataPath + "/" + testName + ".csv", false);
            for (int i = 0; i < frameTimes.Count; i++)
            {
                streamWriter.WriteLine(frameTimes[i].ToString());
            }

            streamWriter.Flush();
            streamWriter.Close();
            label = "File saved at: " + Application.persistentDataPath;
        }catch(IOException e)
        {
            label = e.ToString();
        }
        
        
    }
}
