using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Status 
{
    public class Status : MonoBehaviour
    {
        [Header("Health")]
        public int maxHp = 0;

        [Header("Attack")]
        public float attackRange = 0f;
        public int attackDamage = 0;
        public float attackSpeed = 0f;
        public float criticalChance = 0f;
        public float criticalDamage = 0f;
        public float attackChance = 0f;

        [Header("Defence")]
        public int defencePoint = 0;
        public float evasionPoint = 0f;
        public float decreseCriticalDamage = 0f;
        public float evasionCritical = 0f;

        [Header("Movement")]
        public float movementSpeed = 0f;
    }
}

