using UnityEngine;

public class TFOG_AudioManager_Johnny : MonoBehaviour {
    public AudioSource audioSource;
    [Header("Footsteps")]
    public AudioClip leftFootstep;
    public AudioClip rightFootstep;
    public AudioClip jump;
    public AudioClip land;

    [Header("Attack")]
    public AudioClip swingSword;
    public AudioClip hitEnemy;
    public AudioClip getHit;



    public void LeftFootStep(float volume = 1)
    {
        audioSource.PlayOneShot(leftFootstep, volume);
    }

    public void RightFootStep(float volume = 1)
    {
        audioSource.PlayOneShot(rightFootstep, volume);
    }

    public void Attack(float volume = 1)
    {
        audioSource.PlayOneShot(swingSword, volume);
    }

    public void Hit(float volume = 1)
    {
        audioSource.PlayOneShot(hitEnemy, volume);
    }

    public void GetHit(float volume = 1)
    {
        audioSource.PlayOneShot(getHit, volume);
    }

}
