using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Core;
using RPG.Battle.Core;

public class TestScene1 : MonoBehaviour
{
    public PlayerStatus status;
    public BattleFactory factory;

    public Transform PlayerSpawn;
    public Transform enemySpawn;

    private void Start()
    {
        if(GameManager.Instance != null)
        {
            UserInfo userinfo = new UserInfo();

            factory.CreatePlayer(userinfo, PlayerSpawn.position);
            //print(GameManager.Instance.enemyDataDic[1].enemyName);
            factory.CreateEnemy(GameManager.Instance.enemyDataDic[1], enemySpawn.position);
        }

    }
}
