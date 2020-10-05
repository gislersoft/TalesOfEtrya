using UnityEngine;

public class AradisFSM : StateMachineBehaviour {

    public static bool useFSM = true;

    [Header("Aradis' components")]
    public Transform agentTransform;
    public Transform agentGFX;
    public Rigidbody2D agentRigidbody;

    [Header("Target's components")]
    public Transform targetTransform;

    [Header("Hashes")]
    protected int movementHash;
    protected int attackTriggerHash;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        agentTransform = animator.GetComponent<Transform>();
        targetTransform = TFOGPlayerManager.instance.player.GetComponent<Transform>();
        agentRigidbody = animator.GetComponent<Rigidbody2D>();
        agentGFX = TFOGPlayerManager.instance.AradisGFX.GetComponent<Transform>();
    }
}
