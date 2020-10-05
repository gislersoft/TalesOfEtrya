using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class GrapplingHookController : PlayerInteractionController
{
//    PlayerCombat playerCombat;
//    PlayerStats playerStats;
//    TransitionManager transitionManager;
    

//#region Input position management
//    MultiTargetCameraController cameraController;
//#endregion

//    #region Hook    
//    [Range(0.1f, 1f)]
//    public float castRadius = 0.5f;

//    bool throwed = false;

//    [HideInInspector]
//    public Vector3 travelDirection;
    
//    float distanceTraveled;
//    #endregion
//    public LayerMask layerMask;

//    #region Enemy listing
//    private int nextPosToAddEnemy = 0;
//    private const int hitEnemiesLength = 3;
//    private Transform[] hitEnemies = new Transform[hitEnemiesLength];
//    [HideInInspector]
//    public Transform tree;
//    #endregion

//    protected new void Start()
//    {
//        base.Start();
//        playerCombat = PlayerManager.instance.player.GetComponent<PlayerCombat>();
//        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
//        transitionManager = TransitionManager.instance;
        
//        travelDirection = new Vector3(0f, 0f, 0f);
//        cameraController = cam.GetComponent<MultiTargetCameraController>();

//    }

//    protected new void Update()
//    {
//        base.Update();

//#region Hook input
//        if (Input.GetMouseButtonDown(0) &&
//            playerCombat.cooldown < 0f &&
//            throwed == false)
//        {
//            travelDirection = (worldPointFar - worldPointNear).normalized;
//            ThrowHook();
//        }
//#endregion
//    }

//    void ThrowHook()
//    {
//        Debug.DrawRay(worldPointNear, travelDirection * maxCheckDistance, Color.blue);
//        if (Physics.SphereCast(worldPointNear, castRadius, travelDirection, out hitObject, maxCheckDistance, layerMask))
//        {
//            Debug.Log(hitObject.transform.name);
//            if (hitObject.transform.tag == "Enemy")
//            {

//                cameraController.isMultitargeting = true;
//                if (ListHasEnemies())
//                {
//                    SwapMaterial swapMaterial = hitObject.transform.GetComponent<SwapMaterial>();
//                    if(swapMaterial.currentMaterial == CurrentMaterial.OUTLINE)
//                    {
//                        cameraController.AddEnemiesToList(hitObject.transform);
//                        hitEnemies[nextPosToAddEnemy] = hitObject.transform;
//                        nextPosToAddEnemy = (nextPosToAddEnemy + 1) % hitEnemiesLength;
//                        transitionManager.EnterHittingStage();
//                    }
//                    else if(swapMaterial.currentMaterial == CurrentMaterial.ORIGINAL)
//                    {
//                        cameraController.ClearEnemyList();
//                        ClearEnemyList();
//                        cameraController.AddEnemiesToList(hitObject.transform);
//                        hitEnemies[nextPosToAddEnemy] = hitObject.transform;
//                        nextPosToAddEnemy = (nextPosToAddEnemy + 1) % hitEnemiesLength;
//                        transitionManager.EnterHittingStage();
//                    }
//                }
//                else
//                {
//                    cameraController.AddEnemiesToList(hitObject.transform);
//                    hitEnemies[nextPosToAddEnemy] = hitObject.transform;
//                    nextPosToAddEnemy = (nextPosToAddEnemy + 1) % hitEnemiesLength;
//                    transitionManager.EnterHittingStage();
//                }
//            }

//            if (ListHasEnemies())
//            {
//                if (hitObject.transform.tag == "Tree")
//                {
//                    tree = hitObject.transform;
//                    transitionManager.EnterEvaluationStage();
//                    ClearEnemyList();
//                    cameraController.isMultitargeting = false;
//                    cameraController.ClearEnemyList();
//                }
//                else if (hitObject.transform.tag != "Tree" && 
//                    hitObject.transform.tag != "Enemy")
//                {
//                    transitionManager.ExitHittingStage();
//                    cameraController.isMultitargeting = false;
//                    ClearEnemyList();
//                    cameraController.ClearEnemyList();
//                }
//            }
//            else
//            {
//                transitionManager.ExitHittingStage();
//                cameraController.isMultitargeting = false;
//            }
//        }
//    }

//    private void ClearEnemyList()
//    {
//        for (int i = 0; i < hitEnemiesLength; i++)
//        {
//            hitEnemies[i] = null;
//        }
//    }

//    private void AttachToTree()
//    {
//        StateController[] stateControllers = new StateController[hitEnemiesLength];
//        for (int i = 0; i < hitEnemiesLength; i++)
//        {
//            stateControllers[i] = hitEnemies[i].GetComponent<StateController>();

//            if(stateControllers[i] != null)
//            {
//                stateControllers[i].attachedToTree = true;
//            }
//        }
//    }

//    private bool ListHasEnemies()
//    {
//        for (int i = 0; i < hitEnemiesLength; i++)
//        {
//            if (hitEnemies[i] != null)
//                return true;
//        }

//        return false;
//    }

//    public void ResetToPlayer()
//    {
//        for (int i = 0; i < hitEnemiesLength; i++)
//        {
//            hitEnemies[i] = null;
//        }

//        cameraController.TransitionToPlayer();
//    }
}
