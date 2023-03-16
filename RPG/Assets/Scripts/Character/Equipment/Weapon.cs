using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    public int damage;
    [Range(1, 2.5f)] public float attackSpeed;
    [Range(1, 5f)] public float attackDistance;
    [Range(1, 5f)] public float movementSpeed;
    [Range(0, 60.0f)] public float criticalChance;
    [Range(0, 50.0f)] public float criticalDamage;
    [Range(80.0f, 100.0f)] public float attackChance;
}
