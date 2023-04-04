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
                        Debug.Log("전투중입니다.!");
                        break;
                    case BattleState.STOP:
                        Debug.Log("일시중지중입니다.!");
                        break;
                    case BattleState.DEFEAT:
                        {
                            foreach (var item in LiveEnemys)
                            {
                                item.stateContext.SetState(item.idleState);
                            }
                        }
                        Debug.Log("패배했습니다.!");
                        break;
                    case BattleState.WIN:
                        {
                            if (player != null)
                                player.stateContext.SetState(player.idleState);
                        }
                        Debug.Log("승리했습니다.!");
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
                    // 1. 최초 플레이어 컨트롤러 생성
                    if (player == null)
                    {
                        player = factory.CreatePlayer(GameManager.Instance.Player, playerParent);
                    }
                    // 2. 전투 UI 세팅
                    // 3. READY로 이행
                    SetBattleState(BattleState.READY);
                    break;
                case BattleState.READY:
                    // 1. 스테이지 불러오기
                    LoadStage(currentStageID);
                    // 2. 준비 UI 출력
                    // 3. BATTLE로 이행
                    SetBattleState(BattleState.BATTLE);
                    break;
                case BattleState.BATTLE:
                    // 1. 모든 컨트롤러 행동
                    // 2. 모든 전투 UI 행동
                    // 3. 전투 타임 시작
                    break;
                case BattleState.STOP:
                    // 1. 모든 컨트롤러의 행동 중지
                    // 2. 모든 전투 UI 행동 중지
                    // 3. 전투 타임 중지
                    break;
                case BattleState.DEFEAT:
                    // 1. 전투 패배 UI 출력
                    // 2. Userinfo 수정
                    break;
                case BattleState.WIN:
                    // 전투 승리
                    // 1. 다음 스테이지 정보 전달
                    // 2. Userinfo 수정
                    // 3. 플레이어의 상태 정리(디버프 제거 등..)
                    // 4. READY로 이행
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
            // 만약 플레이어가 없다면 팩토리를 통해 플레이어를 생성
            CreateStage(GameManager.Instance.stageDataDic[StageID]);
        }

        public void LoadStage(StageData data)
        {
            CreateStage(data);
        }

        private void CreateStage(StageData data)
        {
            // 만약 플레이어가 없다면 팩토리를 통해 플레이어를 생성
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
            // 다음 스테이지가 있으면 스테이지 출력
            if (GameManager.Instance.stageDataDic.TryGetValue(currentStageID + 1, out data))
            {
                currentStageID += 1;
                LoadStage(data);
            }
            // 없다면 승리
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
        /// 가장 가까운 T를 찾아서 리턴합니다.
        /// </summary>
        /// <typeparam name="T">Controller한정</typeparam>
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