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
        public EnemyController enemyController;
        #region PlayerCreate
        public PlayerController CreatePlayer(PlayerStatus status, Transform parent = null)
        {
            PlayerController controller = Instantiate<PlayerController>(playerController, parent);
            controller.Initialize();

            PlayerStatus playerStatus = controller.gameObject.GetComponent<PlayerStatus>();
            playerStatus.SetPlayerStatusFromStatus(status);

            BattleStatus battleStatus = controller.gameObject.GetComponent<BattleStatus>();
            battleStatus.UpdateStatus();

            PlayerCharacterUI UI = controller.gameObject.GetComponent<PlayerCharacterUI>();
            SetPlayerUI(ref UI);
            UI.Initialize(battleStatus);

            return controller;
        }

        public void SetPlayerUI(ref PlayerCharacterUI ui)
        {
            ui.hpBar = BattleManager.GetInstance().playerHPBar;
        } 
        #endregion

        #region EnemyCreate
        private static int enemyCount = 1;
        public EnemyController CreateEnemy(EnemyData data, Vector3 position, Transform parent = null)
        {
            EnemyController enemy = Instantiate<EnemyController>(enemyController, position, Quaternion.identity, parent);
            enemy.gameObject.name = "고블리나(" + enemyCount++ + ")";
            BattleStatus battleStatus = enemy.GetComponent<BattleStatus>();
            EnemyStatus status = battleStatus.status as EnemyStatus;
            EnemyCharacterUI ui = enemy.GetComponent<EnemyCharacterUI>();

            status.SetStatus(data);

            GameObject looks = Instantiate(data.enemyLook, enemy.gameObject.transform);

            // Enemy Initialize()
            enemy.SetAnimator(looks.GetComponent<Animator>());
            battleStatus.UpdateStatus();
            enemy.Initialize();
            ui.Initialize(battleStatus);

            return enemy;
        } 
        #endregion
    }
}