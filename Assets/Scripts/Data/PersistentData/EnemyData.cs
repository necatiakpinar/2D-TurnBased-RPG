using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.PersistentData
{
    [Serializable]
    public class EnemyData
    {
        [SerializeField] private List<EnemyAttributes> _enemies = new();

        public List<EnemyAttributes> Enemies => _enemies;

        public void AddEnemy(EnemyAttributes enemy)
        {
            if (!_enemies.Contains(enemy))
                _enemies.Add(enemy);
        }

        public void RemoveEnemy(EnemyAttributes enemy)
        {
            if (_enemies.Contains(enemy))
                _enemies.Remove(enemy);
        }

        public EnemyAttributes GetEnemy(string guid)
        {
            var attributes =
                _enemies.FirstOrDefault(enemy => enemy.Guid == guid);
            return attributes;
        }

        public void IncreaseStats()
        {
            _enemies.ForEach(enemy => enemy.IncreaseStats());
        }
    }
}