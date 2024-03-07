using System;
using Abstractions;
using Data.PersistentData;
using Data.ScriptableObjects;
using Interfaces;
using UnityEngine;

namespace GameElements.Enemies
{
    public class NormalEnemy : BaseEnemy
    {
        private NormalEnemyDataSO _data;

        public NormalEnemyDataSO Data => _data;

        private void Awake()
        {
            _data = (NormalEnemyDataSO)_baseData;
        }

        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();
            _attributes = GameplayDataState.GameplayData.EnemyData.GetEnemy(_data.Id);
            
            if (_attributes == null)
            {
                Debug.LogError("Attributes is null");
                return;
            }
            
            _currentHealth = _attributes.Health;
        }
        
    }
}