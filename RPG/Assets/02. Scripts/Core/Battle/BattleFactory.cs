using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Character.Equipment;

namespace RPG.Battle.Core
{
    public class BattleFactory : MonoBehaviour
    {
        public PlayerController playerController;
        #region PlayerCreate
        public PlayerController CreatePlayer(PlayerStatus status, Transform parent = null)
        {
            PlayerController controller = Instantiate<PlayerController>(playerController, parent);

            PlayerStatus playerStatus = controller.gameObject.GetComponent<PlayerStatus>();

            BattleStatus battleStatus = controller.gameObject.GetComponent<BattleStatus>();

            PlayerCharacterUI UI = controller.gameObject.GetComponent<PlayerCharacterUI>();

            playerStatus.SetPlayerStatusFromStatus(status, playerStatus.GetComponentInChildren<CharacterAppearance>());
            battleStatus.UpdateStatus();
            SetPlayerUI(ref UI);
            UI.Initialize(battleStatus);
            controller.Initialize();

            return controller;
        }

        public void SetPlayerUI(ref PlayerCharacterUI ui)
        {
            ui.hpBar = BattleManager.GetInstance().playerHPBar;
        } 
        #endregion
    }
}