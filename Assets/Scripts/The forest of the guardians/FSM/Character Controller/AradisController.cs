using UnityEngine;
using UnityEngine.AI;

public enum AradisDifficulty
{
    EASY,
    MEDIUM,
    HARD
}

public class AradisController : StateMachineBehaviour
{
    public static AradisDifficulty aradisDifficulty = AradisDifficulty.HARD;

    [Header("Aradis parameters")]
    public static float movementSpeed = 2;
    public static float speedBoost = 5f;
    public static float rotationSpeed = 5;
    public static float strafeSpeed = 10f;
    public static int rotationOrientation; //0 is left, 1 is right
    public static float playerDistance = 5f;
    public static float castRangeCheckDistance = 10f;
    public static float meleeRangeCheckDistance = 2f;
    public static bool isFastMoving = false;
    public static bool fastMoveRequest = false;
    public static float maxCooldown;


    //Animator hashes
    public static int attackTriggerHash;
    public static int movementHash;
    public static int strafeHash;
    public static int attackModeHash;
    public static int cooldownHash;
    public static int distanceHash;
    public static int tauntHash;
    public static int tauntTriggerHash;
    public static int superTauntTriggerHash;
    public static int isFastMovingHash;

    //Nav mesh
    public static NavMeshAgent agent;

    //Player
    public static Transform player;
    public static Transform aradis;
}
