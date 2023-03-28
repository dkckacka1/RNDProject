using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "CreateScriptableObject/CreateWeapon", order = 1)]
    public class WeaponData : EquipmentData
    {
        public GameObject weaponLook;

        public int attackDamage;
        [Range(1, 2.5f)] public float attackSpeed;
        [Range(1, 5f)] public float attackRange;
        [Range(1, 5f)] public float movementSpeed;
        [Range(0, 0.6f)] public float criticalChance;
        [Range(0, 0.5f)] public float criticalDamage;
        [Range(0.8f, 1.0f)] public float attackChance;
    }
}
