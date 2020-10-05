using UnityEngine;

[CreateAssetMenu(fileName ="ReLU", menuName ="NN/Activation/ReLU")]
public class ReLU : ActivationFunction
{
    public override void Activate(ref _Tensor tensor)
    {
        int i = 0;
        int j = 0;
        //Tensor result = new Tensor(tensor.rows, tensor.columns);
        int rows = tensor.rows, columns = tensor.columns;

        for(int k = 0; k < tensor.length; k++)
        {
            tensor.parameters[i, j] = _ReLU(tensor.parameters[i, j]);

            j = (j + 1) % columns;

            if(j == 0)
            {
                i++;
            }
        }
    }

    private float _ReLU(float value)
    {
        return value > 0 ? value : 0f;
    }
}
