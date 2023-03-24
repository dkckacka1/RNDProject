using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyUtility
{
    public static string returnColorText(string text, Color color, Color BaseColor)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}<color=#{ColorUtility.ToHtmlStringRGBA(BaseColor)}>"; ;
    }
}
