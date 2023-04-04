using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;
using RPG.Battle.Control;
using RPG.Character.Status;
using UnityEditor;

namespace RPG.Battle.Core
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] EnemyController enemyController;
        [SerializeField] GameObject battleTextPrefab;
        public Transform playerParent;
        public Transform enemyParent;

        Canvas battleCanvas;
        public void SetUp(Canvas canvas)
        {
            this.battleCanvas = canvas;
        }

        public PlayerController playerController;
        public PlayerController CreatePlayer(PlayerStatus status)
        {
            PlayerController controller = Instantiate<PlayerController>(playerController, playerParent);
            (controller.status.status as PlayerStatus).SetPlayerStatusFromStatus(status);
            controller.gameObject.SetActive(true);

            return controller;
        }

        #region Enemy
        // Pool
        Queue<EnemyController> enemyControllerPool = new Queue<EnemyController>();
        static int count = 1;

        private EnemyController CreateController(EnemyData data)
        {
            EnemyController enemy = Instantiate<EnemyController>(enemyController, enemyParent);
            enemy.gameObject.name = "����� " + count++;
            (enemy.status.status as EnemyStatus).Init(data);
            return enemy;
        }

        public EnemyController GetEnemyController(EnemyData data, Vector3 position)
        {
            EnemyController enemy;
            if (enemyControllerPool.Count > 0)
            {
                // Ǯ�� �����ִٸ� �����ִ� ��Ʈ�ѷ� ��Ȱ��
                enemy = enemyControllerPool.Dequeue();
                SetLook(ref enemy, data);
                enemy.gameObject.transform.position = position;
                enemy.gameObject.SetActive(true);
            }
            else
            {                   
                // Ǯ�� ����ִٸ� ����
                enemy = CreateController(data);
                SetLook(ref enemy, data);
                enemy.gameObject.transform.position = position;
                enemy.gameObject.SetActive(true);
            }
            return enemy;
        }

        public void SetLook(ref EnemyController enemy,EnemyData data)
        {
            if(enemy.enemyLooks == null)
                enemy.enemyLooks = Instantiate(data.enemyLook,enemy.gameObject.transform);
        }


        public void ReturnEnemy(EnemyController enemy)
        {
            enemyControllerPool.Enqueue(enemy);
            enemy.gameObject.SetActive(false);
        }

        #endregion

        #region BattleText

        // Pool
        Queue<BattleText> battleTextPool = new Queue<BattleText>();


        public BattleText CreateText()
        {
            GameObject obj = Instantiate(battleTextPrefab, battleCanvas.gameObject.transform);
            BattleText text = obj.GetComponent<BattleText>();
            return text;
        }

        public BattleText GetText(string textStr, Vector3 position)
        {
            if (battleTextPool.Count > 0)
            {
                // Ǯ�� �ִ� �� ���
                BattleText text = battleTextPool.Dequeue();
                text.Init(textStr, position);
                text.gameObject.SetActive(true);
                return text;
            }
            else
            {
                // ���� ���� Ǯ�� �ֱ�
                BattleText text = CreateText();
                battleTextPool.Enqueue(text);
                text.Init(textStr, position);
                text.gameObject.SetActive(true);
                return text;
            }
        }

        public void ReturnText(BattleText text)
        {
            text.gameObject.SetActive(false);
            battleTextPool.Enqueue(text);
        }
        #endregion
    }
}