using UnityEngine;

[RequireComponent(typeof(CharacterCombat))]
public class LoseObstacle : MonoBehaviour {

    int getHitHash;
    Animator playerAnimator;
    CharacterCombat characterCombat;
    CharacterStats playerStats;

    public delegate void OnObstacleCrash();
    public OnObstacleCrash onObstacleCrashCallback;

	void Start () {
        getHitHash = Animator.StringToHash("getHit");
        playerAnimator = PlayerManager.instance.player.GetComponent<Animator>();
        characterCombat = GetComponent<CharacterCombat>();
        playerStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == StringsInGame.PlayerTag)
        {
            playerAnimator.SetTrigger(getHitHash);
            characterCombat.Attack(playerStats);
        }
    }

}
