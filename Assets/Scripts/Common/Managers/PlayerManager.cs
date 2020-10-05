using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton
    public static PlayerManager instance;
    private static PlayerManager playerManager;
    public GameObject player;
    public Transform viewPoint;


    protected void Awake() {
        instance = this;
    }

    #endregion
    // Use this for initialization

    public static PlayerManager Instance() {
        if (!playerManager) {
            playerManager = FindObjectOfType( typeof( PlayerManager ) ) as PlayerManager;
            if (!playerManager)
                Debug.LogError( "There needs to be one active GetPlayerPosition script on a GameObject in your scene." );
        }

        return playerManager;
    }
}
