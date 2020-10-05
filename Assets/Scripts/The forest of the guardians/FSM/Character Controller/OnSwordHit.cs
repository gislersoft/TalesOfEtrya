using UnityEngine;
using EZCameraShake;

public enum Holder
{
    PLAYER,
    ARADIS
}

public class OnSwordHit : MonoBehaviour {

    AradisStats aradisStats;
    PlayerStats playerStats;
    //CameraShaker cameraShaker;
    public Holder holder;
    public float magnitude;
    public float roughness;
    public float fadeIn;
    public float fadeOut;

    private void Start()
    {
        aradisStats = TFOGPlayerManager.instance.Aradis.GetComponent<AradisStats>();
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == StringsInGame.PlayerTag && aradisStats != null && holder == Holder.ARADIS)
        {
            //cameraShaker.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);

            if (playerStats != null)
            {
                playerStats.TakeDamage(aradisStats.damage.GetValue());
            }
            else
            {
                playerStats.TakeDamage(10);
            }
        }
        else if(other.transform.tag == StringsInGame.EnemyTag && playerStats != null && holder == Holder.PLAYER)
        {
            //cameraShaker.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);

            if(aradisStats != null)
            {
                aradisStats.TakeDamage(playerStats.damage.GetValue());
            }
            else
            {
                aradisStats.TakeDamage(5);
            }
        }

    }
}
