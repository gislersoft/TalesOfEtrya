using UnityEngine;

[CreateAssetMenu(fileName ="Dense layer", menuName ="NN/Layers/Dense")]
public class DenseLayer : NN_Layer {

	public _Tensor kernel;

	public override void InitLayer(_Tensor[] inputs)
	{
		this.inputs = inputs;
		results = new _Tensor[]{ new _Tensor(units, 1)};
		kernel = new _Tensor (units, inputs[0].rows, true);
		bias = 0f;
	}

	public override _Tensor[] ForwardPass(_Tensor[] inputs)
    {
        results[0] = (NN_Utils.Matmul(kernel, inputs[0])) + bias;
        activationFunction.Activate(ref results[0]);

        return results;
    }
}
