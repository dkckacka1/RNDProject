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

        private int gainEnergy = 0;
        private int gainGacha = 0;
        private int gainReinforce = 0;
        private int gainIncant = 0;


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

        }

        private void Start()
        {
            ReadyNextBattle(2f);
        }


        #region ���� �غ�

        private void ReadyNextBattle(float startTime)
        {
            LoadCurrentStage();
            battleUI.ShowReady();
            SetBattleState(BattleSceneState.Ready);
            StartCoroutine(MethodCallTimer(() =>
            {
                battleUI.ShowStart();
                Battle();
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
                Defeat();
            }
            else if (controller is EnemyController)
            {
                var enemy = controller as EnemyController;
                // ������ ����
                EnemyData enemyData;
                if (GameManager.Instance.enemyDataDic.TryGetValue((controller.battleStatus.status as EnemyStatus).enemyID, out enemyData))
                {
                    objectPool.GetLootingItem(Camera.main.WorldToScreenPoint(controller.transform.position), DropItemType.Energy, battleUI.backpack.transform);
                    gainItem(DropItemType.Energy, enemyData.dropEnergy);

                    foreach (var dropTable in enemyData.dropitems)
                    {
                        float random = Random.Range(0f, 100f);
                        if (random <= dropTable.percent)
                        {
                            objectPool.GetLootingItem(Camera.main.WorldToScreenPoint(controller.transform.position), dropTable.itemType, battleUI.backpack.transform);
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
            currentStageID++;
            battleUI.ShowWinText();
            StartCoroutine(MethodCallTimer(() => { ReadyNextBattle(3f); }, 3f));
            SetBattleState(BattleSceneState.Win);
        }

        private void Defeat()
        {
            // �й� ����
            currentBattleState = BattleSceneState.Defeat;
            battleUI.ShowDefeatText();
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
            if (userInfo.risingTopCount < currentStageID)
            {
                userInfo.risingTopCount = currentStageID;
            }

            userInfo.Energy += gainEnergy;
            userInfo.itemGachaTicket += gainGacha;
            userInfo.itemReinforceCount += gainReinforce;
            userInfo.itemIncantCount += gainIncant;
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
                livePlayer = this.objectPool.CreatePlayer(GameManager.Instance.Player);
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
            battleUI.resultUI.InitUI(currentStageID, gainEnergy, gainGacha, gainReinforce, gainIncant);
            battleUI.ShowResultUI();
        }

        public void ToMainScene()
        {
            UpdateUserinfo();
            SceneManager.LoadScene(0);
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