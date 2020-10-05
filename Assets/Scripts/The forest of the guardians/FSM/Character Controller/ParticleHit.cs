using UnityEngine;

public class ParticleHit : MonoBehaviour {
    CharacterStats playerStats;
    AradisStats aradisStats;

    private void OnParticleCollision(GameObject other)
    {
        aradisStats = AradisController.aradis.GetComponent<AradisStats>();

        if (other.gameObject.tag == StringsInGame.PlayerTag)
        {
            playerStats = other.gameObject.GetComponent<CharacterStats>();
            if (playerStats != null)
            {
                if (aradisStats != null)
                {
                    playerStats.TakeDamage(aradisStats.castDamage.GetValue());
                }
                else
                {
                    playerStats.TakeDamage(5);
                }
            }
        }
    }
}
