using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using RPG.Core;
using RPG.Battle.Control;

namespace RPG.Battle.Core
{
    public class BattleManager : MonoBehaviour
    {
        private static BattleManager instance;
        public UserInfo userinfo = new UserInfo();

        public Transform playerParent;
        public Transform enemyParent;
        public Canvas battleCanvas;

        // Component
        public BattleFactory factory;

        public int currentStageID = 1;

        public PlayerController player;
        private List<EnemyController> liveEnemys = new List<EnemyController>();
        // TODO : �÷��̾�� �ϳ��̱� ������ ���� �ʿ�
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

        public BattleState CurrentStats
        {
            get => currentStats;
            private set
            {
                currentStats = value;
                switch (currentStats)
                {
                    case BattleState.BATTLE:
                        Debug.Log("�������Դϴ�.!");
                        break;
                    case BattleState.STOP:
                        Debug.Log("�Ͻ��������Դϴ�.!");
                        break;
                    case BattleState.DEFEAT:
                        {
                            foreach (var item in LiveEnemys)
                            {
                                item.stateContext.SetState(item.idleState);
                            }
                        }
                        Debug.Log("�й��߽��ϴ�.!");
                        break;
                    case BattleState.WIN:
                        {
                            if (player != null)
                                player.stateContext.SetState(player.idleState);
                        }
                        Debug.Log("�¸��߽��ϴ�.!");
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
            userinfo = GameManager.Instance.userInfo;
            LoadStage(currentStageID);
            CurrentStats = BattleState.BATTLE;
        }

        //public IEnumerator Test()
        //{
        //    while(true)
        //    {
        //        yield return new WaitForSeconds(1f);
        //        print($"����ִ� �� : {liveEnemys.Count}");
        //        print($"����ִ� �÷��̾� : {livePlayers.Count}");
        //    }
        //}

        public void LoadStage(int StageID)
        {
            StageData data = GameManager.Instance.stageDataDic[StageID];

            // ���� �÷��̾ ���ٸ� ���丮�� ���� �÷��̾ ����
            if (player == null)
            {
                player = factory.CreatePlayer(userinfo, data.playerSpawnPosition, playerParent);
            }
            else
            {
                player.transform.position = data.playerSpawnPosition;
            }

            // ���丮�� ���� �� ����
            foreach (var enemy in data.enemyDatas)
            {
                EnemyData enemyData = GameManager.Instance.enemyDataDic[enemy.enemyID];

                factory.CreateEnemy(enemyData, enemy.position, enemyParent);
            }
        }

        public void LoadStage(StageData stage)
        {
            // ���� �÷��̾ ���ٸ� ���丮�� ���� �÷��̾ ����
            if (player == null)
            {
                player = factory.CreatePlayer(userinfo, stage.playerSpawnPosition, playerParent);
            }
            else
            {
                player.transform.position = stage.playerSpawnPosition;
            }

            // ���丮�� ���� �� ����
            foreach (var enemy in stage.enemyDatas)
            {
                EnemyData enemyData = GameManager.Instance.enemyDataDic[enemy.enemyID];

                factory.CreateEnemy(enemyData, enemy.position, enemyParent);
            }
        }


        public void DeadController(PlayerController controller)
        {
            CurrentStats = BattleState.DEFEAT;
        }

        public void DeadController(EnemyController controller)
        {
            liveEnemys.Remove(controller);
            if (liveEnemys.Count == 0)
            {
                WinEvent();
            }
        }

        public void WinEvent()
        {
            StageData data;
            // ���� ���������� ������ �������� ���
            if (GameManager.Instance.stageDataDic.TryGetValue(currentStageID + 1, out data))
            {
                currentStageID += 1;
            Debug.Log("������������ ���!");
                LoadStage(data);
            }
            // ���ٸ� �¸�
            else
            {
            Debug.Log("������������ ����!");
                currentStats = BattleState.WIN;
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
        /// ���� ����� T�� ã�Ƽ� �����մϴ�.
        /// </summary>
        /// <typeparam name="T">Controller����</typeparam>
        public T ReturnNearDistanceController<T>(Transform transform) where T : Controller
        {
            if (typeof(T) == typeof(PlayerController))
            {
                if (player != null)
                {
                    return player as T;
                }
            }
            else if (typeof(T) == typeof(EnemyController))
            {
            }
            else
            {
                return null;
            }

            List<T> list = liveEnemys as List<T>;
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

            print("���� Ÿ���� = " + nearTarget.name);

            return (T)nearTarget;
        }

        #region ������ �ʴ� �Լ� ����

        /// <summary>
        /// �� �Լ��� ������ �Լ��Դϴ�. ������ �ʽ��ϴ�.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public EnemyController ReturnMinimumdistanceEnemy(Transform transform)
        {
            if (liveEnemys.Count == 0)
            {
                print("������ �������� �ʽ��ϴ�.");
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
        /// ������ �ʴ� �Լ� �Դϴ�.
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