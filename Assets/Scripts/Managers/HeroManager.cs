using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Data.PersistentData;
using Misc;
using UnityEngine;

namespace Managers
{
    public class HeroManager : MonoBehaviour
    {
        [SerializeField] private List<BaseHero> _heroes;
        [SerializeField] private List<WorldCharacterPlacement> _placements;

        public List<BaseHero> Heroes => _heroes;

        private void OnEnable()
        {
            Action<object[]> onHeroDead = (parameters) => OnHeroDead((BaseHero)parameters[0]);
            EventManager.Subscribe(ActionType.OnHeroDead, onHeroDead);
            Action<object[]> onLevelFinished = (parameters) => OnLevelFinished((bool)parameters[0]);
            EventManager.Subscribe(ActionType.OnLevelFinished, onLevelFinished);


            Func<object[], object> getRandomHero = (_) => GetRandomHero();
            EventManager.Subscribe(FunctionType.GetRandomHero, getRandomHero);
            Func<object[], object> isAllHeroesDead = (_) => IsAllHeroesDead();
            EventManager.Subscribe(FunctionType.IsAllHeroesDead, isAllHeroesDead);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe(ActionType.OnHeroDead);
            EventManager.Unsubscribe(ActionType.OnLevelFinished);

            EventManager.Unsubscribe(FunctionType.GetRandomHero);
            EventManager.Unsubscribe(FunctionType.IsAllHeroesDead);
        }

        private void Start()
        {
            SpawnHeroes();
        }

        private void SpawnHeroes()
        {
            var selectedHeroes = GameplayDataState.GameplayData.PlayerInventory.SelectedHeroes;

            for (int i = 0; i < _placements.Count; i++)
            {
                var heroData = selectedHeroes[i];
                var createdHero =
                    HeroPoolManager.Instance.SpawnFromPool(heroData.Guid, Vector2.zero, Quaternion.identity);
                _heroes.Add(createdHero);
                _placements[i].PlaceCharacter(createdHero);
            }
        }

        private BaseHero GetRandomHero()
        {
            var randomIndex = UnityEngine.Random.Range(0, _heroes.Count);
            return _heroes[randomIndex];
        }

        private bool IsAllHeroesDead()
        {
            return _heroes.Count == 0;
        }

        private void OnHeroDead(BaseHero hero)
        {
            _heroes.Remove(hero);
            _placements.First(placement => placement.Character == hero).RemoveCharacter();
        }

        private void OnLevelFinished(bool isWin)
        {
            if (isWin)
            {
                for (int i = 0; i < _heroes.Count; i++)
                {
                    var hero = _heroes[i];
                    hero.Attributes.IncreaseExperience();
                    GameplayDataState.SaveDataToDisk();
                }
            }
            
        }
    }
}