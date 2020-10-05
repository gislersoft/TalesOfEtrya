using UnityEngine;

public class NN_Utils {

    public static _Tensor Reshape(_Tensor input, int rows, int columns)
    {
        _Tensor result = new _Tensor(rows, columns);

        int length = input.length;

        int i = 0, j = 0, m = 0, n = 0;

        for (int k = 0; k < length; k++)
        {
            result.parameters[m, n] = input.parameters[i, j];

            n = (n + 1) % columns;

            if (n == 0)
            {
                m = (m + 1) % rows;
            }

            j = (j + 1) % input.columns;

            if (j == 0)
            {
                i = (i + 1) % input.rows;
            }
        }

        return result;
    }
    
    public static _Tensor Transpose(_Tensor input)
    {
        _Tensor result = new _Tensor(input.columns, input.rows);

        int length = result.rows * result.columns;

        int matrixColumns = input.columns;
        int matrixRows = input.rows;
        float[, ] _matrix = input.parameters;

        for (int i = 0; i < matrixRows; i++)
        {
            for (int j = 0; j < matrixColumns; j++)
            {
                result.parameters[j, i] = _matrix[i, j];
            }
        }

        return result;
    }

    public static _Tensor GEMM(_Tensor lhs, _Tensor rhs)
    {
        //double[,] a = new double[lhs.rows, lhs.columns];
        //double[,] b = new double[rhs.rows, rhs.columns];

        //for (int i = 0; i < lhs.rows; i++)
        //{
        //    for (int j = 0; j < lhs.columns; j++)
        //    {
        //        a[i, j] = (double)lhs.parameters[i, j];
        //    }
        //}

        //for (int i = 0; i < rhs.rows; i++)
        //{
        //    for (int j = 0; j < rhs.columns; j++)
        //    {
        //        b[i, j] = (double)rhs.parameters[i, j];
        //    }
        //}

        double[,] resultGEMM = new double[lhs.rows, rhs.columns];


        //alglib.rmatrixgemm(lhs.rows, rhs.columns, lhs.columns, 1, lhs.dparameters, 0, 0, 0, rhs.dparameters, 0, 0, 0, 1, ref resultGEMM, 0, 0);

        return new _Tensor(resultGEMM);
    }

    public static _Tensor Matmul(_Tensor lhs, _Tensor rhs)
    {
        if (lhs.columns != rhs.rows)
        {
            throw new System.Exception("Sizes are not matching: " + lhs.columns + "!=" + rhs.rows);
        }

        _Tensor result = new _Tensor(lhs.rows, rhs.columns);

        float dotProduct;

        for (int row = 0; row < lhs.rows; row++)
        {
            for (int col = 0; col < rhs.columns; col++)
            {
                // Multiply the row of A by the column of B to get the row, column of product. 
                dotProduct = 0f;
                for (int inner = 0; inner < lhs.columns; inner++)
                {
                    dotProduct += lhs.parameters[row, inner] * rhs.parameters[inner, col];
                }
                result.parameters[row, col] = dotProduct;
            }
        }

        return result;

        rhs = Transpose(rhs);

        int matMulLengthResult = lhs.rows * rhs.columns;
        _Tensor matmulResult = new _Tensor(lhs.rows * rhs.columns, 1);

        int i = 0;
        int j = 0;
        int offset = lhs.columns;
        int placementOffset = lhs.rows;
        //float dotProduct;
        int firstMatrixPosition;
        int secondMatrixPosition;

        

        _Tensor vector1 = Reshape(lhs, lhs.length, 1);

        _Tensor vector2 = Reshape(rhs, rhs.length, 1);
        Debug.Log(matMulLengthResult);
        for (int k = 0; k < matMulLengthResult; k++)
        {

            firstMatrixPosition = i * offset;
            secondMatrixPosition = j * offset;

            dotProduct = 0f;

            for (int h = 0; h < offset; h++)
            {
                dotProduct += vector1.parameters[firstMatrixPosition, 0] * vector2.parameters[secondMatrixPosition, 0];
                firstMatrixPosition++;
                secondMatrixPosition++;
            }

            matmulResult.parameters[placementOffset * i + j, 0] = dotProduct;

            j = (j + 1) % rhs.columns;

            if (j == 0)
            {
                i = (i + 1) % placementOffset;
            }
        }
        return Reshape(matmulResult, lhs.rows, rhs.rows);
    }

