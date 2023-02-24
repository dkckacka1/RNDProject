using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Control;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;

    private List<EnemyController> liveEnemys;
    private List<PlayerController> livePlayers;

    // TEST
    public List<Controller> lives = new List<Controller>();

    private BattleState currentStats;

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
        set
        {
            liveEnemys = value;
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
        set
        {
            livePlayers = value;
        }
    }

    public BattleState CurrentStats
    {
        get => currentStats;
        private set
        {
            switch (value)
            {
                case BattleState.BATTLE:
                    Debug.Log("�������Դϴ�.!");
                    break;
                case BattleState.STOP:
                    Debug.Log("�Ͻ��������Դϴ�.!");
                    break;
                case BattleState.DEFEAT:
                    Debug.Log("�й��߽��ϴ�.!");
                    break;
                case BattleState.WIN:
                    Debug.Log("�¸��߽��ϴ�.!");
                    break;
            }
            currentStats = value;
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
        CurrentStats = BattleState.BATTLE;
    }

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

    /// <summary>
    /// ���� ����� T�� ã�Ƽ� �����մϴ�.
    /// </summary>
    /// <typeparam name="T">Controller����</typeparam>
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

    // TODO : LINQ�� ���Ͽ� T ���� ���ϱ�
    //public T ReturnNearDistanceController2<T>(Transform transform) where T : Controller
    //{
    //    var list =
    //        from controller in lives where (typeof(T) == controller.GetType())
    //        select controller;

    //    return (T)list[0];
    //}

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

}
