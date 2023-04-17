using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using RPG.Character.Status;
using RPG.Character.Equipment;

namespace RPG.Main.UI
{
    public class MainUI : MonoBehaviour
    {
        [Header("UI")]
        public EquipmentWindowUI equipmentUI;
        public PlayerStatusWindowUI statusUI;
        public StageChoiceWindowUI stageChoiceWindowUI;
        public CharacterAppearance appearance;

        [Space()]
        [SerializeField] TextMeshProUGUI reinforceText;
        [SerializeField] TextMeshProUGUI incantText;
        [SerializeField] TextMeshProUGUI itemGachaTicketText;
        [SerializeField] TextMeshProUGUI EnergyText;
        [SerializeField] Button backButton;

        [Header("Canvas")]
        [SerializeField] Canvas stageCanvas;
        [SerializeField] Canvas statusCanvas;

        private void Start()
        {
            stageChoiceWindowUI.SetUp();
            Init();
            UpdateUI();
        }

        public void Init()
        {
            equipmentUI.Init();
            statusUI.Init();
            stageChoiceWindowUI.Init();
            appearance.EquipWeapon(GameManager.Instance.Player.currentWeapon.weaponLook);
        }

        public void UpdateUI()
        {
            UpdateTicketCount();
            statusUI.UpdateStatusText();
            statusUI.UpdateUserText();
            equipmentUI.UpdateItem(equipmentUI.choiceItem);
        }

        #region ButtonPlugin
        public void SetActiveFalseUI()
        {
            stageCanvas.gameObject.SetActive(false);
            statusCanvas.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
        }

        public void ShowStatusUI()
        {
            statusCanvas.gameObject.SetActive(true);
            if (backButton == null)
            {
                return;
            }
            backButton.gameObject.SetActive(true);
        }

        public void ShowStageChoiceUI()
        {
            stageCanvas.gameObject.SetActive(true);
            if (backButton == null)
            {
                return;
            }
            backButton.gameObject.SetActive(true);
        }

        public void IncantItem()
        {
            if(GameManager.Instance.UserInfo.itemIncantCount <= 0)
            {
                return;
            }

            GameManager.Instance.UserInfo.itemIncantCount--;

            Incant incant;
            if (RandomSystem.GachaIncant(equipmentUI.choiceItem.equipmentType, GameManager.Instance.incantDic, out incant))
            {
                equipmentUI.choiceItem.Incant(incant);
            }

            equipmentUI.choiceItem.UpdateItem();
            GameManager.Instance.Player.SetEquipment();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            UpdateUI();
        }

        public void ReinforceItem()
        {
            if(GameManager.Instance.UserInfo.itemReinforceCount <= 0)
            {
                return;
            }

            GameManager.Instance.UserInfo.itemReinforceCount--;
            if (MyUtility.ProbailityCalc(100f - (RandomSystem.ReinforceCalc(equipmentUI.choiceItem)),0f,100f))
            {
                equipmentUI.choiceItem.ReinforceItem();
                Debug.Log("강화 성공!!");
            }
            else
            {
                Debug.Log("강화 실패!!");
            }


            equipmentUI.choiceItem.UpdateItem();
            GameManager.Instance.Player.SetEquipment();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            UpdateUI();
        }

        public void GachaItem()
        {
            if (GameManager.Instance.UserInfo.itemGachaTicket <= 0)
            {
                return;
            }

            GameManager.Instance.UserInfo.itemGachaTicket--;
            EquipmentItemType type = equipmentUI.choiceItem.equipmentType;
            switch (type)
            {
                case EquipmentItemType.Weapon:
                    {
                        WeaponData data;
                        if (RandomSystem.GachaRandomData(GameManager.Instance.equipmentDataDic, type, out data))
                        {
                            GameManager.Instance.Player.currentWeapon.ChangeData(data);
                            GameManager.Instance.Player.currentWeapon.UpdateItem();
                        }
                    }

                    break;
                case EquipmentItemType.Armor:
                    {
                        ArmorData data;
                        if (RandomSystem.GachaRandomData(GameManager.Instance.equipmentDataDic, type, out data))
                        {
                            GameManager.Instance.Player.currentArmor.ChangeData(data);
                            GameManager.Instance.Player.currentArmor.UpdateItem();
                        }
                    }
                    break;
                case EquipmentItemType.Pants:
                    {
                        PantsData data;
                        if (RandomSystem.GachaRandomData(GameManager.Instance.equipmentDataDic, type, out data))
                        {
                            GameManager.Instance.Player.currentPants.ChangeData(data);
                            GameManager.Instance.Player.currentPants.UpdateItem();
                        }
                    }
                    break;
                case EquipmentItemType.Helmet:
                    {
                        HelmetData data;
                        if (RandomSystem.GachaRandomData(GameManager.Instance.equipmentDataDic, type, out data))
                        {
                            GameManager.Instance.Player.currentHelmet.ChangeData(data);
                            GameManager.Instance.Player.currentHelmet.UpdateItem();
                        }
                    }
                    break;
            }
            GameManager.Instance.Player.SetEquipment();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            UpdateUI();
        }
        #endregion

        public void UpdateTicketCount()
        {
            if (reinforceText == null)
            {
                return;
            }
            this.itemGachaTicketText.text = $"{GameManager.Instance.UserInfo.itemGachaTicket}";
            this.reinforceText.text = $"{GameManager.Instance.UserInfo.itemReinforceCount}";
            this.incantText.text = $"{GameManager.Instance.UserInfo.itemIncantCount}";
            this.EnergyText.text = $"{GameManager.Instance.UserInfo.energy}";
        }

        // HACK : TEST
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 80, 80), "게임 저장"))
            {
                Debug.Log("게임 저장");
                GameSLManager.SaveToPlayerPrefs(GameManager.Instance.UserInfo);
            }

            if (GUI.Button(new Rect(10, 100, 80, 80), "게임 불러오기"))
            {
                GameManager.Instance.UserInfo = GameSLManager.LoadToPlayerPrefs();
                GameManager.Instance.Player.SetPlayerStatusFromUserinfo(GameManager.Instance.UserInfo);
                Debug.Log(GameManager.Instance.UserInfo);
                UpdateUI();
                stageChoiceWindowUI.Init();
            }

            if (GUI.Button(new Rect(10, 190, 80, 80), "강화권 추가"))
            {
                GameManager.Instance.UserInfo.itemReinforceCount += 100;
                UpdateUI();
            }

            if (GUI.Button(new Rect(10, 280, 80, 80), "인챈트 추가"))
            {
                GameManager.Instance.UserInfo.itemIncantCount += 100;
                UpdateUI();
            }
        }
    }
}