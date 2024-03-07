using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Data.PersistentData;
using Misc;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<BaseEnemy> _enemies;
        [SerializeField] private List<WorldCharacterPlacement> _placements;

        public List<BaseEnemy> Enemies => _enemies;

        private void OnEnable()
        {
            Action<object[]> onEnemyDead = (parameters) => OnEnemyDead((BaseEnemy)parameters[0]);
            EventManager.Subscribe(ActionType.OnEnemyDead, onEnemyDead);

            Func<object[], object> isAllEnemiesDead = (_) => IsAllEnemiesDead();
            EventManager.Subscribe(FunctionType.IsAllEnemiesDead, isAllEnemiesDead);
            
            Action<object[]> onLevelFinished = (parameters) => OnLevelFinished((bool)parameters[0]);
            EventManager.Subscribe(ActionType.OnLevelFinished, onLevelFinished);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe(ActionType.OnEnemyDead);
            EventManager.Unsubscribe(ActionType.OnLevelFinished);
            
            EventManager.Unsubscribe(FunctionType.IsAllEnemiesDead);
        }

        private void Start()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            var enemies = GameplayDataState.GameplayData.EnemyData.Enemies;
            
            for (int i = 0; i < _placements.Count; i++)
            {
                var enemyData = enemies[i];
                var createdEnemy =
                    EnemyPoolManager.Instance.SpawnFromPool(enemyData.Guid, Vector2.zero, Quaternion.identity);
                _enemies.Add(createdEnemy);
                _placements[i].PlaceCharacter(createdEnemy);
            }
        }

        private bool IsAllEnemiesDead()
        {
            return _enemies.Count == 0;
        }

        private void OnEnemyDead(BaseEnemy enemy)
        {
            _enemies.Remove(enemy);
            _placements.First(placement => placement.Character == enemy).RemoveCharacter();
        }

        private void OnLevelFinished(bool isWin)
        {
            if (isWin)
            {
                GameplayDataState.GameplayData.EnemyData.IncreaseStats();
                GameplayDataState.SaveDataToDisk();
            }
            
        }
    }
}