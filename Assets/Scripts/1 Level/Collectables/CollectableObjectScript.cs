using UnityEngine;


public class CollectableObjectScript : MonoBehaviour{
    public delegate void OnCollectablePicked();
    public OnCollectablePicked onCollectablePickedCallback;

    #region Singleton
    public static CollectableObjectScript instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

}


