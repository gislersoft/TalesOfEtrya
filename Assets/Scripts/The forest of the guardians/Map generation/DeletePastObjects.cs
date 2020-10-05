using UnityEngine;

public class DeletePastObjects : MonoBehaviour {

    public Transform platformsParent;
    private Transform[] objects;
    private Transform player;
    public float distanceBehindPlayer = 10f;

    public float checkRate = 10f;
    private float countdown;

    private void Start()
    {
        player = PlayerManager.instance.player.transform;
        countdown = checkRate;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0f)
        {
            CheckForPastTiles();
            countdown = checkRate;
        }
    }


    void CheckForPastTiles()
    {
        objects = platformsParent.GetComponentsInChildren<Transform>();
        for (int i = objects.Length - 1; i > 0; i--)
        {
            if (objects[i].position.z < (player.position.z - distanceBehindPlayer))
            {
                //Destroy(objects[i].gameObject);
                objects[i].gameObject.SetActive(false);
            }
        }
    }
}
