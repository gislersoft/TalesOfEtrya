using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public List<Transform> waypointsListArea1;
    public StateController[] enemiesListArea1;

    private void Start()
    {
        for (int i = 0; i < enemiesListArea1.Length; i++)
        {
            enemiesListArea1[i].SetupAI(true, waypointsListArea1);
        }
    }
}
