using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Jewel", menuName = "Inventory/Jewel" )]
public class Jewel : Item {
    public GameObject jewelModel;
    public int launchSpeed;
    public float delayTime = 0;
    private GetPlayerPosition playerPosition;
    //private GetCameraPosition cameraPosition;


    public override void Use() {
        
        playerPosition = GetPlayerPosition.Instance();
        //cameraPosition = GetCameraPosition.Instance();
        var launchedJewel = Instantiate( jewelModel , playerPosition.rightArmTRansform.position, playerPosition.transform.rotation * Quaternion.Euler( -45, 0, 0 ));
        launchedJewel.GetComponent<Rigidbody>().velocity = launchedJewel.transform.forward * launchSpeed;
        base.Use();
        //Debug.Log(playerPosition.viewPoint.rotation.x);
        //Debug.Log( playerPosition.viewPoint.rotation.y );
        //Debug.Log( playerPosition.viewPoint.rotation.z );
        //StartCoroutine( ExecuteAfterTime( 10 ) );
        //Invoke( "DoSomething", 2 );
        RemoveFromInventory();
    }
    
}
