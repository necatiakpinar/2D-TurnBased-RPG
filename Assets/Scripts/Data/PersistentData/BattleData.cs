using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.PersistentData
{
    [Serializable]
    public class BattleData
    {
        [SerializeField] private int _currentBattleLevel;
        
        public int CurrentBattleLevel => _currentBattleLevel;
        
        public void IncreaseBattleLevel()
        {
            _currentBattleLevel++;
        }
    }
}