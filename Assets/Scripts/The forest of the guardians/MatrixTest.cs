using UnityEngine;
using UnityEngine.UI;

public class MatrixTest : MonoBehaviour
{

//    int row = 512;
//    int column = 512;
//    float[,] testMatrix1;
//    float[,] testMatrix2;
//    float[][] jaggedMatrix;
//    float initTimeThreaded;
//    // Use this for initialization
//    void Start()
//    {
//        testMatrix1 = new float[row, column];
//
//        jaggedMatrix = new float[row][];
//
//        for (int i = 0; i < row; i++)
//        {
//
//            jaggedMatrix[i] = new float[column];
//        }
//
//        for (int i = 0; i < row; i++)
//        {
//            for (int j = 0; j < column; j++)
//            {
//                testMatrix1[i, j] = Random.Range(1f, 5f);
//                jaggedMatrix[i][j] = testMatrix1[i, j];
//            }
//        }
//        testMatrix2 = new float[column, row];
//
//        for (int i = 0; i < testMatrix2.GetLength(0); i++)
//        {
//            for (int j = 0; j < testMatrix2.GetLength(1); j++)
//            {
//                testMatrix2[i, j] = Random.Range(0f, 1f);
//            }
//        }
//
//        Tensor t = new Tensor(jaggedMatrix);
//
//
//        float initTime = Time.realtimeSinceStartup;
//
//        t = NN_Utils.Matmul(t, NN_Utils.Transpose(t));
//
//        float endTime = Time.realtimeSinceStartup;
//        Debug.Log("Time elapsed (New): " + (endTime - initTime));
//
//
//        float[,] product = new float[testMatrix1.GetLength(0), testMatrix2.GetLength(1)];
//        initTime = Time.realtimeSinceStartup;
//        for (int row = 0; row < testMatrix1.GetLength(0); row++)
//        {
//            for (int col = 0; col < testMatrix2.GetLength(1); col++)
//            {
//                // Multiply the row of A by the column of B to get the row, column of product.  
//                for (int inner = 0; inner < testMatrix1.GetLength(1); inner++)
//                {
//                    product[row, col] += testMatrix1[row, inner] * testMatrix1[inner, col];
//                }
//            }
//        }
//        endTime = Time.realtimeSinceStartup;
//        Debug.Log("Time elapsed (Standard):

        //ThreadedVectorized(testMatrix1, testMatrix1);
        //TestPointers();
        //TestNoPointer();
        //TestVectorized();

        //testStandard();
        //testVectorizedPointer();
        //testVectorizedThreaded2();

    //}

    //void testVectorizedPointer()
    //{
    //    Tensor matrix1 = new Tensor(jaggedMatrix);
    //    float initTime = Time.realtimeSinceStartup;
    //    initTimeThreaded = initTime;
    //    Tensor result = NN_Utils.Transpose(matrix1);

    //    float endTime = Time.realtimeSinceStartup;
    //    string s1 = "";
    //    for (int i = 0; i < result.rows; i++)
    //    {
    //        for (int j = 0; j < result.columns; j++)
    //        {
    //            s1 += matrix1.matrix[i][j] + " ";
    //        }
    //        s1 += "\n";
    //    }
    //    Debug.Log(s1);
    //    s1 = "";
    //    Debug.Log("--------------------------");
    //    for (int i = 0; i < result.rows; i++)
    //    {
    //        for (int j = 0; j < result.columns; j++)
    //        {
    //            s1 += result.matrix[i][j] + " ";
    //        }
    //        s1 += "\n";
    //    }
    //    Debug.Log(s1);
    //    Debug.Log("Time elapsed (Pointer): " + (endTime - initTime));
    //}

    //void testStandard()
    //{
    //    float[,] testMatrix = testMatrix1;


    //    int length0 = testMatrix.GetLength(0);
    //    int length1 = testMatrix.GetLength(1);
    //    float[,] results = new float[length0, length1];

    //    float initTime = Time.realtimeSinceStartup;
    //    for (int i = 0; i < length0; i++)
    //    {
    //        for (int j = 0; j < length1; j++)
    //        {
    //            results[i, j] = testMatrix[i, j] + testMatrix[i, j];
    //        }
    //    }
    //    float endTime = Time.realtimeSinceStartup;

    //    Debug.Log("Standard: " + (endTime - initTime).ToString());
    //}

    //void testVectorizedThreaded2()
    //{
    //    Matrix test1 = new Matrix(testMatrix1);

