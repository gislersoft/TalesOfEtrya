using UnityEngine;

public class CluePickUp : Interactable
{

    public Item item;
    public AudioSource audio;
    public ParticleSystem particle;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        audio.Play();
        //particle.Play();
        FinderController.instance.CollectablePickedUp();
        Destroy(gameObject);
        //Debug.Log("Picking up item " + item.name);
        /*bool wasPickesUp = Inventory.instance.Add(item);
        if (wasPickesUp)
        {
            audio.Play();
            particle.Play();
            Destroy(gameObject);
            FinderController.instance.CollectablePickedUp();
        }
        Inventory.instance.Remove(item);*/
    }
}