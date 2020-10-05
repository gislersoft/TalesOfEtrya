using UnityEngine;

public class EndlessTerrain : MonoBehaviour {


    #region Tile spawning management

    DrawTiles drawTiles;

    TileSet lastTileSet;

    Transform camTransform;
    Vector3 camFarClipPlane;

    public float checkRate = 10f;
    private float countdown;

    #endregion
    MapThreading mapThreading;

    private void Start()
    {
        mapThreading = GetComponent<MapThreading>();
        camFarClipPlane = new Vector3(0f, 0f, Camera.main.farClipPlane);
        camTransform = Camera.main.transform;
        drawTiles = GetComponent<DrawTiles>();
        
        drawTiles.DrawTilesMap(0, 0, drawTiles.GetPlatformsToDraw());
        lastTileSet = drawTiles.GetLastPlatform();

        countdown = checkRate;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0f)
        {
            CheckIfNewTiles();
            countdown = checkRate;
        }
    }

    private void CheckIfNewTiles()
    {
        float planeDistanceToOrigin = ((camTransform.position + (Vector3.forward * camFarClipPlane.z)).magnitude);

        Vector3 lastTilePosition = GetCenterOfTileSet(lastTileSet.tiles);
         
        Vector3 planeNormal = -(camTransform.position + (Vector3.forward * camFarClipPlane.z)).normalized;

        float projection = Vector3.Dot(lastTilePosition, planeNormal);
        
        if(projection + planeDistanceToOrigin > 0)
        {
            mapThreading.RequestPlatforms(OnMapDataReceived);
        }
        
    }



    void OnMapDataReceived(PlatformType[] selectedPlatforms)
    {
        PlatformType[] platformType = selectedPlatforms;
        lastTileSet = drawTiles.GetLastPlatform();
        int lastPlatformZ = (int)GetCenterOfTileSet(lastTileSet.tiles).z + drawTiles.yOffset;
        drawTiles.DrawTilesMap(lastPlatformZ, 0, platformType);
    }

    public Vector3 GetCenterOfTileSet(Transform[] tiles)
    {
        if(tiles.Length == 1)
        {
            return tiles[0].position;
        }

        Bounds bounds;
        bounds = new Bounds(tiles[0].position, Vector3.zero);

        for (int i = 1; i < tiles.Length; i++)
        {
            bounds.Encapsulate(tiles[i].position);
        }

        return bounds.center;
    }
}

[System.Serializable]
public struct TileSet
{
    public Transform[] tiles;

    public TileSet(Transform[] tiles)
    {
        this.tiles = tiles;
    }

    public TileSet(Transform tiles)
    {
        this.tiles = new Transform[] { tiles };
    }
}
