using UnityEngine;

public abstract class _Decision : ScriptableObject {
    public abstract bool Decide(StateController controller);
}
