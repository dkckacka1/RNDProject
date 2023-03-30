using UnityEngine;

[System.Serializable]
public struct EnemySpawnStruct
{
    public Vector3 position;
    public int enemyID;
}

[System.Serializable]
public struct DropTable
{
    public DropItemType itemType;
    public int percent;
}

