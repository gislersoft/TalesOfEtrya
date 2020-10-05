using UnityEngine;

public class RespawnInteractable : MonoBehaviour {

    [Range(10f, 60f)]
    public float respawnTime = 30f;
    public GameObject letterMeshObject;
    private float counter;
    private bool isInactive = false;
    new private Collider collider;

    private void Start()
    {
        counter = respawnTime;
        collider = GetComponent<BoxCollider>();
    }
  
    private void Update()
    {
        if (isInactive)
        {
            counter -= Time.deltaTime;

            if (counter < 0f)
            {
                counter = respawnTime;
                isInactive = false;
                letterMeshObject.SetActive(true);
                collider.enabled = true;
            }
        }
    }

    public void Inactivate()
    {
        isInactive = true;
        letterMeshObject.SetActive(false);
        collider.enabled = false;
    }

}
