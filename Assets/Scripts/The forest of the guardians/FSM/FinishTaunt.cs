using UnityEngine;

public class FinishTaunt : StateMachineBehaviour {


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FSM_Taunt.isTaunting = false;
        FSM_Taunt.lastTimeAttacked = Time.timeSinceLevelLoad;
    }


}
