using UnityEngine;

public class EvaluationBehavior : AradisController {

    //Evaluation

    public static float aggresionRate = 0.1f;
    public readonly static float evaluationTime = 10f;
    public static float evaluationCountdown;
    public static float easyCooldown = 5f;
    public static float mediumCooldown = 3f;
    public static float hardCooldown = 1.5f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        aggresionRate = 0f;
        evaluationCountdown = evaluationTime;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        evaluationCountdown -= Time.deltaTime;

        if (evaluationCountdown < 0f)
        {
            aggresionRate = EvaluateHeuristic();
            evaluationCountdown = evaluationTime;
        }
        
        if (aggresionRate < -0.2f)
        {
            maxCooldown = easyCooldown;
            aradisDifficulty = AradisDifficulty.EASY;
        }
        else if (aggresionRate >= -0.2f && aggresionRate <= 0.2f)
        {
            maxCooldown = mediumCooldown;
            aradisDifficulty = AradisDifficulty.MEDIUM;
        }
        else if (aggresionRate > 0.2f)
        {
            maxCooldown = hardCooldown;
            aradisDifficulty = AradisDifficulty.HARD;
        }
    }

    public float EvaluateHeuristic()
    {
        int playerCurrentHealth = player.GetComponent<PlayerStats>().CurrentHealth;
        int playerMaxHealth = player.GetComponent<PlayerStats>().maxHealth;

        int aradisCurrentHealth = aradis.GetComponent<AradisStats>().CurrentHealth;
        int aradisMaxHealth = aradis.GetComponent<AradisStats>().maxHealth;

        return (playerCurrentHealth / (float)playerMaxHealth) - (aradisCurrentHealth / (float)aradisMaxHealth);
    }
}
