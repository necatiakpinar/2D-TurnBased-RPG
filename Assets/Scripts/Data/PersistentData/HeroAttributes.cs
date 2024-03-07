using System;
using Misc;
using NecatiAkpinar.Abstractions;
using UnityEngine;

namespace Data.PersistentData
{
    [Serializable]
    public class HeroAttributes
    {
        [SerializeField] private string _guid;
        [SerializeField] private string _name;
        [SerializeField] private int _level;
        [SerializeField] private int _health;
        [SerializeField] private int _attackPower;
        [SerializeField] private int _experience;
        [SerializeField] private int _unlockedBattleLevel;

        public string Guid => _guid;
        public string Name => _name;
        public int Level => _level;
        public int Health => _health;
        public int AttackPower => _attackPower;
        public int Experience => _experience;
        public int UnlockedBattleLevel => _unlockedBattleLevel;
        
        public HeroAttributes(BaseHeroDataSO heroData)
        {
            _guid = heroData.Id;
            _level = heroData.Level;
            _health = heroData.Health;
            _attackPower = heroData.AttackPower;
            _experience = heroData.Experience;
            _unlockedBattleLevel = heroData.UnlockedBattleLevel;
        }

        public void IncreaseExperience(int experience = 1)
        {
            _experience += experience;

            if (_experience >= Constants.REQUIREDEXP_TOLEVELUP)
            {
                LevelUp();
                GameplayDataState.SaveDataToDisk();
            }
        }

        public void LevelUp()
        {
            _level++;
            var healthIncrease = Mathf.RoundToInt(_health * Constants.LEVELUP_MULTIPLIER);
            var attackPowerIncrease = Mathf.RoundToInt(_attackPower * Constants.LEVELUP_MULTIPLIER);
            
            _health += healthIncrease;
            _attackPower += attackPowerIncrease;

            _experience = 0;
        }
    }
}