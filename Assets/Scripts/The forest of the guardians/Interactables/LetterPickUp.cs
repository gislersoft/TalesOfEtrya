using UnityEngine;

public class LetterPickUp : Interactable {

    public Letter letter;

    public delegate void OnLetterPickedUp(int score);
    public static OnLetterPickedUp onLetterPickeUpCallback;

    private MeshRenderer meshRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        onLetterPickeUpCallback = (score) =>
        {
            TFGGameManager.instance.IncreaseScore(score);
        };
        
    }

    public override void Interact()
    {
        PickUpLetter();   
    }

    void PickUpLetter()
    {
        audioSource.mute = false;
        SJCInventory.instance.AddLetter(letter);
        audioSource.Play();
        LetterPickedUp();
        meshRenderer.enabled = false;

        Destroy(gameObject, audioSource.clip.length);
    }

    static void LetterPickedUp()
    {
        if (onLetterPickeUpCallback != null)
        {
            onLetterPickeUpCallback.Invoke(50);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact();
    }
}
