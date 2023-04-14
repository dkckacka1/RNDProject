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
using DG.Tweening;

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
        public static BattleUI BattleUI
        {
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
        public float battleReadyTime = 2f;
        public float playerCreatePositionXOffset = 15f;
        public float EnemyCreatePositionXOffset = -18f;

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
            ReadyNextBattle();
        }


        #region 전투 준비

        private void ReadyNextBattle()
        {
            BattleUI.ShowFloor(currentStageFloor);
            LoadCurrentStage();
            SetBattleState(BattleSceneState.Ready);
            objectPool.ReleaseAllAbility();
            StartCoroutine(MethodCallTimer(() =>
            {
                Battle();
            }, battleReadyTime + 3f));
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
                liveEnemies.Remove(enemy);
                ObjectPool.ReturnEnemy((controller as EnemyController));
                if (liveEnemies.Count <= 0)
                {
                    Win();
                }
            }
        }

        public void LootingItem(EnemyController enemy)
        {
            EnemyData enemyData;
            if (GameManager.Instance.enemyDataDic.TryGetValue((enemy.battleStatus.status as EnemyStatus).enemyID, out enemyData))
            {
                ObjectPool.GetLootingItem(Camera.main.WorldToScreenPoint(enemy.transform.position), DropItemType.Energy, BattleUI.backpack.transform);
                gainItem(DropItemType.Energy, enemyData.dropEnergy);

                foreach (var dropTable in enemyData.dropitems)
                {
                    float random = Random.Range(0f, 100f);
                    if (random <= dropTable.percent)
                    {
                        ObjectPool.GetLootingItem(Camera.main.WorldToScreenPoint(enemy.transform.position), dropTable.itemType, BattleUI.backpack.transform);
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
        }

        private void gainItem(DropItemType type, int count)
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
        #region BattleSceneStateEvent

        private void Win()
        {
            // 승리 연출
            SetBattleState(BattleSceneState.Win);
            currentStageFloor++;
            //BattleUI.ShowWin();
            livePlayer.transform.LookAt(livePlayer.transform.position + Vector3.left);
            livePlayer.animator.SetTrigger("Move");
            livePlayer.transform.DOMoveX(EnemyCreatePositionXOffset, battleReadyTime).OnComplete(() => { ReadyNextBattle(); });
        }

        private void Defeat()
        {
            // 패배 연출
            currentBattleState = BattleSceneState.Defeat;
            BattleUI.ShowDefeat();
            SetBattleState(BattleSceneState.Defeat);
            BattleUI.resultUI.InitUI(currentStageFloor, gainEnergy, gainGacha, gainReinforce, gainIncant);
            BattleUI.ShowResultUI(BattleSceneState.Defeat);
        }

        private void Battle()
        {
            SetBattleState(BattleSceneState.Battle);
        }

        private void Pause()
        {
            SetBattleState(BattleSceneState.Pause);
        }

        #endregion
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
            // 플레이어가 없다면 생성
            {
                livePlayer = BattleManager.ObjectPool.CreatePlayer(GameManager.Instance.Player);
                livePlayer.battleStatus.currentState = CombatState.Actable;
            }

            Vector3 playerPosition = new Vector3(playerCreatePositionXOffset, stage.playerSpawnPosition.y, stage.playerSpawnPosition.z);
            (livePlayer.battleStatus.status as PlayerStatus).SetPlayerDefaultStatus(GameManager.Instance.Player);
            livePlayer.battleStatus.UpdateBehaviour();
            livePlayer.transform.position = playerPosition;
            livePlayer.transform.LookAt(livePlayer.transform.position + Vector3.left);
            livePlayer.animator.SetTrigger("Move");
            livePlayer.transform.DOMoveX(stage.playerSpawnPosition.x, battleReadyTime).OnComplete(() => 
            {
                livePlayer.animator.ResetTrigger("Move");
                livePlayer.animator.SetTrigger("Idle");
            });

            // EnemiesSetting
            foreach (var enemySpawnData in stage.enemyDatas)
            {
                EnemyData enemyData;
                if (GameManager.Instance.enemyDataDic.TryGetValue(enemySpawnData.enemyID, out enemyData))
                {
                    Vector3 enemyPosition = new Vector3(EnemyCreatePositionXOffset, enemySpawnData.position.y, enemySpawnData.position.z);
                    EnemyController enemy = ObjectPool.GetEnemyController(enemyData, enemyPosition);
                    enemy.transform.LookAt(enemy.transform.position + Vector3.right);
                    enemy.animator.SetTrigger("Move");
                    enemy.transform.DOMoveX(enemySpawnData.position.x, battleReadyTime).OnComplete(() => 
                    {
                        enemy.animator.ResetTrigger("Move");
                        enemy.animator.SetTrigger("Idle");
                    });
                    enemy.battleStatus.currentState = CombatState.Actable;
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
                List<EnemyController> list = liveEnemies.Where(enemy => !enemy.battleStatus.isDead).ToList();
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

            return null;
        }

        #region ButtonPlugin

        public void ShowResultUI()
        {
            Pause();
            BattleUI.resultUI.InitUI(currentStageFloor, gainEnergy, gainGacha, gainReinforce, gainIncant);
            BattleUI.ShowResultUI(BattleSceneState.Pause);
        }

        public void ToMainScene()
        {
            UpdateUserinfo();
            ResetStage();
            SceneLoader.LoadMainScene();
        }

        public void ReStartBattle()
        {
            ResetStage();
            SceneLoader.LoadBattleScene(currentStageFloor);
        }

        public void ReturnBattle()
        {
            Battle();
            BattleUI.ReleaseResultUI();
        }
        #endregion

        private void OnGUI()
        {

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