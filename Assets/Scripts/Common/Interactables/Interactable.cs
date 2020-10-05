/*
 * Creator: Juan David Suaza
 * Purpose: Define a base clase for all interactable objects in the game
*/
using UnityEngine;



public class Interactable : MonoBehaviour {
    //Interaction radius
    [Range(1f, 5f)]
    public float interactionRadius = 4f;
    private bool isFocus;
    private bool hasInteracted = false;

    Transform player;

    public void OnFocused(Transform playerTransform)
    {
        this.isFocus = true;
        this.player = playerTransform;
        this.hasInteracted = false;
    }

    public void OnDefocused()
    {
        this.isFocus = false;
        this.player = null;
        this.hasInteracted = false;
    }

	public virtual void Interact(){
		print ("Interacting with " + this.name);
        this.hasInteracted = true;
	}

    //void Update()
    //{
    //    if (isFocus && !this.hasInteracted)
    //    {
    //        float distance = Vector3.Distance(player.position, transform.position);
    //        if(distance < radius)
    //        {
				//Interact ();
    //            this.hasInteracted = true;
    //        }
    //    }
    //}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

}
