using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Tiles/Tile")]
public class Tile : ScriptableObject {

    public string tileName;
    public GameObject prefab;
    public TileType type;
}
