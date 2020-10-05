using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelElectricEvent : MonoBehaviour {

    public int JewelRadious;
    public int JewelDelay;
    public int DurationTime;
    PurpMovementControllerFirstLevel enemyController;
    Animator enemyAnimator;
    Collider[] hitColliders;
    public GameObject particlePrefab;
    private GameObject particle;

    void Start() {
        StartCoroutine( StarEfect( JewelDelay ) );
        StartCoroutine( StopEfect( JewelDelay + DurationTime ) );
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
                enemyAnimator.SetBool( "Taking Damage", true );
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
        while (i < hitColliders.Length) {
            if (hitColliders[ i ].gameObject.CompareTag( "Enemy" )) {
                enemyController = hitColliders[ i ].gameObject.GetComponent<PurpMovementControllerFirstLevel>();
                enemyAnimator = hitColliders[ i ].gameObject.GetComponent<Animator>();
                enemyAnimator.SetBool( "Taking Damage", false );
                //enemyAnimator.enabled = true;
                //enemyController.freezed = false;
            }

            i++;
        }
        DestroyObject( particle );
        DestroyObject( gameObject );
    }
}
