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
            floorText.text = $"���� �� �� : \t{floor}��";
            gainEnergyText.text = gainEnergy.ToString();
            gainGachaText.text = gainGacha.ToString();
            gainReinforceText.text = gainReinfoce.ToString();
            gainIncantText.text = gainIncant.ToString();
        }

        public void ShowDefeatUI()
        {
            titleText.text = "���� ���";
            btnText.text = "���� ��\n�絵��";
            reStartBtn.onClick.RemoveAllListeners();
            reStartBtn.onClick.AddListener(()=> { BattleManager.Instance.ReStartBattle(); });
        }

        public void ShowPauseUI()
        {
            titleText.text = "���� ����";
            btnText.text = "������\n���ư���";
            reStartBtn.onClick.RemoveAllListeners();
            reStartBtn.onClick.AddListener(() => { BattleManager.Instance.ReturnBattle(); });
        }
    }

}