    public static float Dot(_Tensor lhs, _Tensor rhs)
    {
        int length = lhs.length;
        float result = 0f;

        int i = 0, j = 0;
        for (int k = 0; k < length; k++)
        {
            result += lhs.parameters[i, j] * rhs.parameters[i, j];

            j = (j + 1) % lhs.columns;

            if (j == 0)
            {
                i++;
            }
        }

        return result;
    }

    public static _Tensor Convolve(_Tensor[] input, _Tensor filter, int padding = 0, int stride = 1)
    {
        int tensorLengthColumns = Mathf.FloorToInt((input[0].columns + 2 * padding - filter.columns) / stride) + 1;
        int tensorLengthRows = Mathf.FloorToInt((input[0].rows + 2 * padding - filter.rows) / stride) + 1;
        int neighbors = (filter.rows - 1) / 2;

        _Tensor result = new _Tensor(tensorLengthRows, tensorLengthColumns);
        float convolutionSum;
        int row = 0;
        int column = 0;
		int filterRow = 0;
		int filterColumn = 0;
        if(padding == 0)
        {
            for (int i = 0; i < input[0].rows - filter.rows + 1; i+=stride)
            {
                for (int j = 0; j < input[0].columns - filter.columns + 1; j+=stride)
                {
                    convolutionSum = 0f;
                    //Sum over channels
                    for (int c = 0; c < input.Length; c++)
                    {
                        filterRow = 0;
                        filterColumn = 0;
                        //Pass over the filter
                        for (int k = i; k < i + filter.rows; k++)
                        {
                            for (int l = j; l < j + filter.columns; l++)
                            {
                                //Sum over filter * input on c channels
                                convolutionSum += filter.parameters[filterRow, filterColumn] * input[c].parameters[k, l];
                                filterColumn++;
                            }

                            filterColumn = 0;
                            filterRow++;
                        }
                    }
                    result.parameters[row, column] = convolutionSum;

                    column++;
                }
                row++;
                column = 0;
            }

            //for (int i = neighbors; i < input[0].rows - neighbors; i+=stride)
            //         {
            //	for (int j = neighbors; j < input[0].columns - neighbors; j+=stride)
            //             {
            //                 convolutionSum = 0f;

            //                 for (int c = 0; c < input.Length; c++)
            //                 {
            //			filterRow = 0;
            //			filterColumn = 0;
            //			for (int k = i - neighbors; k < i + neighbors + 1; k++)
            //                     {
            //				for (int l = j - neighbors; l < j + neighbors + 1; l++)
            //                         {
            //					convolutionSum += filter.parameters [filterRow] [filterColumn] * input[c].parameters[k, l];
            //					filterColumn++;
            //                         }

            //				filterColumn = 0;
            //				filterRow++;
            //                     }
            //                 }
            //                 result.parameters[row, column] = convolutionSum;

            //                 column++;
            //             }
            //	row++;
            //	column = 0;
            //         }
        }

		return result;
    }

    public static _Tensor WinogradConvolve2D(_Tensor[] input, _Tensor filter, int padding = 0, int stride = 1)
    {
        return new _Tensor();
    }

    public static _Tensor MaxPooling(_Tensor input, int neighbors = 1)
    {
        _Tensor pooled = new _Tensor(input.rows - (2 * neighbors), input.columns - (2 * neighbors));
        float maxValue;
        for (int i = neighbors; i < input.rows - neighbors; i++)
        {
            for (int j = neighbors; j < input.columns - neighbors; j++)
            {
                maxValue = 0f;
                for (int k = i - neighbors; k < i + neighbors; k++)
                {
                    for (int l = j - neighbors; l < j + neighbors; l++)
                    {
                        if(input.parameters[k, l] > maxValue)
                        {
                            maxValue = input.parameters[k, l];
                        }
                    }
                }

                pooled.parameters[i - neighbors, j - neighbors] = maxValue;
            }
        }

        return pooled;
    }

