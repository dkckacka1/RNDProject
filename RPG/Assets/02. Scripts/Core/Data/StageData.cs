using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;

namespace RPG.Battle.Core
{
    [CreateAssetMenu(fileName = "NewStage", menuName = "CreateScriptableObject/CreateStage", order = 0)]
    public class StageData : Data
    {
        [System.Serializable]
        public struct EnemySpawnStruct
        {
            public Vector3 position;
            public int enemyID;
        }

        public Vector3 playerSpawnPosition = new Vector3(8.0f, 0f);
        public EnemySpawnStruct[] enemyDatas;
    }
}