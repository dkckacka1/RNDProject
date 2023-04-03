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
                // 풀이 비어있다면 생성
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
                // 풀에 남아있다면 남아있는 컨트롤러 재활용
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

            // TODO : 에너미의 외형이 동일하다면 외형 재활용
            // 동일하지 않다면 외형을 새롭게 설정해주어야한다.
            if (enemy.enemyLooks == null)
            {
                // 외형이 없다면 외형 설정
                enemy.enemyLooks = Instantiate(data.enemyLook, enemy.gameObject.transform);
                enemy.enemyLooks.name = data.enemyLook.name;
            }
            else
            // 기존 사용하던 외형이 있다면 
            {
                Debug.Log(enemy.enemyLooks.name + " : " + data.enemyLook.name + " = " + (enemy.enemyLooks.name == data.enemyLook.name));
                if ((enemy.enemyLooks.name == data.enemyLook.name) == false)
                // 기존에 사용하던 외형과 enemydata의 외형이 동일하지 않다면 새롭게 외형 세팅
                {
                    Debug.Log("외형 재세팅!");
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
                // 풀에 있는 것 사용
                BattleText text = battleTextPool.Dequeue();
                text.Init(textStr, position);
                text.gameObject.SetActive(true);
                return text;
            }
            else
            {
                // 새로 만들어서 풀에 넣기
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