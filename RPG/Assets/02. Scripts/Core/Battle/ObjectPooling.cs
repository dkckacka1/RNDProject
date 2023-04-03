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
                // 풀이 비어있다면 생성
                EnemyController enemy = CreateController(data, parent);
                InitEnemyController(ref enemy, data, position, parent);
                enemy.transform.position = position;
                enemy.gameObject.SetActive(true);
                return enemy;
            }
            else
            {
                // 풀에 남아있다면 남아있는 컨트롤러 재활용
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
                if (enemy.enemyLooks.name != data.enemyLook.name)
                // 기존에 사용하던 외형과 enemydata의 외형이 동일하지 않다면 새롭게 외형 세팅
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
                // 풀에 있는 것 사용
                BattleText text = battleTextPool.Dequeue();
                text.gameObject.SetActive(true);
                text.SetText(textStr, position);
                return text;
            }
            else
            {
                // 새로 만들어서 풀에 넣기
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