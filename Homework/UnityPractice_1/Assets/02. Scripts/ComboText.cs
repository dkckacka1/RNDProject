using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboText : MonoBehaviour
{
    public Text text;

    public int defaultFontSize = 14;
    public int fontsizeSpeed = 3;
    public int lastSize;

    float timer;

    private void OnEnable()
    {
        text.fontSize = defaultFontSize;
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float addSize = fontsizeSpeed * timer;
        print((int)addSize);
        text.fontSize = defaultFontSize + (int)addSize;
        if (text.fontSize >= lastSize)
        {
            this.gameObject.SetActive(false);
        }

    }
}
