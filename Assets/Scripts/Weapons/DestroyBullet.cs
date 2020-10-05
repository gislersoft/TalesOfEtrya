using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    public int JewelDelay;

    void Start() {
        StartCoroutine( ExecuteAfterTime( JewelDelay ) );
    }

    //private void Awake() {
    //    StartCoroutine( ExecuteAfterTime( 2 ) );
    //}

    void OnDrawGizmosSelected() {
    }

    IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds( time );
        DestroyObject( gameObject );
    }
}
