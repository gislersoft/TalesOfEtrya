using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainGPU : MonoBehaviour {

    public static MountainGPU instance;

    private void Awake()
    {
        instance = this;
    }
    [SerializeField]
    int _instances;
    [SerializeField]
    Mesh _mesh;
    [SerializeField]
    Material _material;
    [SerializeField]
    Vector3 leftRotation;
    [SerializeField]
    Vector3 rightRotation;
    [SerializeField]
    Vector3 scale;
    [SerializeField]
    int offset;

    [SerializeField]
    bool _noShadows = true;

    private List<List<MountainData>> _batches = new List<List<MountainData>>();
    private List<List<Matrix4x4>> _matrixBatches = new List<List<Matrix4x4>>();

    private void Start()
    {
        List<MountainData> currentBatch = new List<MountainData>();
        List<Matrix4x4> currentMatrixBatch = new List<Matrix4x4>();

        Vector3 leftPosition = Vector3.zero;
        Vector3 rightPosition = Vector3.zero;
        
        for (int i = 0; i < _instances; i++)
        {
            leftPosition.z = i * offset;
            MountainData leftSide = new MountainData(leftPosition, Quaternion.Euler(leftRotation.x, leftRotation.y, leftRotation.z), scale);

            rightPosition.x = 4f;
            rightPosition.z = i * offset;
            MountainData rightSide = new MountainData(rightPosition, Quaternion.Euler(rightRotation.x, rightRotation.y, rightRotation.z), scale);

            currentBatch.Add(leftSide);
            currentBatch.Add(rightSide);

            currentMatrixBatch.Add(leftSide.Matrix);
            currentMatrixBatch.Add(rightSide.Matrix);

            if (currentBatch.Count == 1000)
            {
                _batches.Add(currentBatch);
                _matrixBatches.Add(currentMatrixBatch);

                currentBatch = new List<MountainData>();
                currentMatrixBatch = new List<Matrix4x4>();
            }
        }

        if (!_batches.Contains(currentBatch))
        {
            _batches.Add(currentBatch);
            _matrixBatches.Add(currentMatrixBatch);
        }
    }

    private void Update()
    {
        RenderBatches();
    }

    void RenderBatches()
    {
        if (SystemInfo.supportsInstancing)
        {
            for (int b = 0; b < _batches.Count; b++)
            {
                if (_noShadows)
                {
                    Graphics.DrawMeshInstanced(_mesh, 0, _material, _matrixBatches[b], new MaterialPropertyBlock(), castShadows: UnityEngine.Rendering.ShadowCastingMode.Off);
                }
                else
                {
                    Graphics.DrawMeshInstanced(_mesh, 0, _material, _matrixBatches[b]);
                }
            }
        }
        else
        {
            for (int b = 0; b < _batches.Count; b++)
            {
                for (int i = 0; i < _batches[b].Count; i++)
                {
                    Graphics.DrawMesh(_mesh, _matrixBatches[b][i], _material, 0);
                }
                
                
            }
        }
    }



}

public class MountainData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public Matrix4x4 Matrix
    {
        get
        {
            return Matrix4x4.TRS(Position, Rotation, Scale);
        }
    }

    public MountainData(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        Position = pos;
        Rotation = rot;
        Scale = scale;
    }
}
