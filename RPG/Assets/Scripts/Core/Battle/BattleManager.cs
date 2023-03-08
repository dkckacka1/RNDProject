using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using RPG.Control;

namespace RPG
{
    public class BattleManager : MonoBehaviour
    {
        private static BattleManager instance;

        public Transform playerParent;
        public Transform enemyParent;
        public Canvas hpBarCanvas;

        public SpawnPosition playerSpawnPosition;
        public SpawnPosition enemySpawnPosition;

        public List<GameObject> playerPrefabs;
        public List<GameObject> enemyPrefabs;

        private List<EnemyController> liveEnemys;
        private List<PlayerController> livePlayers;
        private BattleState currentStats;
        private readonly Dictionary<BattleState, UnityEvent> battleEventDic
            = new Dictionary<BattleState, UnityEvent>();

        // Encapsulation
        public List<EnemyController> LiveEnemys
        {
            get
            {
                if (liveEnemys == null)
                {
                    liveEnemys = new List<EnemyController>();
                }

                return liveEnemys;
            }
        }
        public List<PlayerController> LivePlayers
        {
            get
            {
                if (livePlayers == null)
                {
                    livePlayers = new List<PlayerController>();
                }
                return livePlayers;
            }
        }
        public BattleState CurrentStats
        {
            get => currentStats;
            private set
            {
                currentStats = value;
                switch (currentStats)
                {
                    case BattleState.BATTLE:
                        Debug.Log("전투중입니다.!");
                        break;
                    case BattleState.STOP:
                        Debug.Log("일시중지중입니다.!");
                        break;
                    case BattleState.DEFEAT:
                        {
                            foreach (var item in LiveEnemys)
                            {
                                item.stateContext.SetState(item.idelState);
                            }
                        }
                        Debug.Log("패배했습니다.!");
                        break;
                    case BattleState.WIN:
                        {
                            foreach (var item in LivePlayers)
                            {
                                item.stateContext.SetState(item.idelState);
                            }
                        }
                        Debug.Log("승리했습니다.!");
                        break;
                }
                Publish(currentStats);
            }
        }

        // Method
        public static BattleManager GetInstance()
        {
            if (instance == null)
            {
                Debug.LogError(typeof(BattleManager).Name + "is NULL");
                return null;
            }

            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Start()
        {
            foreach (var item in playerSpawnPosition.spawnPositions)
            {
                Instantiate<GameObject>(playerPrefabs[0], item.position, Quaternion.identity, playerParent);
            }

            foreach (var item in enemySpawnPosition.spawnPositions)
            {
                Instantiate<GameObject>(enemyPrefabs[0], item.position, Quaternion.identity, enemyParent);
            }

            CurrentStats = BattleState.BATTLE;
        }


        public void DeadController(PlayerController controller)
        {
            livePlayers.Remove(controller);
            if (livePlayers.Count == 0)
            {
                CurrentStats = BattleState.DEFEAT;
            }
        }

        public void DeadController(EnemyController controller)
        {
            liveEnemys.Remove(controller);
            if (liveEnemys.Count == 0)
            {
                CurrentStats = BattleState.WIN;
            }
        }

        public void SubscribeEvent(BattleState state, UnityAction listener)
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

        public void UnsubscribeEvent(BattleState state, UnityAction unityAction)
        {
            UnityEvent thisEvent;

            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.RemoveListener(unityAction);
            }
        }

        public void Publish(BattleState state)
        {
            UnityEvent thisEvent;

            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }


        /// <summary>
        /// 가장 가까운 T를 찾아서 리턴합니다.
        /// </summary>
        /// <typeparam name="T">Controller한정</typeparam>
        public T ReturnNearDistanceController<T>(Transform transform) where T : Controller
        {
            List<T> list;

            if (typeof(T) == typeof(PlayerController))
            {
                list = livePlayers as List<T>;
            }
            else if (typeof(T) == typeof(EnemyController))
            {
                list = liveEnemys as List<T>;
            }
            else
            {
                list = null;
                return null;
            }

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
            if (liveEnemys.Count == 0)
            {
                print("적들은 존재하지 않습니다.");
                return null;
            }

            EnemyController minimumDistanceEnemy = liveEnemys[0];

            foreach (EnemyController enemy in liveEnemys)
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
            if (typeof(T) == typeof(PlayerController))
            {
                foreach (var item in livePlayers)
                {
                    item.Target = ReturnNearDistanceController<EnemyController>(item.transform);
                }
            }
            else if (typeof(T) == typeof(EnemyController))
            {
                foreach (var item in liveEnemys)
                {
                    item.Target = ReturnNearDistanceController<PlayerController>(item.transform);
                }
            }
        }

        #endregion
    }

}