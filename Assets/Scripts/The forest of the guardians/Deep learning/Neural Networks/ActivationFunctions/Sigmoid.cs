using UnityEngine;
public class Sigmoid : ActivationFunction
{
    const float EULER = 2.71828182845904f;
    
    public override void Activate(ref _Tensor tensor)
    {
        int i = 0;
        int j = 0;
        //Tensor result = new Tensor(tensor.rows, tensor.columns);
        int rows = tensor.rows, columns = tensor.columns;

        for (int k = 0; k < tensor.length; k++)
        {
            tensor.parameters[i, j] = _Sigmoid(tensor.parameters[i, j]);

            j = (j + 1) % columns;

            if (j == 0)
            {
                i++;
            }
        }

        //return result;
    }

    private float _Sigmoid(float value)
    {
        return 1f / (1 + Mathf.Pow(EULER, value));
    }
}
