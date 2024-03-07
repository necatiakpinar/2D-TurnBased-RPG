using System.Collections.Generic;
using Abstractions;
using Misc;
using UnityEngine;

namespace Managers
{
    public class HeroPoolManager : Singleton<HeroPoolManager>
    {
        [SerializeField] private List<HeroPoolObject> pools;

        private Dictionary<string, Queue<BaseHero>> _poolDictionary;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _poolDictionary = new Dictionary<string, Queue<BaseHero>>();

            foreach (var pool in pools)
            {
                Queue<BaseHero> objectPool = new Queue<BaseHero>();

                for (int i = 0; i < pool.Size; i++)
                {
                    BaseHero hero = Instantiate(pool.HeroPf);
                    hero.gameObject.SetActive(false);
                    objectPool.Enqueue(hero);
                }

                _poolDictionary.Add(pool.Id, objectPool);
            }
        }

        public BaseHero SpawnFromPool(string id, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(id) || _poolDictionary[id].Count == 0)
                return null;

            var heroToSpawn = _poolDictionary[id].Dequeue();

            heroToSpawn.Init();
            heroToSpawn.gameObject.SetActive(true);
            heroToSpawn.transform.position = position;
            heroToSpawn.transform.rotation = rotation;

            return heroToSpawn;
        }

        public void ReturnToPool(string id, BaseHero objectToReturn)
        {
            if (!_poolDictionary.ContainsKey(id))
                return;

            objectToReturn.ResetObject();
            _poolDictionary[id].Enqueue(objectToReturn);
        }
    }
}