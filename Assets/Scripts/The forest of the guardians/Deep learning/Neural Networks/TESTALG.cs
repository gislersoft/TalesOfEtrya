using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTALG : MonoBehaviour {

    public NeuralNetwork dense;
    public NeuralNetwork gemm;

    double[,] weights;
    double[,] input;
    int inputSize = 23000;
    int rows = 512;
    int colums = 512;

    _Tensor inputTensor;
    _Tensor weightTensor;
    float gemmTime = 0, stdTime = 0;
	
    // Use this for initialization
	void Start () {
        ScreenInput.instance.grab = true;

        weights = new double[inputSize, colums];

        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < colums; j++)
            {
                weights[i, j] = (double)Random.Range(-1f, 1f);
            }
        }

        input = new double[1, inputSize];

        for (int i = 0; i < inputSize; i++)
        {
            input[0, i] = (double)Random.Range(-1f, 1f);
        }

        inputTensor = new _Tensor(input);
        weightTensor = new _Tensor(weights);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1)
        {
            float startTime = Time.realtimeSinceStartup;


            NN_Utils.Matmul(inputTensor, weightTensor);

            float endTime = Time.realtimeSinceStartup;

            stdTime = endTime - startTime;
        }
        if(Input.touchCount == 2)
        {
            float initTime = Time.realtimeSinceStartup;
            double[,] result = new double[1, colums];
            alglib.rmatrixgemm(1, colums, inputSize, 1, input, 0, 0, 0, weights, 0, 0, 0, 1, ref result, 0, 0);

            float endTime = Time.realtimeSinceStartup;

            gemmTime = endTime - initTime;
        }
	}

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 20, 100, 100), gemmTime.ToString());
        GUI.Label(new Rect(10, 30, 100, 100), stdTime.ToString());
    }
}
