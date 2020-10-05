using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerPosition : MonoBehaviour {


    #region Singleton
    public static GetPlayerPosition instance;
    private static GetPlayerPosition getPlayerPosition;
    public Transform viewPoint;
    public Transform rightArmTRansform;


    private void Awake() {
        instance = this;
    }

    #endregion
    // Use this for initialization

    public static GetPlayerPosition Instance() {
        if (!getPlayerPosition) {
            getPlayerPosition = FindObjectOfType( typeof( GetPlayerPosition ) ) as GetPlayerPosition;
            if (!getPlayerPosition)
                Debug.LogError( "There needs to be one active GetPlayerPosition script on a GameObject in your scene." );
        }

        return getPlayerPosition;
    }
}
