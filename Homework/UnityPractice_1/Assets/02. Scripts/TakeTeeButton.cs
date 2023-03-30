using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeTeeButton : MonoBehaviour
{
    public Image buttonImage;
    public Sprite baseImage;
    public Sprite clickImage;

    public void PointerUP()
    {
        print("��ư ��");
        buttonImage.sprite = baseImage;
        GameManager.Instance.DanceStart();
    }

    public void PointerDown()
    {
        print("��ư ����");
        buttonImage.sprite = clickImage;
        GameManager.Instance.TakeTee();
    }
}
