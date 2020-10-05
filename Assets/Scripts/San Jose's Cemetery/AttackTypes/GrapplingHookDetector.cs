using UnityEngine;
using UnityEngine.AI;

public class GrapplingHookDetector : MonoBehaviour {
    //[Range(0f, 5f)]
    //public float castRadius = 1f;
    //[Range(0f, 100f)]
    //public float maxCheckDistance = 50f;
    //RaycastHit hitObject;
    //GrapplingHookController grapplingHookController;
    //TransitionManager transitionManager;
    //EvaluationGameManager evaluationGameManager;

    //public bool searchNewTarget = false;

    //private void Start()
    //{
    //    grapplingHookController = PlayerManager.instance.player.GetComponent<GrapplingHookController>();
    //    transitionManager = TransitionManager.instance;
    //    evaluationGameManager = EvaluationGameManager.instance;
    //}

    //private void Update()
    //{
    //    if (searchNewTarget)
    //    {
    //        SearchNewTarget();
    //    }
    //    if (!evaluationGameManager.isInEvaluationStage && !searchNewTarget)
    //    {
    //        Debug.DrawRay(transform.position, grapplingHookController.travelDirection * castRadius, Color.yellow);

    //        if (Physics.SphereCast(transform.position, castRadius, grapplingHookController.travelDirection, out hitObject, castRadius))
    //        {
    //            if (hitObject.transform.tag == "Enemy")
    //            {
    //                //grapplingHookController.ResetHook();
    //                searchNewTarget = true;
    //                //transitionManager.EnterEvaluationStage();
    //                PurpsEnemyStats cc = hitObject.transform.GetComponent<PurpsEnemyStats>();
    //                cc.TakeDamage(1000);
    //            }
    //            else
    //            {
    //                grapplingHookController.ResetToPlayer();
    //            }
    //        }
    //    }


    //}


    //public void SearchNewTarget()
    //{
    //    float rotation = 50f;
    //    transform.Rotate(0f, rotation * Time.deltaTime, 0f);
    //    Debug.DrawRay(transform.position, transform.forward * maxCheckDistance, Color.yellow);
    //    if (Physics.SphereCast(transform.position, castRadius, transform.forward, out hitObject, maxCheckDistance))
    //    {
    //        if (hitObject.transform.tag == "Enemy")
    //        {
    //            Debug.Log("XD");
    //            searchNewTarget = false;
    //            CameraController cc = Camera.main.GetComponent<CameraController>();
    //        }
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;

    //    Gizmos.DrawWireSphere(transform.position, castRadius);
    //}

}
