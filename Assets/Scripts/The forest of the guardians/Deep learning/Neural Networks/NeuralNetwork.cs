using UnityEngine;

[CreateAssetMenu(fileName ="Neural network", menuName ="NN/Neural Network")]
public class NeuralNetwork : ScriptableObject{

    [Header("Layers")]
	public ConvLayer[] convLayers;
	private _Tensor flatten;
    public DenseLayer[] denseLayers;
    public GEMMDense[] gemmLayers;

    //[Header("Optimization")]
    //public Optimizer optimizer;
    //public ErrorFunction errorFunction:

	private bool isInit = false;

    public void TestInitNNGEEM(_Tensor[] initTensors)
    {
        flatten = NN_Utils.AppendToVector(initTensors);

        //Now, we start initializing the dense layers
        gemmLayers[0].InitLayer(new _Tensor[1] { flatten });
        //We perform the same operations as above
        //input_i = results_(i-1)
        for (int i = 1; i < gemmLayers.Length; i++)
        {
            gemmLayers[i].InitLayer(gemmLayers[i - 1].results);
        }

        isInit = true;
    }
    public void TestInitNNDENSE(_Tensor[] initTensors)
    {
        flatten = NN_Utils.AppendToVector(initTensors);

        //Now, we start initializing the dense layers
        denseLayers[0].InitLayer(new _Tensor[1] { flatten });
        //We perform the same operations as above
        //input_i = results_(i-1)
        for (int i = 1; i < denseLayers.Length; i++)
        {
            denseLayers[i].InitLayer(denseLayers[i - 1].results);
        }

        isInit = true;
    }

    public void TestInitNN(_Tensor[] initTensors){
        
        //Start initializing the first layer
        convLayers[0].InitLayer(initTensors);
        //The next layers are initialized with the results of the last layer, since it is the input of the i-th layer
        //input_i = results_(i-1)
        for (int i = 1; i < convLayers.Length; i++)
        {
            convLayers[i].InitLayer(convLayers[i - 1].results);
        }

        int lastConvIndex = convLayers.Length - 1;

        //To start with the dense layers, we first need to flatten the 3D vector coming from the last conv layer.
        flatten = NN_Utils.AppendToVector(convLayers[lastConvIndex].results);

        //We first check if we have dense layers
        if (denseLayers.Length == 0)
        {
            denseLayers = new DenseLayer[1] { new DenseLayer() };
            denseLayers[0].activationFunction = new Softmax();
        }

        //Now, we start initializing the dense layers
        denseLayers[0].InitLayer(new _Tensor[1] { flatten });
        //We perform the same operations as above
        //input_i = results_(i-1)
        for (int i = 1; i < denseLayers.Length; i++)
        {
            denseLayers[i].InitLayer(denseLayers[i - 1].results);
        }

        isInit = true;
	}
    
	public void RunDenseTest(_Tensor[] initTensors)
    {
        //float startTime = Time.realtimeSinceStartup;
        if (isInit) {
            flatten = NN_Utils.AppendToVector(initTensors);
            flatten.Shape();
			_Tensor[] layerResult = denseLayers [0].ForwardPass(new _Tensor[1]{ flatten });
			for (int k = 1; k < denseLayers.Length; k++) {
				layerResult = denseLayers [k].ForwardPass (layerResult);
			}
		}
        //float finishTime = Time.realtimeSinceStartup;

        //Debug.Log("Dense test time: " + (finishTime - startTime));
	}

    public void RunGEMMDenseTest(_Tensor[] initTensors)
    {
        //float startTime = Time.realtimeSinceStartup;
        if (isInit)
        {
            flatten = NN_Utils.AppendToVector(initTensors);
            _Tensor[] layerResult = gemmLayers[0].ForwardPass(new _Tensor[1] { flatten });
            for (int k = 1; k < gemmLayers.Length; k++)
            {
                layerResult = gemmLayers[k].ForwardPass(layerResult);
            }
        }
        //float finishTime = Time.realtimeSinceStartup;

        //Debug.Log("GEMM Dense test time: " + (finishTime - startTime));
    }

