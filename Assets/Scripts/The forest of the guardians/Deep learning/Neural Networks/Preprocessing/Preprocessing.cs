using UnityEngine;

public static class Preprocessing {

   
    public static Color RGB2YUV(Color color)
    {
        Color YUV = new Color();

        color = color * 255f;

        YUV.r = color.r * 0.299f + color.g * 0.587f + color.b * 0.114f;
        YUV.g = color.r * -0.147f + color.g * -0.289f + color.b * 0.436f;
        YUV.b = color.r * 0.615f + color.g * -0.515f + color.b * -0.1f;

        YUV = YUV / 255f;

        return YUV;
    }

    public static Color RGB2Gray(Color color)
    {
        Color gray = new Color();

        float mean = (color.r + color.g + color.b) / 3f;
        
        gray.r = mean;
        gray.g = mean;
        gray.b = mean;

        return gray;
    }

    public static Color[] RGB2Gray(Color[] color)
    {
        Color[] gray = new Color[color.Length];

        for (int i = 0; i < color.Length; i++)
        {
            gray[i] = RGB2Gray(color[i]);
        }

        return gray;
    }

    public static Color[] RGB2YUV(Color[] color)
    {
        Color[] YUV = new Color[color.Length];

        for (int i = 0; i < color.Length; i++)
        {
            YUV[i] = RGB2YUV(color[i]);
        }

        return YUV;
    }


}
