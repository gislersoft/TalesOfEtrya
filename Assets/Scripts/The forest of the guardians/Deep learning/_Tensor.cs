using UnityEngine;
using System;

[Serializable]
public struct _Tensor
{
    public float[,] parameters;
    public int rows, columns, length;
    public int[] shape;
    public readonly static _Tensor Zero = new _Tensor(0, 0);


    public _Tensor(float[,] matrix)
    {
        parameters = matrix;
        rows = matrix.GetLength(0);
        columns = matrix.GetLength(1);
        length = rows * columns;
        shape = new int[2] { rows, columns };
    }

    public _Tensor(double[,] matrix)
    {
        parameters = new float[matrix.GetLength(0), matrix.GetLength(1)];
        rows = matrix.GetLength(0);
        columns = matrix.GetLength(1);
        length = rows * columns;
        shape = new int[2] { rows, columns };

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                parameters[i, j] = (float)matrix[i, j];
            }
        }
    }

    public _Tensor(int rows, int columns, bool randomInit = false)
    {
        this.rows = rows;
        this.columns = columns;
        length = this.rows * this.columns;
        parameters = new float[rows, columns];
        shape = new int[2] { rows, columns };

        if (randomInit)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    parameters[i, j] = UnityEngine.Random.Range(0f, 1f);
                }
            }
        }

    }

    public _Tensor(float[] pixels, int width, int height)
    {
        rows = height;
        columns = width;
        length = rows * columns;
        shape = new int[] { rows, columns };

        parameters = new float[rows, columns];

        int row = 0;
        int col = 0;

        for (int k = 0; k < length; k++)
        {
            parameters[row, col] = pixels[k];

            col = (col + 1) % columns;

            if(col == 0)
            {
                row++;
            }
        }
    }

    public static _Tensor operator +(_Tensor matrix1, _Tensor matrix2)
    {
        _Tensor result = new _Tensor(matrix1.rows, matrix1.columns);

        int row = 0, column = 0, length = matrix1.length;
        int matrixColumns = matrix1.columns, matrixRows = matrix1.rows;
        float[,] _matrix1 = matrix1.parameters, _matrix2 = matrix2.parameters;
        for (int i = 0; i < length; i++)
        {
            result.parameters[row, column] = _matrix1[row, column] + _matrix2[row, column];
            column = (column + 1) % matrixColumns;

            if (column == 0)
            {
                row = (row + 1) % matrixRows;
            }

        }
        return result;
    }

    public static _Tensor operator -(_Tensor matrix1, _Tensor matrix2)
    {
        _Tensor result = new _Tensor(matrix1.rows, matrix1.columns);

        int row = 0, column = 0, length = matrix1.length;
        int matrixColumns = matrix1.columns, matrixRows = matrix1.rows;
        float[, ] _matrix1 = matrix1.parameters, _matrix2 = matrix2.parameters;
        for (int i = 0; i < length; i++)
        {
            result.parameters[row, column] = _matrix1[row, column] - _matrix2[row, column];
            column = (column + 1) % matrixColumns;

            if (column == 0)
            {
                row = (row + 1) % matrixRows;
            }

        }
        return result;
    }

    public static _Tensor operator *(_Tensor matrix1, _Tensor matrix2)
    {
        _Tensor result = new _Tensor(matrix1.rows, matrix1.columns);

        int row = 0, column = 0, length = matrix1.length;
        int matrixColumns = matrix1.columns, matrixRows = matrix1.rows;
        float[,] _matrix1 = matrix1.parameters, _matrix2 = matrix2.parameters;
        for (int i = 0; i < length; i++)
        {
            result.parameters[row, column] = _matrix1[row, column] * _matrix2[row, column];
            column = (column + 1) % matrixColumns;

            if (column == 0)
            {
                row = (row + 1) % matrixRows;
            }

        }
        return result;
    }

    public static _Tensor operator +(_Tensor matrix1, float scalar)
    {
        _Tensor result = new _Tensor(matrix1.rows, matrix1.columns);

        int row = 0, column = 0, length = matrix1.length;
        int matrixColumns = matrix1.columns, matrixRows = matrix1.rows;
        float[,] _matrix1 = matrix1.parameters;
        float _scalar = scalar;
        for (int i = 0; i < length; i++)
        {
            result.parameters[row, column] = _matrix1[row, column] + _scalar;
            column = (column + 1) % matrixColumns;

            if (column == 0)
            {
                row = (row + 1) % matrixRows;
            }

        }
        return result;
    }

    public static _Tensor operator -(_Tensor matrix1, float scalar)
    {
        _Tensor result = new _Tensor(matrix1.rows, matrix1.columns);

        int row = 0, column = 0, length = matrix1.length;
        int matrixColumns = matrix1.columns, matrixRows = matrix1.rows;
        float[, ] _matrix1 = matrix1.parameters;
        float _scalar = scalar;
        for (int i = 0; i < length; i++)
        {
            result.parameters[row, column] = _matrix1[row, column] - _scalar;
            column = (column + 1) % matrixColumns;

            if (column == 0)
            {
                row = (row + 1) % matrixRows;
            }

        }
        return result;
    }

    public static _Tensor operator *(_Tensor matrix1, float scalar)
    {
        _Tensor result = new _Tensor(matrix1.rows, matrix1.columns);

        int row = 0, column = 0, length = matrix1.length;
        int matrixColumns = matrix1.columns, matrixRows = matrix1.rows;
        float[, ] _matrix1 = matrix1.parameters;
        float _scalar = scalar;
        for (int i = 0; i < length; i++)
        {
            result.parameters[row, column] = _matrix1[row, column] * _scalar;
            column = (column + 1) % matrixColumns;

            if (column == 0)
            {
                row = (row + 1) % matrixRows;
            }

        }
        return result;
    }

    public void Print()
    {
        string s = "";
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                s += parameters[i, j] + " ";
            }
            s += "\n";
        }

        Debug.Log(s);
    }

    public void Shape()
    {
        
        
        Debug.Log(shape[0] + " " + shape[1]);
    }
}



