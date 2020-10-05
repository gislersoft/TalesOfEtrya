using UnityEngine;

[CreateAssetMenu(fileName = "Conv layer", menuName = "NN/Layers/Convolutional")]
public class ConvLayer : NN_Layer {

    [Header("Convolutional layer parameters")]
    //[HideInInspector, SerializeField]
    public _Tensor[] kernels;
    public int stride;
    public int padding;
    public int kernelSize;
    
	public override void InitLayer (_Tensor[] inputs)
	{
		this.inputs = inputs;
		results = new _Tensor[units];
		kernels = new _Tensor[units];
        int tensorLengthColumns = Mathf.FloorToInt((inputs[0].columns + 2 * padding - kernelSize) / stride) + 1;
        int tensorLengthRows = Mathf.FloorToInt((inputs[0].rows + 2 * padding - kernelSize) / stride) + 1;

        for (int i = 0; i < units; i++) //Units refers to the number of kernels in the layer
		{
			kernels[i] = new _Tensor(kernelSize, kernelSize);

            for (int k = 0; k < kernels[i].rows; k++)
            {
                for (int l = 0; l < kernels[i].columns; l++)
                {
                    kernels[i].parameters[k, l] = 1f;
                }
            }
            results[i] = new _Tensor(tensorLengthRows, tensorLengthColumns);
		}

		Length = units * (Mathf.FloorToInt((this.inputs[0].rows + 2 * padding - kernelSize) / stride) + 1) * (Mathf.FloorToInt((this.inputs[0].columns + 2 * padding - kernelSize) / stride) + 1);
	}

	public override _Tensor[] ForwardPass(_Tensor[] inputs)
    {
        for (int i = 0; i < kernels.Length; i++)
        {
            results[i] = NN_Utils.Convolve(inputs, kernels[i], padding, stride) + bias;
        }

        return results;
    }

//	public override void InitLayer()
//	{
//		kernels = new Tensor[units];
//		for (int i = 0; i < units; i++) //Units refers to the number of kernels in the layer
//		{
//			kernels[i] = new Tensor(kernelSize, kernelSize, true);
//		}
//	}
//
//
//    public override void InitLayer(params int[] dimensions)
//    {
//        throw new System.NotImplementedException();
//    }
//
//    public override void InitLayer(Tensor initTensor)
//    {
//        kernels = new Tensor[units];
//        for (int i = 0; i < units; i++) //Units refers to the number of kernels in the layer
//        {
//            kernels[i] = new Tensor(kernelSize, kernelSize, true);
//        }
//    }
//
//    public override Tensor[] ForwardPass(Tensor[] inputs)
//    {
//        results = new Tensor[units];
//
//        for (int i = 0; i < kernels.Length; i++)
//        {
//            results[i] = NN_Utils.Convolve(inputs, kernels[i], padding, stride);
//        }
//
//        return results;
//    }
//
//    public override Tensor ForwardPass(Tensor input)
//    {
//        throw new System.NotSupportedException("This operation is only available for dense layers");
//    }
//
//    public override void BackwardPass()
//    {
//        throw new System.NotImplementedException();
//    }

    
}
