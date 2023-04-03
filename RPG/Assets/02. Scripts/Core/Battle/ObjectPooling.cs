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
        public BattleFactory factory;

        [SerializeField] EnemyController enemyController;
        [SerializeField] GameObject battleTextPrefab;

        Canvas battleCanvas;
        public void SetUp(Canvas canvas)
        {
            this.battleCanvas = canvas;
        }

        #region Enemy
        // Pool
        Queue<EnemyController> enemyControllerPool = new Queue<EnemyController>();

        public EnemyController CreateController(EnemyData data, Transform parent = null)
        {
            EnemyController enemy = Instantiate<EnemyController>(enemyController, parent);
            EnemyCharacterUI enemyUI = enemy.GetComponent<EnemyCharacterUI>();
            enemy.SetUp();
            enemyUI.SetUP(enemy.GetComponent<BattleStatus>());
            return enemy;
        }

        public EnemyController GetEnemyController(EnemyData data, Vector3 position, Transform parent = null)
        {
            if (enemyControllerPool.Count <= 0)
            {
                // Ǯ�� ����ִٸ� ����
                EnemyController enemy = CreateController(data, parent);
                InitEnemyController(ref enemy, data, parent);
                enemy.gameObject.transform.position = position;
                enemy.gameObject.SetActive(true);
                print(enemy.gameObject.transform.name + " : " + enemy.gameObject.transform.position);
                EditorApplication.isPaused = true;
                return enemy;
            }
            else
            {
                // Ǯ�� �����ִٸ� �����ִ� ��Ʈ�ѷ� ��Ȱ��
                EnemyController enemy = enemyControllerPool.Dequeue();
                InitEnemyController(ref enemy, data, parent);
                enemy.gameObject.transform.position = position;
                enemy.gameObject.SetActive(true);
                EditorApplication.isPaused = true;
                return enemy;
            }
        }

        public void InitEnemyController(ref EnemyController enemy, EnemyData data, Transform parent = null)
        {
            BattleStatus battleStatus = enemy.GetComponent<BattleStatus>();
            EnemyStatus status = battleStatus.status as EnemyStatus;
            EnemyCharacterUI enemyUI = enemy.GetComponent<EnemyCharacterUI>();

            status.Init(data);

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
                Debug.Log(enemy.enemyLooks.name + " : " + data.enemyLook.name + " = " + (enemy.enemyLooks.name == data.enemyLook.name));
                if ((enemy.enemyLooks.name == data.enemyLook.name) == false)
                // ������ ����ϴ� ������ enemydata�� ������ �������� �ʴٸ� ���Ӱ� ���� ����
                {
                    Debug.Log("���� �缼��!");
                    Destroy(enemy.enemyLooks);
                    enemy.enemyLooks = Instantiate(data.enemyLook, enemy.gameObject.transform);
                }
            }

            battleStatus.Init();
            enemyUI.Init();
            enemy.Init(enemy.enemyLooks.GetComponent<Animator>());
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