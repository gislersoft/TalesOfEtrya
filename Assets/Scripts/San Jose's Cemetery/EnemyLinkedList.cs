using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinkedList {

    [HideInInspector]
    public Transform mySelf;
    public Transform prevEnemy;
    public Transform nextEnemy;
}

public class EnemyLinkedList : MonoBehaviour
{
    public LinkedList linkedList;

    void Start()
    {
        linkedList.mySelf = transform;
    }
}
