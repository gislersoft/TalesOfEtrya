using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour {

    public GameObject jewelModel;
    public int launchSpeed;
    public float delayTime = 0;
    private GetPlayerPosition playerPosition;
    private GetCameraPosition cameraPosition;

    void FixedUpdate() {
        simulatePath();
    }

    /// <summary>
    /// Simulate the path of a launched ball.
    /// Slight errors are inherent in the numerical method used.
    /// </summary>
    /// 
    void simulatePath() {
        playerPosition = GetPlayerPosition.Instance();
        cameraPosition = GetCameraPosition.Instance();
        var launchedJewel = Instantiate( jewelModel, playerPosition.rightArmTRansform.position, playerPosition.transform.rotation * Quaternion.Euler( -45, 0, 0 ) );
        launchedJewel.GetComponent<Rigidbody>().velocity = launchedJewel.transform.forward * launchSpeed;
    }
}