    //    float initTime = Time.realtimeSinceStartup;
    //    initTimeThreaded = initTime;
    //    MatrixOperation.Operation operation = () =>
    //    {
    //        return test1 + test1;
    //    };

    //    MatrixOperation.matrixOperation.RequestMatrixOps(operation, Test);
    //}

    //void Test(Matrix matrix)
    //{
    //    float endTime = Time.realtimeSinceStartup;
    //    Debug.Log("Received result in :" + (endTime - initTimeThreaded).ToString());
    //}

    //unsafe void TestNoPointer()
    //{
    //    float[,] testMatrix = testMatrix1;

    //    int length0 = testMatrix.GetLength(0);
    //    int length1 = testMatrix.GetLength(1);
    //    float sum = 0f;
    //    float initTime = Time.realtimeSinceStartup;
    //    for (int i = 0; i < length0; i++)
    //    {
    //        for (int j = 0; j < length1; j++)
    //        {
    //            sum += testMatrix[i, j];
    //        }
    //    }
    //    float endTime = Time.realtimeSinceStartup;

    //    Debug.Log("No pointer: " + (endTime - initTime).ToString() + " Sum = " + sum.ToString());
    //}

    //void TestVectorized()
    //{
    //    float[,] testMatrix = testMatrix1;
    //    float initTime = Time.realtimeSinceStartup;
    //    Matrix test = new Matrix(testMatrix);
    //    float sum = 0f;

    //    for (int i = 0; i < test.length; i++)
    //    {
    //        sum += test.vector[i, 0];
    //    }
    //    float endTime = Time.realtimeSinceStartup;
    //    Debug.Log("Vectorized: " + (endTime - initTime).ToString() + " Sum = " + sum.ToString());
    //}

    //unsafe void TestPointers()
    //{
    //    float[,] testMatrix = testMatrix1;

    //    fixed (float* p = &testMatrix[0, 0])
    //    {
    //        float* pCopy = p;
    //        int length = testMatrix.GetLength(0) * testMatrix.GetLength(1);
    //        float sum = 0f;
    //        float initTime = Time.realtimeSinceStartup;
    //        for (int i = 0; i < length; i++)
    //        {
    //            sum += *(pCopy + i);
    //        }
    //        float endTime = Time.realtimeSinceStartup;
    //        Debug.Log("Pointers: " + (endTime - initTime).ToString() + " Sum = " + sum.ToString());
    //    }
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        Vectorized(testMatrix1, testMatrix2);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        ThreadedVectorized(testMatrix1, testMatrix2);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        Nonvectorized(testMatrix1, testMatrix2);
    //    }
    //}

    //float ThreadedVectorized(float[,] testMatrix1, float[,] testMatrix2)
    //{
    //    Matrix test1 = new Matrix(testMatrix1);
    //    Matrix test2 = new Matrix(testMatrix2, 1);

    //    float initTime = Time.realtimeSinceStartup;
    //    initTimeThreaded = initTime;
    //    MatrixOperation.Operation operation = () =>
    //    {
    //        return Matrix.Matmul(test1, test2);
    //    };

    //    MatrixOperation.matrixOperation.RequestMatrixOps(operation, Test);

    //    float endTime = Time.realtimeSinceStartup;

    //    Debug.Log("Time elapsed (Threaded): " + (endTime - initTime));

    //    return endTime - initTime;
    //}



    //float Vectorized(float[,] testMatrix1, float[,] testMatrix2)
    //{
    //    Matrix test1 = new Matrix(testMatrix1);
    //    Matrix test2 = new Matrix(testMatrix2, 1);

    //    float initTime = Time.realtimeSinceStartup;

    //    //Matrix sumResult = test1 + test2;
    //    //Matrix subResult = test1 - test2;
    //    //Matrix mulResult = test1 * test2;
    //    //Matrix sumScalarResult = test1 + 4f;
    //    //Matrix subScalarResult = test1 - 4f;
    //    //Matrix mulScalarResult = test1 * 4f;

    //    //float dotProduct = Matrix.Dot(test1, test2);

    //    Matrix matmulResult = Matrix.Matmul(test1, test2);

    //    float endTime = Time.realtimeSinceStartup;

    //    Debug.Log("Time elapsed (Vectorized): " + (endTime - initTime));

    //    return endTime - initTime;
    //    //string s = "";
    //    //for (int i = 0; i < matmulResult.length; i++)
    //    //{

