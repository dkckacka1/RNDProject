using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Battle.Core;


namespace RPG.Main.UI
{
    public class StageWindowUI : MonoBehaviour
    {
        StageData data;

        [SerializeField] TextMeshProUGUI floorText;
        [SerializeField] TextMeshProUGUI isClearText;
        [SerializeField] TextMeshProUGUI isLastText;
        [SerializeField] TextMeshProUGUI ConsumeEnergyText;
        [SerializeField] RawImage unClickedImage;

        [SerializeField] Button stageBaltteButton;

        public void Init(StageData data, bool isClear, bool isLast)
        {
            this.data = data;
            floorText.text = $"{this.data.ID}Ãþ";

            ConsumeEnergyText.text = $"-{data.ConsumEnergy}";

            if (isClear == true)
            {
                isClearText.gameObject.SetActive(true);
            }

            if (isLast == true)
            {
                isLastText.gameObject.SetActive(true);
            }

            if (isClear == false && isLast == false)
            {
                stageBaltteButton.interactable = false;
                unClickedImage.gameObject.SetActive(true);
            }
        }
    }

}