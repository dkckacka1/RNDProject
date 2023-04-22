using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using RPG.Battle.Core;

namespace RPG.Battle.UI
{
    public class BattleResultWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI floorText;
        [SerializeField] TextMeshProUGUI gainEnergyText;
        [SerializeField] TextMeshProUGUI gainGachaText;
        [SerializeField] TextMeshProUGUI gainReinforceText;
        [SerializeField] TextMeshProUGUI gainIncantText;

        [Header("ChangeBattleState")]
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] Button reStartBtn;
        [SerializeField] TextMeshProUGUI btnText;


        [SerializeField] float scaleSpeedTime;

        private void OnEnable()
        {
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
            this.gameObject.transform.DOScale(1, scaleSpeedTime).SetEase(Ease.OutBack);
        }

        public void InitUI(int floor, int gainEnergy, int gainGacha, int gainReinfoce, int gainIncant)
        {
            UpdateUI(floor, gainEnergy, gainGacha, gainReinfoce, gainIncant);
        }

        public void UpdateUI(int floor, int gainEnergy, int gainGacha, int gainReinfoce, int gainIncant)
        {
            floorText.text = $"현재 층 수 : \t{floor}층";
            gainEnergyText.text = gainEnergy.ToString();
            gainGachaText.text = gainGacha.ToString();
            gainReinforceText.text = gainReinfoce.ToString();
            gainIncantText.text = gainIncant.ToString();
        }

        public void ShowDefeatUI()
        {
            titleText.text = "전투 결과";
            btnText.text = "현재 층\n재도전";
            reStartBtn.onClick.RemoveAllListeners();
            reStartBtn.onClick.AddListener(()=> { BattleManager.Instance.ReStartBattle(); });
        }

        public void ShowPauseUI()
        {
            titleText.text = "전투 중지";
            btnText.text = "전투로\n돌아가기";
            reStartBtn.onClick.RemoveAllListeners();
            reStartBtn.onClick.AddListener(() => { BattleManager.Instance.ReturnBattle(); });
        }
    }

}