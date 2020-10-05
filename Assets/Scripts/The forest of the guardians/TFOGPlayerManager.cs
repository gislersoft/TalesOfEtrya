using UnityEngine;

public class TFOGPlayerManager : PlayerManager {

    public new static TFOGPlayerManager instance;
    public Transform Aradis;
    public Transform AradisGFX;
    private new void Awake()
    {
        base.Awake();
        instance = this;
        //ApplicationSettings.SetTargetFrameRate();
    }

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(10, 30, 100, 100), FSM_Base.aradisDifficulty.ToString());
    //    GUI.Label(new Rect(10, 50, 100, 100), Time.timeScale.ToString());
    //}
}
