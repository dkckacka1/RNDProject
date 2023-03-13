using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Core
{
    // TODO : �۾��� �������� �����͸� ���� �������� ����
    [CreateAssetMenu(fileName = "NewStage",menuName = "CreateScriptableObject/CreateStage", order = 0)]
    public class Stage : ScriptableObject
    {
        public Vector3 playerSpawnPosition = new Vector3(8.0f, 0f);
        public List<Vector3> enemySpawnPositions;

        public void CreatePlayer(GameObject playerPrefab, Transform parent)
        {
            Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity, parent);
        }

        public void CreateEnemys(Transform parent)
        {
            //foreach (var enemy in enemySpawnPosition)
            //{
            //    Instantiate(enemy.EnemyPrefab, enemy.transform.position, Quaternion.identity, parent);
            //}
        }
    }
}