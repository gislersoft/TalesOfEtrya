using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCameraPosition : MonoBehaviour {


    #region Singleton
    public static GetCameraPosition instance;
    private static GetCameraPosition getCameraPosition;
    public Transform viewPoint;


    private void Awake() {
        instance = this;
    }

    #endregion
    // Use this for initialization

    public static GetCameraPosition Instance() {
        if (!getCameraPosition) {
            getCameraPosition = FindObjectOfType( typeof( GetCameraPosition ) ) as GetCameraPosition;
            if (!getCameraPosition)
                Debug.LogError( "There needs to be one active GetPlayerPosition script on a GameObject in your scene." );
        }

        return getCameraPosition;
    }
}
