using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Battle.Core;


public class StageWindowUI : MonoBehaviour
{
    StageData data;

    [SerializeField] TextMeshProUGUI floorText;
    [SerializeField] TextMeshProUGUI isClearText;
    [SerializeField] TextMeshProUGUI isLastText;

    [SerializeField] Button stageBaltteButton;

    public void Init(StageData data, bool isClear, bool isLast)
    {
        this.data = data;
        floorText.text = $"{this.data.ID}Ãþ";

        if(isClear == true)
        {
            isClearText.gameObject.SetActive(true);
        }

        if(isLast == true)
        {
            isLastText.gameObject.SetActive(true);
        }

        if(isClear == false && isLast == false)
        {
            stageBaltteButton.interactable = false;
        }
    }
}
