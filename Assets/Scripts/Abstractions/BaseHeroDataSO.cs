using Abstractions;
using UnityEngine;
using UnityEngine.Serialization;

namespace NecatiAkpinar.Abstractions
{
    public abstract class BaseHeroDataSO : BaseCharacterDataSO
    {
        [SerializeField] private int _experience;
        [SerializeField] private int _level;
        [SerializeField] private int _unlockedBattleLevel;

        public int Experience => _experience;
        public int Level => _level;
        public int UnlockedBattleLevel => _unlockedBattleLevel;
    }
}