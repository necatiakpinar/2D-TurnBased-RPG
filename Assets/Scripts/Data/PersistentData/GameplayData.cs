using System;
using UnityEngine;

namespace Data.PersistentData
{
    [Serializable]
    public class GameplayData
    {
        [SerializeField] private PlayerInventory _playerInventory = new();
        [SerializeField] private EnemyData _enemyData = new();
        [SerializeField] private BattleData _battleData = new();
        
        public PlayerInventory PlayerInventory => _playerInventory;
        public EnemyData EnemyData => _enemyData;
        public BattleData BattleData => _battleData;
    }
}