/*
public unsafe class Tensor<T> : IList, IList<T>, IReadOnlyList<T>
{
    internal static ITensorArithmetic<T> arithmetic = TensorArithmetic.GetArithmetic<T>();
    internal int[] Dimensions;
    internal long Length;
    private T[] _elements;

    public Tensor()
    {
        _elements = new T[0];
        Dimensions = new int[] { 1 };
        Length = 1;
    }

    public Tensor(int[] dimensions)
    {
        Dimensions = dimensions;

    }

    #region IList members

    public object this[int index]
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public bool IsReadOnly => false;

    public bool IsFixedSize => true;

    public int Count => (int)Length;

    public object SyncRoot => this;

    public bool IsSynchronized => false;

    public int Add(object value)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(object value)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(Array array, int index)
    {
        throw new NotImplementedException();
    }

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public int IndexOf(object value)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, object value)
    {
        throw new NotImplementedException();
    }

    public void Remove(object value)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region IList<T> members
    T IList<T>.this[int index]
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }


    public void Add(T item)
    {
        throw new NotImplementedException();
    }



    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }


    public int IndexOf(T item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, T item)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region IReadOnlyList<T> members
    T IReadOnlyList<T>.this[int index]
    {
        get
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
*/

#if false
public class Matrix {
    
    public float[,] matrix;
    public float[,] vector;
    private int rows;
    private int columns;
    public int length;
    private int axis = 0;

    public Matrix(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        length = rows * columns;
        vector = new float[rows * columns, 1];
        matrix = new float[rows, columns];
    }

    public Matrix(float[,] matrix, int axis = 0){
        this.matrix = matrix;
        rows = matrix.GetLength(0);
        columns = matrix.GetLength(1);
        length = rows * columns;
        this.axis = axis;
        Flatten();
    }

    private void Flatten()
    {
        int maxLength = rows * columns;
        vector = new float[maxLength, 1];

        int i = 0;
        int j = 0;

        if(axis == 0) //vectorize by rows
        {
            for (int position = 0; position < maxLength; position++)
            {
                vector[position, 0] = matrix[i, j];
                j = (j + 1) % columns;

                if (j == 0)
                {
                    i = (i + 1) % rows;
                }
            }
        }
        else if(axis == 1) // Vectorize by columns
        {
            for (int position = 0; position < maxLength; position++)
            {
                vector[position, 0] = matrix[i, j];
                i = (i + 1) % rows;

                if (i == 0)
                {
                    j = (j + 1) % columns;
                }
            }
        }

       
    }



    public static Matrix operator +(Matrix matrix1, Matrix matrix2)
    {
        if(matrix1.length != matrix2.length)
        {
            throw new System.Exception("Sizes are not matching: " + matrix1.length + "!=" + matrix2.length);
        }

        Matrix result = new Matrix(matrix1.rows, matrix1.columns);

        int length = result.length;
        for (int i = 0; i < length; i++)
        {
            result.vector[i, 0] = matrix1.vector[i, 0] + matrix2.vector[i, 0];
        }

        return result;
    }

    public static Matrix operator -(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.length != matrix2.length)
        {
            throw new System.Exception("Sizes are not matching: " + matrix1.length + "!=" + matrix2.length);
        }

