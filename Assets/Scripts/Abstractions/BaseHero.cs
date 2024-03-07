using Data.PersistentData;
using Interfaces;
using Managers;
using Misc;
using UnityEngine;

namespace Abstractions
{
    public abstract class BaseHero : BaseCharacter, ISelectable, IAttackable
    {
        [SerializeField] private SpriteRenderer _healthBar;
        [SerializeField] private GameObject _selectionIndicator;
        
        protected HeroAttributes _attributes;
        
        public HeroAttributes Attributes => _attributes;
        
        protected int _currentHealth;
        public void Select()
        {
            _selectionIndicator.SetActive(true);
        }

        public void Deselect()
        {
            _selectionIndicator.SetActive(false);
        }

        public void Attack(IAttackable target)
        {
            if (target != null)
            {
                target.TakeDamage(_attributes.AttackPower);
            }
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            UpdateHealthBar();
            
            if (_currentHealth <= 0)
                Die();
        }
        
        private void UpdateHealthBar()
        {
            float healthPercent = _currentHealth / (float)_attributes.Health;
            _healthBar.material.SetFloat("_FillAmount", healthPercent);
        }

        public void Die()
        {
            EventManager.Notify(ActionType.OnHeroDead, this);
            HeroPoolManager.Instance.ReturnToPool(_attributes.Guid, this);
        }
    }
}