    public static _Tensor AppendToVector(params _Tensor[] tensors)
    {
        int vectorLength = 0;
        int i;
        for (i = 0; i < tensors.Length; i++)
        {
            vectorLength += tensors[i].length;
        }

        _Tensor vector = new _Tensor(vectorLength, 1);

        int vectorPos = 0;
        for (i = 0; i < tensors.Length; i++)
        {
            for (int k = 0; k < tensors[i].rows; k++)
            {
                for (int l = 0; l < tensors[i].columns; l++)
                {
                    vector.parameters[vectorPos, 0] = tensors[i].parameters[k, l];
                    vectorPos++;
                }
            }
        }

        return vector;
    }

    public static int Argmax(_Tensor tensor)
    {
        int index = 0;
        float maxValue = float.MinValue;
        for (int i = 0; i < tensor.rows; i++)
        {
            for (int j = 0; j < tensor.columns; j++)
            {
                if(tensor.parameters[i, j] > maxValue)
                {
                    maxValue = tensor.parameters[i, j];
                    index = i + j * tensor.columns; 
                }
            }
        }

        return index;
    }

    public static int Argmax(double[,] tensor)
    {
        int index = 0;
        double maxValue = double.MinValue;
        for (int i = 0; i < tensor.GetLength(0); i++)
        {
            for (int j = 0; j < tensor.GetLength(1); j++)
            {
                if (tensor[i, j] > maxValue)
                {
                    maxValue = tensor[i, j];
                    index = i + j * tensor.GetLength(1);
                }
            }
        }

        return index;
    }

    public static float Max(_Tensor tensor)
    {
        float maxValue = float.MinValue;
        for (int i = 0; i < tensor.rows; i++)
        {
            for (int j = 0; j < tensor.columns; j++)
            {
                if (tensor.parameters[i, j] > maxValue)
                {
                    maxValue = tensor.parameters[i, j];
                }
            }
        }

        return maxValue;
    }

    public static void GEMM(double[,] lhs, double[,] rhs, ref double[,] result)
    {
        alglib.rmatrixgemm(lhs.GetLength(0), rhs.GetLength(1), lhs.GetLength(1), 1, lhs, 0, 0, 0, rhs, 0, 0, 0, 1, ref result, 0, 0);
    }

    public static void Transpose(double[,] matrix, ref double[,] result)
    {
        alglib.rmatrixtranspose(matrix.GetLength(0), matrix.GetLength(1), matrix, 0, 0, ref result, 0, 0);
    }

