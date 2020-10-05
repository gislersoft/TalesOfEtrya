using UnityEngine;

[CreateAssetMenu(fileName = "Platform", menuName = "Tiles/Platform")]
public class Platform : ScriptableObject {

    public string platformName;
    public int yOffset;
    public int xOffset;
    public GameObject[] prefabs;

    public PlatformType type;


    public GameObject GetRandomPrefab()
    {
        if (prefabs.Length == 1)
            return prefabs[0];

        int randomPos = Random.Range(0, prefabs.Length);
        return prefabs[randomPos];
    }
}
