using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyUtility
{
    public static string returnColorText(string text, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>"; ;
    }

    public static string returnAlignmentText(string text, alignmentType type)
    {
        return $"<align=\"{type}\">{text}</align>"; ;
    }

    public static string returnSideText(string leftText, string rightText)
    {
        return $"<align=left>{leftText}<line-height=0>\n<align=right>{rightText}<line-height=1em></align>";
    }
}
