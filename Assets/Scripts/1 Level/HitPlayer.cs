using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour {
    public AudioSource audioSource;
    public PlayerStatsForFirstLevel playerStats;
    public PurpStatsForFirstLevel enemyStats;


    void Start() {
    }
    //public Collider collider;
    // Use this for initialization
    
    private void OnTriggerEnter(Collider other) {
        audioSource.Play();
        playerStats.TakeDamage(enemyStats.damage);
    }
}
