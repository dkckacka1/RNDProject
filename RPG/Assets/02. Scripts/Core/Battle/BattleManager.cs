using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using RPG.Core;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Battle.UI;

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
        public static BattleUI BattleUI { 
            get
            {
                if (instance == null)
                {
                    Debug.Log("BattleManager is NULL");
                    return null;
                }
                return battleUI;
            } 
        }
        public static ObjectPooling ObjectPool
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log("BattleManager is NULL");
                    return null;
                }
                return objectPool;
            }
        }

        [Header("BattleCore")]
        // Component
        public BattleSceneState currentBattleState = BattleSceneState.Default;
        private static BattleUI battleUI;
        private static ObjectPooling objectPool;


        [Header("Controller")]
        public PlayerController livePlayer;
        public List<EnemyController> liveEnemies = new List<EnemyController>();


        [Header("Stage")]
        public int currentStageFloor = 1;
        private StageData stageData;

        private int gainEnergy = 0;
        private int gainGacha = 0;
        private int gainReinforce = 0;
        private int gainIncant = 0;

        private readonly Dictionary<BattleSceneState, UnityEvent> battleEventDic = new Dictionary<BattleSceneState, UnityEvent>();
        private delegate void voidFunc();
        private delegate IEnumerator IEnumeratorFunc();

        [Space()]
        [SerializeField] bool isTest;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                battleUI = GetComponentInChildren<BattleUI>();
                objectPool = GetComponentInChildren<ObjectPooling>();
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            

            ObjectPool.SetUp(BattleUI.battleCanvas);
        }

        private void Start()
        {
            if (GameManager.Instance == null || isTest == true)
            {
                return;
            }

            currentStageFloor = GameManager.Instance.choiceStageID;
            ReadyNextBattle(2f);
        }


        #region ���� �غ�

        private void ReadyNextBattle(float startTime)
        {
            BattleUI.ShowFloor(currentStageFloor);
            LoadCurrentStage();
            BattleUI.ShowReady();
            SetBattleState(BattleSceneState.Ready);
            objectPool.ReleaseAllAbility();
            StartCoroutine(MethodCallTimer(() =>
            {
                BattleUI.ShowStart();
                Battle();
            }, startTime));
        }


        #endregion


        private StageData LoadStageData()
        {
            StageData stage;

            if (GameManager.Instance.stageDataDic.TryGetValue(currentStageFloor, out stage))
            {
                return stage;
            }

            Debug.Log("Stage Data in NULL!");
            return null;
        }

        public void CharacterDead(Controller controller)
        {
            if (controller is PlayerController)
            {
                Defeat();
            }
            else if (controller is EnemyController)
            {
                var enemy = controller as EnemyController;
                // ������ ����
                EnemyData enemyData;
                if (GameManager.Instance.enemyDataDic.TryGetValue((controller.battleStatus.status as EnemyStatus).enemyID, out enemyData))
                {
                    ObjectPool.GetLootingItem(Camera.main.WorldToScreenPoint(controller.transform.position), DropItemType.Energy, BattleUI.backpack.transform);
                    gainItem(DropItemType.Energy, enemyData.dropEnergy);

                    foreach (var dropTable in enemyData.dropitems)
                    {
                        float random = Random.Range(0f, 100f);
                        if (random <= dropTable.percent)
                        {
                            ObjectPool.GetLootingItem(Camera.main.WorldToScreenPoint(controller.transform.position), dropTable.itemType, BattleUI.backpack.transform);
                            switch (dropTable.itemType)
                            {
                                case DropItemType.GachaItemScroll:
                                    gainItem(DropItemType.GachaItemScroll, 1);
                                    break;
                                case DropItemType.reinfoceScroll:
                                    gainItem(DropItemType.reinfoceScroll, 1);
                                    break;
                                case DropItemType.IncantScroll:
                                    gainItem(DropItemType.IncantScroll, 1);
                                    break;
                            }
                        }
                    }
                }

                liveEnemies.Remove(enemy);
                ObjectPool.ReturnEnemy((controller as EnemyController));
                if (liveEnemies.Count <= 0)
                {
                    Win();
                }
            }
        }

        private void gainItem(DropItemType type,int count)
        {
            switch (type)
            {
                case DropItemType.Energy:
                    gainEnergy += count;
                    break;
                case DropItemType.GachaItemScroll:
                    gainGacha += count;
                    break;
                case DropItemType.reinfoceScroll:
                    gainReinforce += count;
                    break;
                case DropItemType.IncantScroll:
                    gainIncant += count;
                    break;
            }
        }

        private void Win()
        {
            // �¸� ����
            currentStageFloor++;
            BattleUI.ShowWin();
            StartCoroutine(MethodCallTimer(() => { ReadyNextBattle(3f); }, 3f));
            SetBattleState(BattleSceneState.Win);
        }

        private void Defeat()
        {
            // �й� ����
            currentBattleState = BattleSceneState.Defeat;
            BattleUI.ShowDefeat();
            SetBattleState(BattleSceneState.Defeat);
        }

        private void Battle()
        {
            SetBattleState(BattleSceneState.Battle);
        }

        private void Pause()
        {
            SetBattleState(BattleSceneState.Pause);
        }

        private void UpdateUserinfo()
        {
            UserInfo userInfo = GameManager.Instance.UserInfo;
            if (userInfo.risingTopCount < currentStageFloor)
            {
                userInfo.risingTopCount = currentStageFloor;
            }

            userInfo.energy += gainEnergy;
            userInfo.itemGachaTicket += gainGacha;
            userInfo.itemReinforceCount += gainReinforce;
            userInfo.itemIncantCount += gainIncant;
        }

        private void ResetStage()
        {
            if (livePlayer != null)
            {
                livePlayer.gameObject.SetActive(false);
            }

            foreach (var enemy in liveEnemies)
            {
                ObjectPool.ReturnEnemy(enemy);
            }

            liveEnemies.Clear();
        }


        public void SetBattleState(BattleSceneState state)
        {
            this.currentBattleState = state;
            Publish(currentBattleState);
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
            // �÷��̾ ���ٸ� ����
            {
                livePlayer = BattleManager.ObjectPool.CreatePlayer(GameManager.Instance.Player);
            }
            else
            // �ִٸ� ��ġ�� ����
            {
                livePlayer.transform.position = stage.playerSpawnPosition;
            }

            // EnemiesSetting
            foreach (var enemySpawnData in stage.enemyDatas)
            {
                EnemyData enemyData;
                if (GameManager.Instance.enemyDataDic.TryGetValue(enemySpawnData.enemyID, out enemyData))
                {
                    EnemyController enemy = ObjectPool.GetEnemyController(enemyData, enemySpawnData.position);
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

        // �̺�Ʈ ����
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

        // �̺�Ʈ ���� ����
        public void UnsubscribeEvent(BattleSceneState state, UnityAction unityAction)
        {
            UnityEvent thisEvent;

            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.RemoveListener(unityAction);
            }
        }

        // �̺�Ʈ Ȱ��ȭ
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
        /// ���� ����� T�� ã�Ƽ� �����մϴ�.
        /// </summary>
        /// <typeparam name="T">Controller����</typeparam>
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

        #region ButtonPlugin

        public void ShowResultUI()
        {
            Pause();
            BattleUI.resultUI.InitUI(currentStageFloor, gainEnergy, gainGacha, gainReinforce, gainIncant);
            BattleUI.ShowResultUI();
        }

        public void ToMainScene()
        {
            UpdateUserinfo();
            ResetStage();
            SceneManager.LoadScene(0);
        }

        public void ReturnBattle()
        {
            Battle();
            BattleUI.ReleaseResultUI();
        }
        #endregion

        #region ������ �ʴ� �Լ� ����

        /// <summary>
        /// �� �Լ��� ������ �Լ��Դϴ�. ������ �ʽ��ϴ�.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public EnemyController ReturnMinimumdistanceEnemy(Transform transform)
        {
            if (liveEnemies.Count == 0)
            {
                print("������ �������� �ʽ��ϴ�.");
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