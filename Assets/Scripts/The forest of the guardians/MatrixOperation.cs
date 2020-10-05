using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class MatrixOperation : MonoBehaviour {

    public static MatrixOperation matrixOperation;

    private void Awake()
    {
        matrixOperation = this;
    }

    [SerializeField]
    private Queue<TensorOperationResult> resultsQueue;
    public delegate _Tensor Operation();

    private void Start()
    {
        resultsQueue = new Queue<TensorOperationResult>();
        matrixOperation = this;
    }
    
    public void RequestMatrixOps(Operation matrixOp, Action<_Tensor> callback)
    {
        ThreadStart threadStart = delegate
        {
            OperationThread(matrixOp, callback);
        };

        new Thread(threadStart).Start();
    }

    public void OperationThread(Operation matrixOp, Action<_Tensor> callback)
    {
        _Tensor result = matrixOp();
        lock (resultsQueue)
        {
            resultsQueue.Enqueue(new TensorOperationResult(result, callback));
        }
    }

    private void Update()
    {
        if(resultsQueue.Count > 0)
        {
            int queueCount = resultsQueue.Count;
            for (int i = 0; i < queueCount; i++)
            {
                TensorOperationResult matrixOperationResult = resultsQueue.Dequeue();
                matrixOperationResult.callback(matrixOperationResult.result);
            }
        }
    }
}


[System.Serializable]
public struct TensorOperationResult
{
    public readonly _Tensor result;
    public readonly Action<_Tensor> callback;

    public TensorOperationResult(_Tensor result, Action<_Tensor> callback)
    {
        this.result = result;
        this.callback = callback;
    }
}
