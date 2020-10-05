using UnityEngine;

[System.Serializable]
public abstract class ActivationFunction : ScriptableObject {

    public abstract void Activate(ref _Tensor tensor);
}
