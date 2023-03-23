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

            StageData data = GameManager.Instance.stageDataDic[1];

            factory.CreatePlayer(userinfo, data.playerSpawnPosition);
            //print(GameManager.Instance.enemyDataDic[1].enemyName);

            foreach (var enemy in data.enemyDatas)
            {
                factory.CreateEnemy(GameManager.Instance.enemyDataDic[enemy.enemyID], enemy.position);
            }
        }

    }
}
