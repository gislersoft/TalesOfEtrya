using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelFireEvent : MonoBehaviour {

    public int JewelRadious;
    public int JewelDelay;
    public int DurationTime;
    PurpMovementControllerFirstLevel enemyController;
    Animator enemyAnimator;
    Collider[] hitColliders;
    public GameObject particlePrefab;
    private GameObject particle;
    bool inCoolDown = false;

    void Start() {
        StartCoroutine( StarEfect( 0 ) );
        StartCoroutine( StopEfect( 0 + DurationTime ) );
        inCoolDown = true;
        CoolDown( JewelDelay );
    }

    //private void Awake() {
    //    StartCoroutine( ExecuteAfterTime( 2 ) );
    //}

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere( transform.position, JewelRadious );
    }

    IEnumerator StarEfect(float time) {
        yield return new WaitForSeconds( time );
        //Debug.Log( "Explode" );

        hitColliders = Physics.OverlapSphere( gameObject.transform.position, JewelRadious );
        //GameObject.Destroy;
        //DestroyObject(gameObject);
        int i = 0;
        while (i < hitColliders.Length) {
            if (hitColliders[ i ].gameObject.CompareTag( "Enemy" )) {

                enemyController = hitColliders[ i ].gameObject.GetComponent<PurpMovementControllerFirstLevel>();
                enemyAnimator = hitColliders[ i ].gameObject.GetComponent<Animator>();
                enemyAnimator.SetBool( "Dead", true );
                //GeneralEnemiesControllerFirstLevel.instance.StopTutorial(); // ESTO NO DEBE ESTAR COMENTADO
                //enemyAnimator.enabled = false;
                //enemyController.freezed = true;
            }

            i++;
        }
        particle = Instantiate( particlePrefab );
        particle.transform.position = gameObject.transform.position;
    }

    IEnumerator StopEfect(float time) {
        yield return new WaitForSeconds( time );
        //Debug.Log( "Finishing" );
        //GameObject.Destroy;
        //DestroyObject(gameObject);
        int i = 0;
        /*
        while (i < hitColliders.Length) {
            if (hitColliders[ i ].gameObject.CompareTag( "Enemy" )) {
                enemyController = hitColliders[ i ].gameObject.GetComponent<PurpMovementControllerFirstLevel>();
                enemyAnimator = hitColliders[ i ].gameObject.GetComponent<Animator>();
                enemyAnimator.enabled = true;
                enemyController.freezed = false;
            }

            i++;
        }*/
        DestroyObject( particle );
        DestroyObject( gameObject );
    }

    IEnumerator CoolDown(float time) {
        yield return new WaitForSeconds( time );
        inCoolDown = false;
    }
}
