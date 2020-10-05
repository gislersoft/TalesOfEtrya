using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelIceEvent : MonoBehaviour {

    public int JewelRadious;
    public int JewelDelay;
    public int DurationTime;
    PurpMovementControllerFirstLevel enemyController;
    Animator enemyAnimator;
    Collider[] hitColliders;
    public GameObject particlePrefab;
    private GameObject particle;

    void Start() {
        Debug.Log( particlePrefab.transform.position.y );
        StartCoroutine( StarEfect( JewelDelay ) );
        StartCoroutine( StopEfect( JewelDelay + DurationTime ) );
    }

    //private void Awake() {
    //    StartCoroutine( ExecuteAfterTime( 2 ) );
    //}

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere( transform.position, JewelRadious );
    }

    IEnumerator StarEfect(float time) {
        yield return new WaitForSeconds( time );
        //Debug.Log( "Explode" );

        hitColliders = Physics.OverlapSphere( transform.position, JewelRadious );
        //GameObject.Destroy;
        //DestroyObject(gameObject);
        int i = 0;
        while (i < hitColliders.Length) {
            if (hitColliders[ i ].gameObject.CompareTag( "Enemy" )) {

                enemyController = hitColliders[ i ].gameObject.GetComponent<PurpMovementControllerFirstLevel>();
                enemyAnimator = hitColliders[ i ].gameObject.GetComponent<Animator>();
                enemyAnimator.enabled = false;
                enemyController.freezed = true;
            }

            i++;
        }
        particle = Instantiate( particlePrefab );
        
        particle.transform.position = new Vector3(transform.position.x, transform.position.y + particlePrefab.transform.position.y, gameObject.transform.position.z);
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
                enemyAnimator.enabled = true;
                enemyController.freezed = false;
            }

            i++;
        }
        DestroyObject( particle );
        DestroyObject( gameObject );
    }
}
