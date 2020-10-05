using UnityEngine;

public class CoinPickUp : Interactable {

    public delegate void OnCoinPickedUp(int score);
    public static OnCoinPickedUp onCoinPickeUpCallback;

    private MeshRenderer meshRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        onCoinPickeUpCallback = (score) =>
        {
            TFGGameManager.instance.IncreaseScore(score);
        };

    }

    public override void Interact()
    {
        audioSource.mute = false;
        SJCInventory.instance.AddCoin();
        audioSource.Play();

        meshRenderer.enabled = false;

        if (onCoinPickeUpCallback != null)
        {
            onCoinPickeUpCallback.Invoke(1);
        }

        Destroy(gameObject, audioSource.clip.length);
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact();
    }
}
