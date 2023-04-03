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
            (controller.status.status as PlayerStatus).SetPlayerStatusFromStatus(status);
            controller.gameObject.SetActive(true);

            return controller;
        }
        #endregion
    }
}