    public static void RandomInit(ref double[,,,] a)
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                for (int k = 0; k < a.GetLength(2); k++)
                {
                    for (int l = 0; l < a.GetLength(3); l++)
                    {
                        a[i, j, k, l] = Random.Range(-1, 1);
                    }
                }
            }
        }
    }

    public static void RandomInit(ref double[,] a)
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                a[i, j] = Random.Range(-1f, 1f);
            }
        }
    }

    public static void RandomInit(ref double[,,] a)
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                for (int k = 0; k < a.GetLength(2); k++)
                {
                    a[i, j, k] = Random.Range(-1f, 1f);
                }
            }
        }
    }

    public static double[,,] DepthWiseConv(double[,,] inputTensor, double[,,] kernel, int stride = 1)
    {
        int tensorLengthRows = Mathf.FloorToInt((inputTensor.GetLength(0) - kernel.GetLength(0)) / stride) + 1;
        int tensorLengthColumns = Mathf.FloorToInt((inputTensor.GetLength(1) - kernel.GetLength(1)) / stride) + 1;

        double[,,] result = new double[tensorLengthRows, tensorLengthColumns, inputTensor.GetLength(2)];

        int resultRow = 0;
        int resultColumn = 0;
        for (int channel = 0; channel < inputTensor.GetLength(2); channel++)
        {
            for (int i = 0; i < inputTensor.GetLength(0) - kernel.GetLength(0) + 1; i += stride)
            {
                for (int j = 0; j < inputTensor.GetLength(1) - kernel.GetLength(1) + 1; j += stride)
                {
                    double convResult = 0d;
                    int filterRow = 0;
                    int filterCol = 0;
                    for (int k = i; k < i + kernel.GetLength(0); k++)
                    {
                        for (int l = j; l < j + kernel.GetLength(1); l++)
                        {
                            convResult += inputTensor[k, l, channel] * kernel[filterRow, filterCol, channel];
                            filterCol++;
                        }
                        filterCol = 0;
                        filterRow++;
                    }
                    result[resultRow, resultColumn, channel] = Mathf.Clamp((float)convResult, float.MinValue, float.MaxValue);
                    
                    resultColumn++;
                }
                resultRow++;
                resultColumn = 0;
            }
            resultRow = resultColumn = 0;
        }

        return result;
    }

    public static double[,,] PointWiseConv(double[,,] input, double[,,,] kernels)
    {
        int inputRows = input.GetLength(0);
        int inputColumns = input.GetLength(1);
        int inputDepth = input.GetLength(2);

        int kernelDepth = kernels.GetLength(3);


        double[,,] result = new double[inputRows, inputColumns, kernelDepth];

        for (int kernel = 0; kernel < kernelDepth; kernel++)
        {
            for (int i = 0; i < inputRows; i++)
            {
                for (int j = 0; j < inputColumns; j++)
                {
                    double convResult = 0d;

                    for (int channel = i; channel < inputDepth; channel++)
                    {
                        convResult += input[i, j, channel] * kernels[0, 0, channel, kernel];
                    }

                    result[i, j, kernel] = Mathf.Clamp((float)convResult, float.MinValue, float.MaxValue);
                }
            }
        }

        return result;
    }

    public static double[,] Flatten(double[,,] input)
    {
        int inputRows = input.GetLength(0);
        int inputColumns = input.GetLength(1);
        int inputDepth = input.GetLength(2);

        int flattenLength = inputRows * inputColumns * inputDepth;

        double[,] result = new double[1, flattenLength];

        int resultColumn = 0;


        for (int c = 0; c < inputDepth; c++)
        {
            for (int i = 0; i < inputRows; i++)
            {
                for (int j = 0; j < inputColumns; j++)
                {
                    result[0, resultColumn] = input[i, j, c];
                    resultColumn++;
                }
            }
        }

        return result;
    }

    public static void ReLU(ref double[,,] input)
    {
        for (int i = 0; i < input.GetLength(0); i++)
        {
            for (int j = 0; j < input.GetLength(1); j++)
            {
                for (int c = 0; c < input.GetLength(2); c++)
                {
                    if(input[i, j, c] < 0)
                    {
                        input[i, j, c] = 0d;
                    }
                }
            }
        }
    }

    public static void ReLU(ref double[,] input)
    {
        for (int i = 0; i < input.GetLength(0); i++)
        {
            for (int j = 0; j < input.GetLength(1); j++)
            {
                if (input[i, j] < 0)
                {
                    input[i, j] = 0d;
                }
            }
        }
    }

    public static void Softmax(ref double[,] input)
    {
        float sum = 0f;
        for (int i = 0; i < input.GetLength(1); i++)
        {
            sum += Mathf.Exp((float)input[0, i]);
        }
        sum = Mathf.Clamp(sum, float.MinValue, float.MaxValue);

        for (int i = 0; i < input.GetLength(1); i++)
        {
            input[0, i] = Mathf.Clamp(Mathf.Exp((float)input[0, i]) / sum, float.MinValue, float.MaxValue);
            if(input[0, i] == double.NaN)
            {
                input[0, i] = 0;
            }
        }
    }
}


