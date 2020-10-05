using UnityEngine;
[CreateAssetMenu(fileName = "Softmax", menuName = "NN/Activation/Softmax")]
public class Softmax : ActivationFunction {

    const float EULER = 2.71828182845904f;

    public override void Activate(ref _Tensor tensor)
    {
        int i = 0;
        int j = 0;
        //Tensor result = new Tensor(tensor.rows, tensor.columns);
        int rows = tensor.rows, columns = tensor.columns;

        float sum = 0f;

        for (int k = 0; k < tensor.length; k++)
        {
            sum += Mathf.Pow(EULER, tensor.parameters[i, j]);

            j = (j + 1) % columns;

            if (j == 0)
            {
                i++;
            }
        }

        i = 0;
        j = 0;

        for (int k = 0; k < tensor.length; k++)
        {
            tensor.parameters[i, j] = _Softmax(tensor.parameters[i, j], sum);

            j = (j + 1) % columns;

            if (j == 0)
            {
                i++;
            }
        }

        //return result;
    }

    private float _Softmax(float value, float sum)
    { 
        return Mathf.Pow(EULER, value) / sum;
    }
}
