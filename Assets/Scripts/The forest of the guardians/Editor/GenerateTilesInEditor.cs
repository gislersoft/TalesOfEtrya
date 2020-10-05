using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DrawTiles))]
public class GenerateTilesInEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawTiles drawTiles = target as DrawTiles;

        if (DrawDefaultInspector())
        {
            if (drawTiles.autoUpdate)
            {
                drawTiles.ClearGameObjects();
                //drawTiles.GetPlatformsToDraw();
                drawTiles.DrawTilesMap(0, 0, drawTiles.GetPlatformsToDraw());
            }
        }

        if (GUILayout.Button("Clear"))
        {
            drawTiles.ClearGameObjects();
        }

        if (GUILayout.Button("Draw"))
        {
            //drawTiles.GetPlatformsToDraw();
            drawTiles.DrawTilesMap(0, 0, drawTiles.GetPlatformsToDraw());
        }
    }
}
