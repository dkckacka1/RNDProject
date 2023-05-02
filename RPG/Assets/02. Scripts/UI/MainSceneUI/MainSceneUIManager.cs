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

        public void ShowUI(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
        }

        public void ReleaseUI(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
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

        // HACK : TEST
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 80, 80), "게임 저장"))
            {
                //GameSLManager.SaveToPlayerPrefs(GameManager.Instance.UserInfo);
                GameSLManager.SaveToJSON(GameManager.Instance.UserInfo,Application.dataPath + @"\Userinfo.json");
            }

            if (GUI.Button(new Rect(10, 100, 80, 80), "게임 불러오기"))
            {
                GameManager.Instance.UserInfo = GameSLManager.LoadFromJson(Application.dataPath + @"\Userinfo.json");
                GameManager.Instance.Player.SetPlayerStatusFromUserinfo(GameManager.Instance.UserInfo);
                UpdateUI();
            }

            if (GUI.Button(new Rect(10, 190, 80, 80), "쿠폰 추가"))
            {
                GameManager.Instance.UserInfo.itemReinforceTicket += 100;
                GameManager.Instance.UserInfo.itemIncantTicket += 100;
                GameManager.Instance.UserInfo.itemGachaTicket += 100;
                UpdateUI();
            }
        }
    }
}