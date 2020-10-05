using UnityEngine;

public class FSM_Taunt : AradisFSM {

    public const float tauntCheckDistance = 5f;
    public static float lastTimeAttacked;
    public const float maxWaitTime = 5f;
    public float waitTime;
    public static bool isTaunting;
    private int tauntTriggerHash;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        tauntTriggerHash = Animator.StringToHash("AradisTauntTrigger");
        waitTime = maxWaitTime;
        lastTimeAttacked = 0f;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitTime = Time.timeSinceLevelLoad;

        if (waitTime > lastTimeAttacked + maxWaitTime && isTaunting == false)
        {
            animator.SetTrigger(tauntTriggerHash);
            isTaunting = true;
            lastTimeAttacked = Time.timeSinceLevelLoad;
        }
    }
}
