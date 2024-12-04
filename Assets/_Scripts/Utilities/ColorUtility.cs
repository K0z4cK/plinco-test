using UnityEngine;

public enum ColorType { Green = 0, Yellow = 1, Red = 2}


public static class ColorUtility 
{
    public static Color GetColor(ColorType color)
    {
        return color switch
        {
            ColorType.Green => Color.green,
            ColorType.Yellow => Color.yellow,
            ColorType.Red => Color.red,
            _ => Color.white,
        };
    }

    public static string GetLayerName(ColorType color)
    {
        return color switch
        {
            ColorType.Green => "Green",
            ColorType.Yellow => "Yellow",
            ColorType.Red => "Red",
            _ => "Default",
        };
    }
}