    public void RunFullTest(_Tensor[] initTensors)
    {
        if (isInit)
        {

            _Tensor[] layerResult = convLayers[0].ForwardPass(initTensors);

            for (int i = 1; i < convLayers.Length; i++)
            {
                layerResult = convLayers[i].ForwardPass(layerResult);
            }

            flatten = NN_Utils.AppendToVector(layerResult);
            
            layerResult = denseLayers[0].ForwardPass(new _Tensor[1] { flatten });
            for (int k = 1; k < denseLayers.Length; k++)
            {
                layerResult = denseLayers[k].ForwardPass(layerResult);
            }

            layerResult[0].Shape();
            layerResult[0].Print();
            
        }
    }

    public void InitializeNN(_Tensor[] initTensors)
    {
		
		if (convLayers.Length > 0) {			

			//Start initializing the first layer
			convLayers [0].InitLayer (initTensors);
			//The next layers are initialized with the results of the last layer, since it is the input of the i-th layer
			//input_i = results_(i-1)
			for (int i = 1; i < convLayers.Length; i++) {
				convLayers [i].InitLayer (convLayers [i - 1].results);
			}

			int lastConvIndex = convLayers.Length - 1;

            //To start with the dense layers, we first need to flatten the 3D vector coming from the last conv layer.
            flatten = NN_Utils.AppendToVector(convLayers[lastConvIndex].results);
            
			//We first check if we have dense layers
			if (denseLayers.Length == 0) {
				denseLayers = new DenseLayer[1] { new DenseLayer () };
				denseLayers [0].activationFunction = new Softmax ();
			}

			//Now, we start initializing the dense layers
			denseLayers [0].InitLayer (new _Tensor[1]{ flatten });
			//We perform the same operations as above
			//input_i = results_(i-1)
			for (int i = 1; i < denseLayers.Length; i++) {
				denseLayers [i].InitLayer (denseLayers [i - 1].results);
			}
		} else {
            //We reshape the input tensor to a 1D tensor
            flatten = NN_Utils.AppendToVector(initTensors);

			//We first check if we have dense layers
			if (denseLayers.Length == 0) {
				denseLayers = new DenseLayer[1] { new DenseLayer () };
				denseLayers [0].activationFunction = new Softmax ();
			}

			//Now, we start initializing the dense layers
			denseLayers [0].InitLayer (new _Tensor[1]{ flatten });
			//We perform the same operations as above
			//input_i = results_(i-1)
			for (int i = 1; i < denseLayers.Length; i++) {
				denseLayers [i].InitLayer (denseLayers [i - 1].results);
			}
		}
		isInit = true;
    }

    public static NeuralNetwork Copy(NeuralNetwork nn)
    {
        NeuralNetwork copy = new NeuralNetwork();

        //Parameters
        copy.convLayers = nn.convLayers;
        copy.denseLayers = nn.denseLayers;
        copy.flatten = nn.flatten;

        //Optimization
        //copy.optimizer = nn.optimizer;
        //copy.errorFunction = nn.errorFunction;

        copy.isInit = true;

        return copy;
    }

    public _Tensor ForwardPass(_Tensor[] initTensors)
    {
        if(initTensors.Length == 0)
        {
            return new _Tensor();
        }
        if (isInit)
        {
            _Tensor[] layerResult = convLayers[0].ForwardPass(initTensors);

            for (int i = 1; i < convLayers.Length; i++)
            {
                layerResult = convLayers[i].ForwardPass(layerResult);
            }

            flatten = NN_Utils.AppendToVector(layerResult);

            layerResult = denseLayers[0].ForwardPass(new _Tensor[1] { flatten });
            for (int k = 1; k < denseLayers.Length; k++)
            {
                layerResult = denseLayers[k].ForwardPass(layerResult);
            }

            return layerResult[0];
        }
        else
        {
            InitializeNN(initTensors);

            _Tensor[] layerResult = convLayers[0].ForwardPass(initTensors);

            for (int i = 1; i < convLayers.Length; i++)
            {
                layerResult = convLayers[i].ForwardPass(layerResult);
            }

            flatten = NN_Utils.AppendToVector(layerResult);

            layerResult = denseLayers[0].ForwardPass(new _Tensor[1] { flatten });
            for (int k = 1; k < denseLayers.Length; k++)
            {
                layerResult = denseLayers[k].ForwardPass(layerResult);
            }

            return layerResult[0];
        }
    }
}
