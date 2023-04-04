using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using RPG.Core;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Battle.UI;
using System;

namespace RPG.Battle.Core
{
    public class BattleManager : MonoBehaviour
    {
        // Singletone
        public static BattleManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log("BattleManager is NULL");
                    return null;
                }

                return instance;
            }
        }
        private static BattleManager instance;

        [Header("BattleCore")]
        // Component
        public BattleUI battleUI;
        public ObjectPooling objectPool;
        public BattleSceneState currentBattleState = BattleSceneState.Default;

        [Header("Controller")]
        public PlayerController livePlayer;
        public List<EnemyController> liveEnemies = new List<EnemyController>();


        [Header("Stage")]
        public int currentStageID = 0;
        private StageData stageData;

        private readonly Dictionary<BattleSceneState, UnityEvent> battleEventDic = new Dictionary<BattleSceneState, UnityEvent>();
        private delegate void voidFunc();
        private delegate IEnumerator IEnumeratorFunc();

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this.gameObject);
                return;
            }

            objectPool.SetUp(battleUI.battleCanvas);

            SubscribeEvent(BattleSceneState.Win, Win);
            SubscribeEvent(BattleSceneState.Defeat, Defeat);
        }

        private void Start()
        {
            ReadyNextBattle(2f);
        }

        public void SetBattleState(BattleSceneState state)
        {
            this.currentBattleState = state;
            Publish(currentBattleState);
        }

        #region 전투 준비

        private void ReadyNextBattle(float startTime)
        {
            LoadCurrentStage();
            battleUI.ShowReady();
            StartCoroutine(MethodCallTimer(() =>
            {
                battleUI.ShowStart();
                currentBattleState = BattleSceneState.Battle;
            }, startTime));
        }


        #endregion


        private StageData LoadStageData()
        {
            StageData stage;

            if (GameManager.Instance.stageDataDic.TryGetValue(currentStageID, out stage))
            {
                return stage;
            }

            return null;
        }

        public void CharacterDead(Controller controller)
        {
            if (controller is PlayerController)
            {
                SetBattleState(BattleSceneState.Defeat);
            }
            else if (controller is EnemyController)
            {
                var enemy = controller as EnemyController;
                liveEnemies.Remove(enemy);
                if (liveEnemies.Count <= 0)
                {
                    SetBattleState(BattleSceneState.Win);
                }
                StartCoroutine(MethodCallTimer(() =>
                {
                    objectPool.ReturnEnemy(enemy);
                }, 1f));
            }
        }

        private void Win()
        {
            // 승리 연출
            currentBattleState = BattleSceneState.Win;
            battleUI.ShowWinText();
            StartCoroutine(MethodCallTimer(() => { ReadyNextBattle(3f); }, 3f));
        }

        private void Defeat()
        {
            // 패배 연출
            currentBattleState = BattleSceneState.Defeat;
            battleUI.ShowDefeatText();
        }

        #region LoadStage

        private void LoadCurrentStage()
        {
            stageData = LoadStageData();
            SetUpStage(ref stageData);
        }

        private void SetUpStage(ref StageData stage)
        {
            // PlayerSetting
            if (livePlayer == null)
            // 플레이어가 없다면 생성
            {
                livePlayer = this.objectPool.CreatePlayer(GameManager.Instance.Player);
            }
            else
            // 있다면 위치값 세팅
            {
                livePlayer.transform.position = stage.playerSpawnPosition;
            }

            // EnemiesSetting
            foreach (var enemySpawnData in stage.enemyDatas)
            {
                EnemyData enemyData;
                if (GameManager.Instance.enemyDataDic.TryGetValue(enemySpawnData.enemyID, out enemyData))
                {
                    EnemyController enemy = objectPool.GetEnemyController(enemyData, enemySpawnData.position);
                    liveEnemies.Add(enemy);
                }
            }
        }
        #endregion

        private IEnumerator MethodCallTimer(voidFunc func, float duration)
        {
            yield return new WaitForSeconds(duration);
            func.Invoke();
        }

        #region eventListener

        // 이벤트 구독
        public void SubscribeEvent(BattleSceneState state, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                battleEventDic.Add(state, thisEvent);
            }
        }

        // 이벤트 구독 해제
        public void UnsubscribeEvent(BattleSceneState state, UnityAction unityAction)
        {
            UnityEvent thisEvent;

            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.RemoveListener(unityAction);
            }
        }

        // 이벤트 활성화
        public void Publish(BattleSceneState state)
        {
            UnityEvent thisEvent;

            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        #endregion

        /// <summary>
        /// 가장 가까운 T를 찾아서 리턴합니다.
        /// </summary>
        /// <typeparam name="T">Controller한정</typeparam>
        public T ReturnNearDistanceController<T>(Transform transform) where T : Controller
        {
            if (typeof(T) == typeof(PlayerController))
            {
                if (livePlayer != null)
                {
                    return livePlayer as T;
                }
            }
            else if (typeof(T) == typeof(EnemyController))
            {
            }
            else
            {
                return null;
            }

            List<T> list = liveEnemies as List<T>;
            if (list.Count == 0) return null;

            Controller nearTarget = list[0];
            float distance = Vector3.Distance(nearTarget.transform.position, transform.position);

            for (int i = 1; i < list.Count; i++)
            {
                float newDistance = Vector3.Distance(list[i].transform.position, transform.position);

                if (distance > newDistance)
                {
                    nearTarget = list[i];
                    distance = newDistance;
                }
            }

            return (T)nearTarget;
        }

        #region 사용되지 않는 함수 모음

        /// <summary>
        /// 이 함수는 오래된 함수입니다. 사용되지 않습니다.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public EnemyController ReturnMinimumdistanceEnemy(Transform transform)
        {
            if (liveEnemies.Count == 0)
            {
                print("적들은 존재하지 않습니다.");
                return null;
            }

            EnemyController minimumDistanceEnemy = liveEnemies[0];

            foreach (EnemyController enemy in liveEnemies)
            {
                float distacne = Vector3.Distance(transform.position, minimumDistanceEnemy.transform.position);
                float newDistance = Vector3.Distance(transform.position, enemy.transform.position);

                if (newDistance < distacne)
                {
                    minimumDistanceEnemy = enemy;
                }
            }

            return minimumDistanceEnemy;
        }


        /// <summary>
        /// 사용되지 않는 함수 입니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controllerMyself"></param>
        public void MoveToNextPhase<T>(T controllerMyself) where T : Controller
        {
            //if (typeof(T) == typeof(PlayerController))
            //{
            //    foreach (var item in livePlayers)
            //    {
            //        item.target = ReturnNearDistanceController<EnemyController>(item.transform);
            //    }
            //}
            //else if (typeof(T) == typeof(EnemyController))
            //{
            //    foreach (var item in liveEnemys)
            //    {
            //        item.target = ReturnNearDistanceController<PlayerController>(item.transform);
            //    }
            //}
        }

        #endregion
    }

}