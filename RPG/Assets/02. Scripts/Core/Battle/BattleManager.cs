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

namespace RPG.Battle.Core
{
    public class BattleManager : MonoBehaviour
    {
        private static BattleManager instance;
        public UserInfo userinfo;
        public PlayerStatus playerStatus;

        public PlayerController player;
        public Transform playerParent;
        public Transform enemyParent;

        [Header("UI")]
        // PlayerUI
        public Canvas battleCanvas;
        public PlayerHPBar playerHPBar;

        [Header("Component")]
        // Component
        public BattleFactory factory;
        public ObjectPooling objectPool;

        [Header("Stage")]
        public int currentStageID;

        private List<EnemyController> liveEnemys = new List<EnemyController>();
        private BattleState currentState;
        private readonly Dictionary<BattleState, UnityEvent> battleEventDic
            = new Dictionary<BattleState, UnityEvent>();

        public bool isTest = false;

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
            get => currentState;
            private set
            {
                currentState = value;
                switch (currentState)
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
                Publish(currentState);
            }
        }

        // Method
        public static BattleManager GetInstance()
        {
            if (instance == null)
            {
                Debug.Log(typeof(BattleManager).Name + "is NULL");
                return null;
            }

            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            //userinfo = GameManager.Instance.UserInfo;
            //currentStageID = GameManager.Instance.choiceStageID;

            //objectPool.SetUp(battleCanvas);
        }

        private void Start()
        {
            userinfo = GameManager.Instance.UserInfo;
            currentStageID = GameManager.Instance.choiceStageID;

            objectPool.SetUp(battleCanvas);

            if (isTest == true)
            {
                return;
            }
            SetBattleState(BattleState.INIT);
        }

        public void SetBattleState(BattleState battleState)
        {

            currentState = battleState;

            switch (battleState)
            {
                case BattleState.INIT:
                    // 1. ���� �÷��̾� ��Ʈ�ѷ� ����
                    if (player == null)
                    {
                        player = factory.CreatePlayer(GameManager.Instance.Player, playerParent);
                    }
                    // 2. ���� UI ����
                    // 3. READY�� ����
                    SetBattleState(BattleState.READY);
                    break;
                case BattleState.READY:
                    // 1. �������� �ҷ�����
                    LoadStage(currentStageID);
                    // 2. �غ� UI ���
                    // 3. BATTLE�� ����
                    SetBattleState(BattleState.BATTLE);
                    break;
                case BattleState.BATTLE:
                    // 1. ��� ��Ʈ�ѷ� �ൿ
                    // 2. ��� ���� UI �ൿ
                    // 3. ���� Ÿ�� ����
                    break;
                case BattleState.STOP:
                    // 1. ��� ��Ʈ�ѷ��� �ൿ ����
                    // 2. ��� ���� UI �ൿ ����
                    // 3. ���� Ÿ�� ����
                    break;
                case BattleState.DEFEAT:
                    // 1. ���� �й� UI ���
                    // 2. Userinfo ����
                    break;
                case BattleState.WIN:
                    // ���� �¸�
                    // 1. ���� �������� ���� ����
                    // 2. Userinfo ����
                    // 3. �÷��̾��� ���� ����(����� ���� ��..)
                    // 4. READY�� ����
                    break;
            }
        }

        public IEnumerator SetBattleState(BattleState battleState, float time)
        {
            yield return new WaitForSeconds(time);
            SetBattleState(battleState);
        }

        public void LoadStage(int StageID)
        {
            // ���� �÷��̾ ���ٸ� ���丮�� ���� �÷��̾ ����
            CreateStage(GameManager.Instance.stageDataDic[StageID]);
        }

        public void LoadStage(StageData data)
        {
            CreateStage(data);
        }

        private void CreateStage(StageData data)
        {
            // ���� �÷��̾ ���ٸ� ���丮�� ���� �÷��̾ ����
            if (player == null)
            {
                player = factory.CreatePlayer(GameManager.Instance.Player, playerParent);
            }
            else
            {
                player.transform.position = data.playerSpawnPosition;
            }

            foreach (var enemy in data.enemyDatas)
            {
                EnemyData enemyData = GameManager.Instance.enemyDataDic[enemy.enemyID];

                EnemyController enemyController = objectPool.GetEnemyController(enemyData, enemy.position, enemyParent);
                liveEnemys.Add(enemyController);

            }
        }

        public void DeadController(PlayerController controller)
        {
            CurrentStats = BattleState.DEFEAT;
        }

        public void DeadController(EnemyController controller)
        {
            objectPool.ReturnEnemy(controller);
            liveEnemys.Remove(controller);
            if (liveEnemys.Count == 0)
            {
                SetBattleState(BattleState.WIN);
            }
        }

        public void WinEvent()
        {
            StageData data;
            // ���� ���������� ������ �������� ���
            if (GameManager.Instance.stageDataDic.TryGetValue(currentStageID + 1, out data))
            {
                currentStageID += 1;
                LoadStage(data);
            }
            // ���ٸ� �¸�
            else
            {
                currentState = BattleState.WIN;
            }
        }

        #region eventListener

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

        #endregion

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