        Matrix result = new Matrix(matrix1.rows, matrix1.columns);
        int length = result.length;
        for (int i = 0; i < length; i++)
        {
            result.vector[i, 0] = matrix1.vector[i,0] - matrix2.vector[i, 0];
        }

        return result;
    }

    public static Matrix operator *(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.length != matrix2.length)
        {
            throw new System.Exception("Sizes are not matching: " + matrix1.length + "!=" + matrix2.length);
        }

        Matrix result = new Matrix(matrix1.rows, matrix1.columns);

        int length = result.length;
        for (int i = 0; i < length; i++)
        {
            result.vector[i,0] = matrix1.vector[i,0] * matrix2.vector[i,0];
        }

        return result;
    }

    public static Matrix operator +(Matrix matrix1, float scalar)
    {
        Matrix result = new Matrix(matrix1.rows, matrix1.columns);

        int length = result.length;
        for (int i = 0; i < length; i++)
        {
            result.vector[i,0] = matrix1.vector[i,0] + scalar;
        }

        return result;
    }

    public static Matrix operator -(Matrix matrix1, float scalar)
    {

        Matrix result = new Matrix(matrix1.rows, matrix1.columns);

        int length = result.length;
        for (int i = 0; i < length; i++)
        {
            result.vector[i,0] = matrix1.vector[i,0] - scalar;
        }

        return result;
    }

    public static Matrix operator *(Matrix matrix1, float scalar)
    {
        Matrix result = new Matrix(matrix1.rows, matrix1.columns);

        int length = result.length;
        for (int i = 0; i < length; i++)
        {
            result.vector[i,0] = matrix1.vector[i,0] * scalar;
        }

        return result;
    }

    public static float Dot(Matrix matrix1, Matrix matrix2)
    {
        float result = 0f;

        int length = matrix1.length;
        for (int i = 0; i < length; i++)
        {
            result += matrix1.vector[i,0] * matrix2.vector[i,0];
        }

        return result;
    }

    public static Matrix Matmul(Matrix matrix1, Matrix matrix2)
    {
        if(matrix1.columns != matrix2.rows)
        {
            throw new System.Exception("Sizes are not matching: " + matrix1.columns + "!=" + matrix2.rows);
        }

        int matMulLengthResult = matrix1.rows * matrix2.columns;
        Matrix matmulResult = new Matrix(matrix1.rows, matrix2.columns);

        int i = 0;
        int j = 0;
        int offset = matrix1.columns;
        int placementOffset = matrix1.rows;
        float dotProduct;
        int firstMatrixPosition;
        int secondMatrixPosition;
        for (int k = 0; k < matMulLengthResult; k++)
        {
            
            firstMatrixPosition = i * offset;
            secondMatrixPosition = j * offset;

            dotProduct = 0f;

            for (int h = 0; h < offset; h++)
            {
                dotProduct += matrix1.vector[firstMatrixPosition, 0] * matrix2.vector[secondMatrixPosition, 0];
                firstMatrixPosition++;
                secondMatrixPosition++;
            }
            
            matmulResult.vector[placementOffset * i + j, 0] = dotProduct;


            j = (j + 1) % matrix2.columns;

            if (j == 0)
            {
                i = (i + 1) % placementOffset;
            }
        }
        return matmulResult;
    }
#region OldImplementatio
    //public static void MatmulThread(Matrix matrix1, Matrix matrix2, int init_row, int init_column, int offset, int placementOffset)
    //{
        
    //    //float[,] rowVector = new float[offset, 1];
    //    //float[,] columnVector = new float[offset, 1];

    //    int firstMatrixPosition = init_row * offset;
    //    int secondMatrixPosition = init_column * offset;

    //    float dotProduct = 0f;

    //    for (int i = 0; i < offset; i++)
    //    {
    //        //rowVector[i, 0] = matrix1.vector[firstMatrixPosition, 0];
    //        //firstMatrixPosition++;

    //        //columnVector[i, 0] = matrix2.vector[secondMatrixPosition, 0];
    //        //secondMatrixPosition++;

    //        dotProduct += matrix1.vector[firstMatrixPosition, 0] * matrix2.vector[secondMatrixPosition, 0];
    //        firstMatrixPosition++;
    //        secondMatrixPosition++;
    //    }

    //    //Matrix A = new Matrix(rowVector);
    //    //Matrix B = new Matrix(columnVector);

    //    matmulResult.vector[placementOffset * init_row + init_column, 0] = dotProduct;// Dot(A, B);
        

    //}
#endregion

};
#endif