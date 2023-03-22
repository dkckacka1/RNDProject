using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Core
{
    [System.Serializable]
    public class EnemyCreate
    {
        [SerializeReference]public Vector3 position;
        [SerializeReference] public EnemyData data;
    }
}