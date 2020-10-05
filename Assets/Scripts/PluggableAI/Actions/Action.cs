using UnityEngine;

public abstract class ActionSO : ScriptableObject {
    //SO stands for Scriptable Object because mesh brush has already an Action class
    public abstract void Act(StateController controller);
}
