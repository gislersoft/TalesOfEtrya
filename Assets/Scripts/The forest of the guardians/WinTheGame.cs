using UnityEngine;
using System.Collections;

public class WinTheGame : MonoBehaviour {

    bool loading;
    

    private void Start()
    {
        loading = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.tag == StringsInGame.PlayerTag && !loading)
        {
            loading = true;
            TFGGameManager.instance.WonTheGame();
        }
    }


}
