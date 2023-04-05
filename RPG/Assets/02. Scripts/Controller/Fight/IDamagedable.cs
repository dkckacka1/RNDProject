using UnityEngine;

namespace RPG.Battle.Fight
{
    public interface IDamagedable
    {
        void TakeDamage(int damage, DamagedType type);
        void Heal(int healPoint);
        void Dead();

        Transform Transfrom { get; }

        bool IsDead { get;}
        float AttackChance { get; }
        float EvasionPoint { get; }
        float DecreseCriticalDamage { get; }
        float EvasionCritical { get; }
        int DefencePoint { get; }


    }
}