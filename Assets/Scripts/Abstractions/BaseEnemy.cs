using System;
using Data.PersistentData;
using Interfaces;
using Managers;
using Misc;
using UnityEngine;

namespace Abstractions
{
    public abstract class BaseEnemy : BaseCharacter, ISelectable, IAttackable
    {
        [SerializeField] private SpriteRenderer _healthBar;
        [SerializeField] private GameObject _selectionIndicator;
        
        protected EnemyAttributes _attributes;
        protected int _currentHealth;
        
        public EnemyAttributes Attributes => _attributes;

        public void Select()
        {
        }

        public void Deselect()
        {
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
            EventManager.Notify(ActionType.OnEnemyDead, this);
            EnemyPoolManager.Instance.ReturnToPool(_attributes.Guid, this);
        }

    }
}