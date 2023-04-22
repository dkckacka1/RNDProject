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
            appearance.EquipWeapon(GameManager.Instance.Player.currentWeapon.weaponApparenceID);
        }

        public void UpdateUI()
        {
            UpdateTicketCount();
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
            if(GameManager.Instance.UserInfo.itemIncantTicket <= 0)
            {
                return;
            }

            GameManager.Instance.UserInfo.itemIncantTicket--;
            
            GameManager.Instance.Player.SetEquipment();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            UpdateUI();
        }

        public void ReinforceItem()
        {
            if(GameManager.Instance.UserInfo.itemReinforceTicket <= 0)
            {
                return;
            }

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
            
            GameManager.Instance.Player.SetEquipment();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            UpdateUI();
        }
       
        public void LoadStageChoiceScene()
        {
            SceneLoader.LoadStageChoiceScene();
        }
        #endregion

        public void UpdateTicketCount()
        {
            if (reinforceText == null)
            {
                return;
            }
            this.itemGachaTicketText.text = $"{GameManager.Instance.UserInfo.itemGachaTicket}";
            this.reinforceText.text = $"{GameManager.Instance.UserInfo.itemReinforceTicket}";
            this.incantText.text = $"{GameManager.Instance.UserInfo.itemIncantTicket}";
            this.EnergyText.text = $"{GameManager.Instance.UserInfo.energy}";
        }

        public void ShowBlackSmithUI(Canvas blackSmithCanvas)
        {
            blackSmithCanvas.gameObject.SetActive(true);
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
                GameManager.Instance.UserInfo.itemReinforceTicket += 100;
                UpdateUI();
            }

            if (GUI.Button(new Rect(10, 280, 80, 80), "인챈트 추가"))
            {
                GameManager.Instance.UserInfo.itemIncantTicket += 100;
                UpdateUI();
            }
        }
    }
}