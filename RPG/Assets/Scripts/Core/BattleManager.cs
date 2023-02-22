using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Control;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;

    public List<EnemyController> liveEnemys;
    public List<PlayerController> livePlayers;

    // TEST
    public List<Controller> lives = new List<Controller>();

    public BattleState currentStats;

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
        foreach (var item in livePlayers)
        {
            item.target = ReturnNearDistanceController<EnemyController>(item.transform);
        }

        foreach (var item in liveEnemys)
        {
            item.target = ReturnNearDistanceController<PlayerController>(item.transform);
        }
    }

    public void MoveToNextPhase<T>(T controllerMyself) where T : Controller
    {
        if (typeof(T) == typeof(PlayerController))
        {
            foreach (var item in livePlayers)
            {
                item.target = ReturnNearDistanceController<EnemyController>(item.transform);
            }
        }
        else if (typeof(T) == typeof(EnemyController))
        {
            foreach (var item in liveEnemys)
            {
                item.target = ReturnNearDistanceController<PlayerController>(item.transform);
            }
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

    //public T ReturnNearDistanceController2<T>(Transform transform) where T : Controller
    //{
    //    var list =
    //        from controller in lives where (typeof(T) == controller.GetType())
    //        select controller;

    //    return (T)list[0];
    //}

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

}
