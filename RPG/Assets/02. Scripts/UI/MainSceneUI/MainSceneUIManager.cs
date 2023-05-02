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
    public class MainSceneUIManager : MonoBehaviour
    {
        [Header("UI")]
        public CharacterAppearance appearance;

        [Space()]
        [SerializeField] TextMeshProUGUI reinforceText;
        [SerializeField] TextMeshProUGUI incantText;
        [SerializeField] TextMeshProUGUI itemGachaTicketText;
        [SerializeField] TextMeshProUGUI EnergyText;

        [Header("Canvas")]
        [SerializeField] Canvas statusCanvas;

        private void Start()
        {
            Init();
            UpdateUI();
        }

        public void Init()
        {
            appearance.EquipWeapon(GameManager.Instance.Player.currentWeapon.weaponApparenceID, GameManager.Instance.Player.currentWeapon.handleType);
        }

        public void UpdateUI()
        {
            UpdateTicketCount();
        }

        #region ButtonPlugin
        public void SetActiveFalseUI()
        {
            statusCanvas.gameObject.SetActive(false);
        }

        public void ShowStatusUI()
        {
            statusCanvas.gameObject.SetActive(true);
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
            if (GUI.Button(new Rect(10, 10, 80, 80), "���� ����"))
            {
                //GameSLManager.SaveToPlayerPrefs(GameManager.Instance.UserInfo);
                GameSLManager.SaveToJSON(GameManager.Instance.UserInfo,Application.dataPath + @"\Userinfo.json");
            }

            if (GUI.Button(new Rect(10, 100, 80, 80), "���� �ҷ�����"))
            {
                GameManager.Instance.UserInfo = GameSLManager.LoadFromJson(Application.dataPath + @"\Userinfo.json");
                GameManager.Instance.Player.SetPlayerStatusFromUserinfo(GameManager.Instance.UserInfo);
                UpdateUI();
            }

            if (GUI.Button(new Rect(10, 190, 80, 80), "��ȭ�� �߰�"))
            {
                GameManager.Instance.UserInfo.itemReinforceTicket += 100;
                UpdateUI();
            }

            if (GUI.Button(new Rect(10, 280, 80, 80), "��æƮ �߰�"))
            {
                GameManager.Instance.UserInfo.itemIncantTicket += 100;
                UpdateUI();
            }
        }
    }
}