using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Core
{
    [CreateAssetMenu(fileName = "NewStage",menuName = "CreateScriptableObject/CreateStage", order = 0)]
    public class Stage : ScriptableObject
    {
        public Vector3 playerSpawnPosition = new Vector3(8.0f, 0f);
        public List<Vector3> enemySpawnPositions;
        public List<GameObject> enemyPrefabs;

        public void CreatePlayer(GameObject playerPrefab, Transform parent)
        {
            Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity, parent);
        }

        public void CreateEnemys(Transform parent)
        {
            for (int i = 0; i < enemySpawnPositions.Count; i++)
            {
                Instantiate(enemyPrefabs[i], enemySpawnPositions[i], Quaternion.identity, parent);
            }
        }
    }
}