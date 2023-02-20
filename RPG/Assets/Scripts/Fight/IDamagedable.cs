namespace RPG.Fight
{
    public interface IDamagedable
    {
        void TakeDamage(int damage);
        void Heal(int healPoint);
        void Dead();

        bool IsDead { get; set; }
    }
}