using UnityEngine;

namespace RPG.Battle.Fight
{
    public interface IDamagedable
    {
        void TakeDamage(int damage);
        void Heal(int healPoint);
        void Dead();

        Transform transfrom { get; }

        bool IsDead { get; set; }
    }
}