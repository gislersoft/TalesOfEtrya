using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenInput : MonoBehaviour {

    public static ScreenInput instance;

    [Header("Textures")]
    public RenderTexture renderTexture;
    public int rtHeight;
    public int rtWidth;
    public Texture2D screenTexture;
    Color[] pixels;

    [Header("Frames frequency")]
    public int stepFrames = 4;
    public bool grab = false;
    private Queue<Texture2D> textureQueue;
    public Texture2D[] currentTextures;

    [Header("Preprocessing")]
    public TextureFormat textureFormat;


    private void Awake()
    {
        instance = this;
        textureQueue = new Queue<Texture2D>();
        currentTextures = new Texture2D[stepFrames];
        rtHeight = renderTexture.height;
        rtWidth = renderTexture.width;
    }

    //private void Start()
    //{
    //    textureQueue = new Queue<Texture2D>();
    //    currentTextures = new Texture2D[stepFrames];
    //}

    private void OnPostRender()
    {
        if (grab)
        {
            screenTexture = TextureManagement.GetRTPixels(renderTexture, textureFormat);

            textureQueue.Enqueue(screenTexture);

            if (textureQueue.Count > stepFrames)
            {
                textureQueue.Dequeue();
            }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 50, 100, 20), (1 / Time.deltaTime).ToString());
    }

    public double[,,] GetInputTensor()
    {
        Texture2D[] textures = textureQueue.ToArray();
        double[,,] result = new double[rtHeight, rtWidth, stepFrames];
        int totalLength = rtHeight * rtWidth;

        float[] yValues;
        for (int i = 0; i < textures.Length; i++)
        {
            pixels = textures[i].GetPixels();

            yValues = new float[pixels.Length];
            for (int j = 0; j < yValues.Length; j++)
            {
                yValues[0] = Preprocessing.RGB2Gray(pixels[j]).a;
            }
            int row = 0;
            int column = 0;
            for (int pos = 0; pos < totalLength; pos++)
            {
                result[row, column, i] = yValues[pos];

                column = (column + 1) % result.GetLength(1);

                if(column == 0)
                {
                    row++;
                }
            }
        }

        return result;
    }

    public _Tensor[] QLearningTensors()
    {
        Texture2D[] textures = textureQueue.ToArray();

        _Tensor[] tensorCube = new _Tensor[textures.Length];

        float[] yChannelValues;
        for (int i = 0; i < textures.Length; i++)
        {
            pixels = textures[i].GetPixels();

            currentTextures[i] = new Texture2D(textures[i].width, textures[i].height, textureFormat, false);
            currentTextures[i].SetPixels(Preprocessing.RGB2Gray(pixels));
            currentTextures[i].Apply();

            yChannelValues = new float[pixels.Length];
            for (int j = 0; j < pixels.Length; j++)
            {
                yChannelValues[j] = Preprocessing.RGB2Gray(pixels[j]).a;
            }

            tensorCube[i] = new _Tensor(yChannelValues, textures[i].width, textures[i].height);
        }
        
        return tensorCube;
    }

    public _Tensor[] RGBTensorsFromTexture()
    {
        
        pixels = screenTexture.GetPixels();


        int inputHeight = screenTexture.height;
        int inputWidth = screenTexture.width;

		float[,] rChannel = new float[inputHeight, inputWidth];
		float[,] gChannel = new float[inputHeight, inputWidth];
		float[,] bChannel = new float[inputHeight, inputWidth];

        int k;

        int i = 0;
        int j = 0;

        for (k = 0; k < pixels.Length; k++)
        {
            rChannel[i, j] = pixels[k].r;
            gChannel[i, j] = pixels[k].g;
            bChannel[i, j] = pixels[k].b;

            j = (j + 1) % inputWidth;

            if (j == 0)
            {
                i++;
            }
        }

        _Tensor rTensor = new _Tensor(rChannel);
        _Tensor gTensor = new _Tensor(gChannel);
        _Tensor bTensor = new _Tensor(bChannel);

        return new _Tensor[3] { rTensor, gTensor, bTensor };
    }
}
