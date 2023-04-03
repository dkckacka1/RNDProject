using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;
using RPG.Battle.Control;
using RPG.Character.Status;

namespace RPG.Battle.Core
{
    public class ObjectPooling : MonoBehaviour
    {
        public BattleFactory factory;

        [SerializeField] EnemyController enemyController;
        [SerializeField] GameObject battleTextPrefab;

        Canvas battleCanvas;
        public void Init(Canvas canvas)
        {
            this.battleCanvas = canvas;
        }

        #region Enemy
        // Pool
        Queue<EnemyController> enemyControllerPool = new Queue<EnemyController>();

        public EnemyController CreateController(EnemyData data, Transform parent = null)
        {
            EnemyController enemy = Instantiate<EnemyController>(enemyController, parent);
            enemyControllerPool.Enqueue(enemy);
            return enemy;
        }

        public EnemyController GetEnemyController(EnemyData data, Vector3 position, Transform parent = null)
        {
            if (enemyControllerPool.Count <= 0)
            {
                // Ǯ�� ����ִٸ� ����
                EnemyController enemy = CreateController(data, parent);
                InitEnemyController(ref enemy, data, position, parent);
                enemy.transform.position = position;
                enemy.gameObject.SetActive(true);
                return enemy;
            }
            else
            {
                // Ǯ�� �����ִٸ� �����ִ� ��Ʈ�ѷ� ��Ȱ��
                EnemyController enemy = enemyControllerPool.Dequeue();
                InitEnemyController(ref enemy, data, position, parent);
                enemy.transform.position = position;
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }

        public void InitEnemyController(ref EnemyController enemy, EnemyData data, Vector3 position, Transform parent = null)
        {
            BattleStatus battleStatus = enemy.GetComponent<BattleStatus>();
            EnemyStatus status = battleStatus.status as EnemyStatus;
            EnemyCharacterUI ui = enemy.GetComponent<EnemyCharacterUI>();

            status.SetStatus(data);

            // TODO : ���ʹ��� ������ �����ϴٸ� ���� ��Ȱ��
            // �������� �ʴٸ� ������ ���Ӱ� �������־���Ѵ�.
            if (enemy.enemyLooks == null)
            {
                // ������ ���ٸ� ���� ����
                enemy.enemyLooks = Instantiate(data.enemyLook, enemy.gameObject.transform);
                enemy.enemyLooks.name = data.enemyLook.name;
            }
            else
            // ���� ����ϴ� ������ �ִٸ� 
            {
                if (enemy.enemyLooks.name != data.enemyLook.name)
                // ������ ����ϴ� ������ enemydata�� ������ �������� �ʴٸ� ���Ӱ� ���� ����
                {
                    Destroy(enemy.enemyLooks);
                    enemy.enemyLooks = Instantiate(data.enemyLook, enemy.gameObject.transform);
                }
            }

            enemy.SetAnimator(enemy.enemyLooks.GetComponent<Animator>());
            battleStatus.UpdateStatus();
            enemy.Initialize();
            ui.Initialize(battleStatus);
        }

        public void ReturnEnemy(EnemyController enemy)
        {
            enemy.gameObject.SetActive(false);
            enemyControllerPool.Enqueue(enemy);
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
                text.gameObject.SetActive(true);
                text.SetText(textStr, position);
                return text;
            }
            else
            {
                // ���� ���� Ǯ�� �ֱ�
                BattleText text = CreateText();
                battleTextPool.Enqueue(text);
                text.gameObject.SetActive(true);
                text.SetText(textStr, position);
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