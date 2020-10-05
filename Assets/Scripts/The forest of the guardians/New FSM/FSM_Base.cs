using System.Collections.Generic;
using UnityEngine;

public class FSM_Base : StateMachineBehaviour
{
    //Agents
    [Header("Agents")]
    public static Transform agentTransform;
    public static Transform targetTransform;
    public static CharacterStats agentStats;
    public static CharacterStats targetStats;

    //Heuristic evaluation
    [Header("Difficulty")]
    public Dictionary<AradisDifficulty, DifficultyParameters> difficultyParameters = new Dictionary<AradisDifficulty, DifficultyParameters>
    {
        {AradisDifficulty.EASY, new DifficultyParameters(0.8f, 4, 0) },
        {AradisDifficulty.MEDIUM, new DifficultyParameters(1, 6, 2) },
        {AradisDifficulty.HARD, new DifficultyParameters(1.2f, 8, 5) },
    };
    public static AradisDifficulty aradisDifficulty;
    protected const float valueThreshold = 0.15f;
    protected const float evaluationTime = 5f;
    public static float evaluationCooldown;

    //Attack
    [Header("Attack")]
    public static float attackSpeed = 1.5f;
    public static float attackCooldown;
    public const int maxNumberOfAttacks = 3;
    public static int attackCount;
    public const float meleeRange = 2f;
    public static int currentAttack = 0;
    public static LongRangeCast castSpell;
    public static FaceTowardEnemy facing;
    public static AttackCheck attackCheck;

    //audio
    public static TFOG_AudioManager_Johnny audioManager;

    //Movement
    [Header("Movement")]
    public static readonly Vector3 leftCornerPosition = new Vector3(-13, 1, 15);
    public static readonly Vector3 rightCornerPosition = new Vector3(0, 1, 15);
    public static float moveSpeed = 6f;

    //Animation hashes
    protected static int moveAwayTriggerHash;
    protected static int baseAttackTriggerHash;
    protected static int meleeAttackTriggerHash;
    protected static int moveToPlayerTriggerHash;
    protected static int baseAttackPhaseHash;
    protected static int attackSpeedHash;

    protected static bool started = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //agentTransform = animator.GetComponent<Transform>();
        //targetTransform = TFOGPlayerManager.instance.player.transform;

        //moveAwayTriggerHash = Animator.StringToHash(StringsInGame.MoveAwayTriggerHashName);
        //baseAttackTriggerHash = Animator.StringToHash(StringsInGame.BaseAttackTriggerHashName);
        //meleeAttackTriggerHash = Animator.StringToHash(StringsInGame.MeleeAttackTriggerHash);
        //moveToPlayerTriggerHash = Animator.StringToHash(StringsInGame.MoveToPlayerTriggerHash);
        //baseAttackPhaseHash = Animator.StringToHash(StringsInGame.BaseAttackHash);
        //attackSpeedHash = Animator.StringToHash(StringsInGame.AttackSpeedHash);

        //agentStats = TFOGPlayerManager.instance.Aradis.GetComponent<AradisStats>();
        //targetStats = TFOGPlayerManager.instance.player.GetComponent<PlayerStats>();

        //if (!started)
        //{
        //    aradisDifficulty = AradisDifficulty.MEDIUM;
        //    attackSpeed = difficultyParameters[aradisDifficulty].attackSpeed;
        //    moveSpeed = difficultyParameters[aradisDifficulty].moveSpeed;
        //    agentStats.armor.SetValue(difficultyParameters[aradisDifficulty].armor);
        //    evaluationCooldown = evaluationTime;

        //    started = true;
        //    animator.SetFloat(attackSpeedHash, attackSpeed);
        //}
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.ResetTrigger(moveAwayTriggerHash);
        animator.ResetTrigger(baseAttackTriggerHash);
        animator.ResetTrigger(meleeAttackTriggerHash);
        animator.ResetTrigger(moveToPlayerTriggerHash);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        evaluationCooldown -= Time.deltaTime;
        if(evaluationCooldown <= 0f)
        {
            evaluationCooldown = evaluationTime;
            //Heuristic evaluation
            float heuristicValue =
                (targetStats.CurrentHealth / (float)targetStats.maxHealth) -
                (agentStats.CurrentHealth / (float)agentStats.maxHealth);

            //Set the difficulty
            if (heuristicValue < -valueThreshold)
            {
                aradisDifficulty = AradisDifficulty.EASY;
                attackSpeed = difficultyParameters[aradisDifficulty].attackSpeed;
                moveSpeed = difficultyParameters[aradisDifficulty].moveSpeed;
                agentStats.armor.SetValue(difficultyParameters[aradisDifficulty].armor);
            }
            else if (-valueThreshold <= heuristicValue && heuristicValue <= valueThreshold)
            {
                aradisDifficulty = AradisDifficulty.MEDIUM;
                attackSpeed = difficultyParameters[aradisDifficulty].attackSpeed;
                moveSpeed = difficultyParameters[aradisDifficulty].moveSpeed;
                agentStats.armor.SetValue(difficultyParameters[aradisDifficulty].armor);
            }
            else if (heuristicValue > valueThreshold)
            {
                aradisDifficulty = AradisDifficulty.HARD;
                attackSpeed = difficultyParameters[aradisDifficulty].attackSpeed;
                moveSpeed = difficultyParameters[aradisDifficulty].moveSpeed;
                agentStats.armor.SetValue(difficultyParameters[aradisDifficulty].armor);
            }
            animator.SetFloat(attackSpeedHash, attackSpeed);
        }
    }

    public struct DifficultyParameters
    {
        public float attackSpeed;
        public int armor;
        public float moveSpeed;

        public DifficultyParameters(float attackSpeed, float moveSpeed, int armor)
        {
            this.attackSpeed = attackSpeed;
            this.armor = armor;
            this.moveSpeed = moveSpeed;
        }
    }
}
