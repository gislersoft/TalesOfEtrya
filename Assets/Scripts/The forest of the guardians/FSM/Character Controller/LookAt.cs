using UnityEngine;

public class LookAt : MonoBehaviour {

    Transform character;
    public Transform target;

	// Use this for initialization
	void Start () {
        character = transform;
	}
	
	// Update is called once per frame
	void Update () {
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        Vector3 direction = target.position - character.position;

        Quaternion newRot = Quaternion.LookRotation(direction);

        character.rotation = Quaternion.Slerp(character.rotation, newRot, Time.deltaTime * AradisController.rotationSpeed);
    }
}
