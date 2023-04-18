using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
using RPG.Core;
using RPG.Battle.Core;
using RPG.Character.Equipment;

namespace RPG.Test
{
    public class BattleTester : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] GameManager gameManager;
        [SerializeField] BattleManager battleManager;

        [Header("TestButton")]
        [SerializeField] bool createButton;
        [SerializeField] bool battleStateButton;
        [SerializeField] bool changeEquipmentButton;

        [Header("SpawnPosition")]
        [SerializeField] Transform playerPos;
        [SerializeField] List<Transform> enemiesPos;
        [Range(1, 3)]
        [SerializeField] int enemyCreateNum;

        [Header("DataID")]
        [SerializeField] int stageDataID;
        [SerializeField] int enemyDataID;
        [SerializeField] int equipmentDataID;
        [SerializeField] int incantDataID;

        private void Start()
        {

        }

        private void OnGUI()
        {
            if (createButton)
            {
                if (GUI.Button(new Rect(10, 10, 100, 100), "CreatePlayer"))
                {
                    var player = BattleManager.ObjectPool.CreatePlayer(this.gameManager.Player);
                    battleManager.livePlayer = player;
                    player.transform.position = playerPos.position;
                }

                if (GUI.Button(new Rect(10, 130, 100, 100), "CreateEnemy"))
                {
                    for (int i = 1; i <= enemyCreateNum; i++)
                    {
                        EnemyData data;
                        if (!gameManager.enemyDataDic.TryGetValue(enemyDataID, out data))
                        {
                            Debug.Log("EnemyData is NULL");
                            return;
                        }

                        var enemy = BattleManager.ObjectPool.GetEnemyController(data, enemiesPos[i - 1].position);
                        battleManager.liveEnemies.Add(enemy);
                    }
                }

                if (GUI.Button(new Rect(10, 230, 100, 100), "RemoveAllController"))
                {
                    if (battleManager.livePlayer != null)
                    {
                        Destroy(battleManager.livePlayer.gameObject);
                        battleManager.livePlayer = null;
                    }
                    foreach (var enemy in battleManager.liveEnemies)
                    {
                        BattleManager.ObjectPool.ReturnEnemy(enemy);
                    }
                    battleManager.liveEnemies.Clear();
                }

                if (GUI.Button(new Rect(10, 330, 100, 100), "CreateStage"))
                {
                    StageData data;
                    if (!gameManager.stageDataDic.TryGetValue(stageDataID, out data))
                    {
                        Debug.Log("StageData is NULL");
                        return;
                    }

                    var player = BattleManager.ObjectPool.CreatePlayer(this.gameManager.Player);
                    battleManager.livePlayer = player;
                    player.transform.position = data.playerSpawnPosition;

                    foreach (var enemySpawnData in data.enemyDatas)
                    {
                        EnemyData enemyData;
                        if (!gameManager.enemyDataDic.TryGetValue(enemySpawnData.enemyID, out enemyData))
                        {
                            Debug.Log("EnemyData is NULL");
                            return;
                        }
                        var enemy = BattleManager.ObjectPool.GetEnemyController(enemyData, enemySpawnData.position);
                        battleManager.liveEnemies.Add(enemy);
                    }
                }
            }

            if (battleStateButton)
            {
                if (GUI.Button(new Rect(10, 10, 100, 100), "Battle"))
                {
                    battleManager.SetBattleState(BattleSceneState.Battle);
                }

                if (GUI.Button(new Rect(10, 130, 100, 100), "Pause"))
                {
                    battleManager.SetBattleState(BattleSceneState.Pause);
                }
            }

            if (changeEquipmentButton)
            {
                if (GUI.Button(new Rect(10, 10, 100, 100), "ChangeEquipment"))
                {
                    EquipmentData data;
                    if (!gameManager.equipmentDataDic.TryGetValue(equipmentDataID, out data))
                    {
                        Debug.Log("EquipmentData is NULL");
                        return;
                    }

                    Debug.Log($@"아이템 ID : {data.ID}
아이템 이름 : {data.EquipmentName}
아이템 타입 : {data.equipmentType}
레어도 : {data.equipmentTier}");

                    switch (data.equipmentType)
                    {
                        case EquipmentItemType.Weapon:
                            gameManager.Player.currentWeapon.ChangeData(data);
                            break;
                        case EquipmentItemType.Armor:
                            gameManager.Player.currentArmor.ChangeData(data);
                            break;
                        case EquipmentItemType.Pants:
                            gameManager.Player.currentPants.ChangeData(data);
                            break;
                        case EquipmentItemType.Helmet:
                            gameManager.Player.currentHelmet.ChangeData(data);
                            break;
                    }

                    gameManager.Player.SetEquipment();
                }

                if (GUI.Button(new Rect(10, 130, 100, 100), "ChangeIncant"))
                {
                    Incant incant;
                    if (!gameManager.incantDic.TryGetValue(incantDataID, out incant))
                    {
                        Debug.Log("IncantData is NULL");
                        return;
                    }

                    Debug.Log($@"인챈트 ID : {incant.incantID}
인챈트 이름 : {incant.incantName}
인챈트 스킬 설명 : {incant.abilityDesc}");

                    switch (incant.itemType)
                    {
                        case EquipmentItemType.Weapon:
                            gameManager.Player.currentWeapon.Incant(incant);
                            break;
                        case EquipmentItemType.Armor:
                            gameManager.Player.currentArmor.Incant(incant);
                            break;
                        case EquipmentItemType.Pants:
                            gameManager.Player.currentPants.Incant(incant);
                            break;
                        case EquipmentItemType.Helmet:
                            gameManager.Player.currentHelmet.Incant(incant);
                            break;
                    }

                    gameManager.Player.SetEquipment();
                }
            }
        }
    }
}