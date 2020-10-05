using UnityEngine;

public class TextureManagement {

    static public Texture2D GetRTPixels(RenderTexture rt, TextureFormat textureFormat)
    {
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = rt;

        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rt.width, rt.height, textureFormat, false);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();
        // Restorie previously active render texture
        RenderTexture.active = currentActiveRT;
        return tex;
    }
}
