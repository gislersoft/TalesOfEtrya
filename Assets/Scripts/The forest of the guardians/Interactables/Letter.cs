using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Letter", menuName = "Inventory/Letter")]
public class Letter : Item {

    public char letter;
    public GameObject letterHolder;
}
