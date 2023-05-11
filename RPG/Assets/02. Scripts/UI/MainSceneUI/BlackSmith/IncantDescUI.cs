using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Character.Equipment;
using UnityEngine.UI;

namespace RPG.Main.UI.BlackSmith
{
    public class IncantDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameTxt;
        [SerializeField] TextMeshProUGUI addDescTxt;
        [SerializeField] TextMeshProUGUI minusDescTxt;

        public void ShowIncant(Incant incant)
        {
            if (incant == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            nameTxt.text = incant.incantName;

            string addDesc = incant.GetAddDesc();

            if (addDesc == "")
            {
                addDescTxt.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                addDescTxt.text = addDesc;
                addDescTxt.transform.parent.gameObject.SetActive(true);
            }

            string minusDesc = incant.GetMinusDesc();

            if (minusDesc == "")
            {
                minusDescTxt.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                minusDescTxt.text = minusDesc;
                minusDescTxt.transform.parent.gameObject.SetActive(true);
            }

            for (int i = 0; i < this.transform.childCount; i++)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.GetChild(i));
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);

            this.gameObject.SetActive(true);
        }
    }
}

