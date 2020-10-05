using UnityEngine;

public class DrawTiles : MonoBehaviour{

    public bool autoUpdate = true;
    public bool generate = false;

    [Header("Map gen parameters")]
    [Range(1, 6)]
    public int laneWidth = 3;
    [Range(1, 100)]
    public int laneHeight = 20;
    [Range(0, 10)]
    public int tilesBeforeChangingType = 3;
    [Range(1, 10)]
    public int initialEmptyTiles = 3;
    public int randomSeed = 2;

    [Header("Platforms management")]
    public Transform tilesParent;
    public Platform[] platforms;
    private GameObject[,] currentTiles;
    [HideInInspector]
    public GameObject[] currentPlatforms;
    public int yOffset;
    public int xOffset;
    private TileType[,] selectedTileTypes;
    private PlatformType[] selectedPlatforms;

    private bool firstTime = true;

    private void Start()
    {
        ClearGameObjects();
        randomSeed = Random.Range(0, int.MaxValue);
        Random.InitState(randomSeed);
        LaneLimitBehavior.LaneLimits(laneWidth, xOffset, 1);
        firstTime = true;

        if (generate)
            DrawTilesMap(0, 0, GetPlatformsToDraw());
    }

    public void ClearGameObjects()
    {
        Transform[] childs = tilesParent.GetComponentsInChildren<Transform>();

        for (int i = childs.Length - 1; i > 0; i--)
        {
            DestroyImmediate(childs[i].gameObject);
        }
    }

    public PlatformType[] GetPlatformsToDraw()
    {
        if (firstTime)
        {
            selectedPlatforms = RandomTiles.GrammarBasedPlatformSelector(laneHeight, randomSeed, initialEmptyTiles);
            firstTime = false;

            return selectedPlatforms;
        }
        else
        {
            selectedPlatforms = RandomTiles.GrammarBasedPlatformSelector(laneHeight, randomSeed, 0);

            return selectedPlatforms;
        }
    }

    public void DrawTilesMap(int initPosI, int initPosJ, PlatformType[] selectedPlatforms)
    {
        if (selectedPlatforms == null)
            selectedPlatforms = GetPlatformsToDraw();

        currentPlatforms = new GameObject[selectedPlatforms.Length];

        for (int i = 0; i < currentPlatforms.Length; i++)
        {
            for (int j = 0; j < platforms.Length; j++)
            {
                if(platforms[j].type == selectedPlatforms[i])
                {
                    currentPlatforms[i] = Instantiate(
                        platforms[j].GetRandomPrefab(), 
                        new Vector3(platforms[j].xOffset, 0f, i * platforms[j].yOffset + initPosI), 
                        Quaternion.identity, 
                        tilesParent);
                    break;
                }
                    
            }
        }

        WordManager.instance.GetLetterHolders(currentPlatforms);
    }


    public TileSet GetLastPlatform()
    {
        int lastPlatform = currentPlatforms.Length- 1;

        TileSet tileSet = new TileSet(currentPlatforms[lastPlatform].GetComponent<Transform>());
        return tileSet;
    }

    public TileSet GetLastTileSet()
    {
        int lastTilesRow = currentTiles.GetLength(0) - 1;

        Transform[] tiles = new Transform[currentTiles.GetLength(1)];

        for (int i = 0; i < currentTiles.GetLength(1); i++)
        {
            tiles[i] = currentTiles[lastTilesRow, i].GetComponent<Transform>();

        }

        TileSet tileSet = new TileSet(tiles);

        return tileSet;
    }

}