    //    //    s += matmulResult.vector[i, 0] + "\t";
    //    //}
    //    //Debug.Log("Vectorized");
    //    //Debug.Log(s);
    //}

    ///*
    // * This method is for performance tests when using a non vectorized operations
    // *
    //*/
    //float Nonvectorized(float[,] testMatrix1, float[,] testMatrix2)
    //{
    //    float[,] sumResult = new float[testMatrix1.GetLength(0), testMatrix1.GetLength(1)];
    //    float[,] subResult = new float[testMatrix1.GetLength(0), testMatrix1.GetLength(1)];
    //    float[,] mulResult = new float[testMatrix1.GetLength(0), testMatrix1.GetLength(1)];
    //    float[,] sumScalarResult = new float[testMatrix1.GetLength(0), testMatrix1.GetLength(1)];
    //    float[,] subScalarResult = new float[testMatrix1.GetLength(0), testMatrix1.GetLength(1)];
    //    float[,] mulScalarResult = new float[testMatrix1.GetLength(0), testMatrix1.GetLength(1)];

    //    float initTime = Time.realtimeSinceStartup;

    //    //for (int i = 0; i < testMatrix1.GetLength(0); i++)
    //    //{
    //    //    for (int j = 0; j < testMatrix1.GetLength(1); j++)
    //    //    {
    //    //        sumResult[i, j] = testMatrix1[i, j] + testMatrix1[i, j];
    //    //    }
    //    //}

    //    //for (int i = 0; i < testMatrix1.GetLength(0); i++)
    //    //{
    //    //    for (int j = 0; j < testMatrix1.GetLength(1); j++)
    //    //    {
    //    //        subResult[i, j] = testMatrix1[i, j] - testMatrix1[i, j];
    //    //    }
    //    //}

    //    //for (int i = 0; i < testMatrix1.GetLength(0); i++)
    //    //{
    //    //    for (int j = 0; j < testMatrix1.GetLength(1); j++)
    //    //    {
    //    //        mulResult[i, j] = testMatrix1[i, j] * testMatrix1[i, j];
    //    //    }
    //    //}

    //    //for (int i = 0; i < testMatrix1.GetLength(0); i++)
    //    //{
    //    //    for (int j = 0; j < testMatrix1.GetLength(1); j++)
    //    //    {
    //    //        sumScalarResult[i, j] = testMatrix1[i, j] + 4f;
    //    //    }
    //    //}

    //    //for (int i = 0; i < testMatrix1.GetLength(0); i++)
    //    //{
    //    //    for (int j = 0; j < testMatrix1.GetLength(1); j++)
    //    //    {
    //    //        subScalarResult[i, j] = testMatrix1[i, j] - 4f;
    //    //    }
    //    //}

    //    //for (int i = 0; i < testMatrix1.GetLength(0); i++)
    //    //{
    //    //    for (int j = 0; j < testMatrix1.GetLength(1); j++)
    //    //    {
    //    //        mulScalarResult[i, j] = testMatrix1[i, j] * 4f;
    //    //    }
    //    //}

    //    //float result = 0f;

    //    //for (int i = 0; i < testMatrix1.GetLength(0); i++)
    //    //{
    //    //    for (int j = 0; j < testMatrix1.GetLength(1); j++)
    //    //    {
    //    //        result += testMatrix1[i, j] * testMatrix1[i, j];
    //    //    }
    //    //}

    //    float[,] product = new float[testMatrix1.GetLength(0), testMatrix2.GetLength(1)];

    //    for (int row = 0; row < testMatrix1.GetLength(0); row++)
    //    {
    //        for (int col = 0; col < testMatrix2.GetLength(1); col++)
    //        {
    //            // Multiply the row of A by the column of B to get the row, column of product.  
    //            for (int inner = 0; inner < testMatrix1.GetLength(1); inner++)
    //            {
    //                product[row, col] += testMatrix1[row, inner] * testMatrix2[inner, col];
    //            }
    //        }
    //    }


    //    float endTime = Time.realtimeSinceStartup;
    //    Debug.Log("Time elapsed (Non-vectorized): " + (endTime - initTime));
    //    return endTime - initTime;


    //    //string s = "";
    //    //for (int i = 0; i < product.GetLength(0); i++)
    //    //{
    //    //    for (int j = 0; j < product.GetLength(1); j++)
    //    //    {
    //    //        s += product[i, j] + "\t";
    //    //    }
    //    //    s += "\n";
    //    //}
    //    //Debug.Log("Non-vectorized");
    //    //Debug.Log(s);
    //}
}
