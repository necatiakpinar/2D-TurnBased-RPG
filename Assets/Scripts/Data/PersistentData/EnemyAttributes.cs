using System;
using Abstractions;
using Misc;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.PersistentData
{
    [Serializable]
    public class EnemyAttributes
    {
        [SerializeField] private string _guid;
        [SerializeField] private int _health;
        [SerializeField] private int _attackPower;

        public string Guid => _guid;
        public int Health => _health;
        public int AttackPower => _attackPower;
        
        public EnemyAttributes(BaseEnemyDataSO enemyData)
        {
            _guid = enemyData.Id;
            _health = enemyData.Health;
            _attackPower = enemyData.AttackPower;
        }

        public void IncreaseStats()
        {
            var healthIncrease = Mathf.RoundToInt(_health * Constants.ENEMY_STAT_INCREASER);
            var attackPowerIncrease = Mathf.RoundToInt(_attackPower * Constants.ENEMY_STAT_INCREASER);
            
            _health += healthIncrease;
            _attackPower += attackPowerIncrease;
        }
    }
}