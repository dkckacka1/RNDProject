using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Battle.UI
{
    public class BattleResultWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI floorText;
        [SerializeField] TextMeshProUGUI gainEnergyText;
        [SerializeField] TextMeshProUGUI gainGachaText;
        [SerializeField] TextMeshProUGUI gainReinforceText;
        [SerializeField] TextMeshProUGUI gainIncantText;

        public void InitUI(int floor, int gainEnergy, int gainGacha, int gainReinfoce, int gainIncant)
        {
            UpdateUI(floor, gainEnergy, gainGacha, gainReinfoce, gainIncant);
        }

        public void UpdateUI(int floor, int gainEnergy, int gainGacha, int gainReinfoce, int gainIncant)
        {
            floorText.text = $"⑷營 類 熱 : \t{floor}類";
            gainEnergyText.text = gainEnergy.ToString();
            gainGachaText.text = gainGacha.ToString();
            gainReinforceText.text = gainReinfoce.ToString();
            gainIncantText.text = gainIncant.ToString();
        }
    }

}