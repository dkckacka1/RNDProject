using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace RPG.Battle.UI
{
    public class BattleResultWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI floorText;
        [SerializeField] TextMeshProUGUI gainEnergyText;
        [SerializeField] TextMeshProUGUI gainGachaText;
        [SerializeField] TextMeshProUGUI gainReinforceText;
        [SerializeField] TextMeshProUGUI gainIncantText;

        [SerializeField] float scaleSpeedTime;

        private void OnEnable()
        {
            this.gameObject.transform.DOScale(1, scaleSpeedTime).SetEase(Ease.OutBack);
        }

        private void OnDisable()
        {
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }

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