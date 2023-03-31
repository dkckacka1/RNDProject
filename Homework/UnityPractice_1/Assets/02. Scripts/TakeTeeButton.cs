using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeTeeButton : MonoBehaviour
{
    public void PointerUP()
    {
        print("버튼 땜");
        GameManager.Instance.DanceStart();
    }

    public void PointerDown()
    {
        print("버튼 누름");
        GameManager.Instance.TakeTee();
    }
}
