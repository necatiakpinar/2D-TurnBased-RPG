namespace Interfaces
{
    public interface IAttackable
    {
        public void Attack(IAttackable target);
        public void TakeDamage(int damage);
        public void Die();
    }
}