using System.Collections.Generic;
using Abstractions;
using Misc;
using UnityEngine;

namespace Managers
{
    public class EnemyPoolManager : Singleton<EnemyPoolManager>
    {
        [SerializeField] private List<EnemyPoolObject> pools;

        private Dictionary<string, Queue<BaseEnemy>> _poolDictionary;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _poolDictionary = new Dictionary<string, Queue<BaseEnemy>>();

            foreach (var pool in pools)
            {
                Queue<BaseEnemy> objectPool = new Queue<BaseEnemy>();

                for (int i = 0; i < pool.Size; i++)
                {
                    BaseEnemy enemy = Instantiate(pool.EnemyPf);
                    enemy.gameObject.SetActive(false);
                    objectPool.Enqueue(enemy);
                }

                _poolDictionary.Add(pool.Id, objectPool);
            }
        }

        public BaseEnemy SpawnFromPool(string id, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(id) || _poolDictionary[id].Count == 0)
                return null;

            var enemyToSpawn = _poolDictionary[id].Dequeue();

            enemyToSpawn.Init();
            enemyToSpawn.gameObject.SetActive(true);
            enemyToSpawn.transform.position = position;
            enemyToSpawn.transform.rotation = rotation;

            return enemyToSpawn;
        }

        public void ReturnToPool(string id, BaseEnemy objectToReturn)
        {
            if (!_poolDictionary.ContainsKey(id))
                return;

            objectToReturn.ResetObject();
            _poolDictionary[id].Enqueue(objectToReturn);
        }
    }
}