using System.Linq;
using Data.PersistentData;
using NecatiAkpinar.Abstractions;
using UnityEngine;

namespace Managers
{
    public class HeroSelectionManager : MonoBehaviour
    {
        private PlayerInventory _playerInventory;
        private EnemyData _enemyData;
        private BattleData _battleData;

        private void Awake()
        {
            _playerInventory = GameplayDataState.GameplayData.PlayerInventory;
            _battleData = GameplayDataState.GameplayData.BattleData;
            _enemyData = GameplayDataState.GameplayData.EnemyData;

            TryAddingNewUnlockedHeroes();
            TryAddingEnemyToTheBattle();
        }

        private void TryAddingEnemyToTheBattle()
        {
            var enemyData = DataManager.Instance.EnemyDataContainer.EnemyDataList[0];
            var isExist = _enemyData.Enemies.Any(data => data.Guid == enemyData.Id);
            if (isExist)
                return;

            var enemyAttributes = new EnemyAttributes(enemyData);
            _enemyData.AddEnemy(enemyAttributes);
            GameplayDataState.SaveDataToDisk();
        }

        private void TryAddingNewUnlockedHeroes()
        {
            var heroesData = DataManager.Instance.HeroDataContainer.HeroDataList;

            foreach (var heroData in heroesData)
                if (heroData.UnlockedBattleLevel <= _battleData.CurrentBattleLevel)
                    AddHero(heroData);
        }

        private void AddHero(BaseHeroDataSO hero)
        {
            var isExist = _playerInventory.OwnedHeroes.Any(heroData => heroData.Guid == hero.Id);
            if (isExist)
                return;

            var heroAttributes = new HeroAttributes(hero);
            if (!_playerInventory.OwnedHeroes.Contains(heroAttributes))
                _playerInventory.AddHero(heroAttributes);

            GameplayDataState.SaveDataToDisk();
        }
    }
}