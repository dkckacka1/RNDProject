using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeTeeButton : MonoBehaviour
{
    public void PointerUP()
    {
        print("��ư ��");
        GameManager.Instance.DanceStart();
    }

    public void PointerDown()
    {
        print("��ư ����");
        GameManager.Instance.TakeTee();
    }
}
