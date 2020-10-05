using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TFOG_CharacterMovement))]
public class ValueGrowthEditor : Editor {

    public override void OnInspectorGUI()
    {
        TFOG_CharacterMovement movement = target as TFOG_CharacterMovement;

        if (DrawDefaultInspector())
        {
            if (!Application.isPlaying)
            {
                movement.speedValueGrowth.exponentialGrowth.initialValue = movement.runSpeed;
            }
            
        }
    }
}
