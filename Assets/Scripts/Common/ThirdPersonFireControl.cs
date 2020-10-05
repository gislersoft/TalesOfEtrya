using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonFireControl : MonoBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public int velocityMultiplier;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Fire();
		}
	}

	void Fire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * velocityMultiplier;

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 4.0f);
	}
}
