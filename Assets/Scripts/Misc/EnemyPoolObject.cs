using System;
using Abstractions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Misc
{
    [Serializable]
    public class EnemyPoolObject
    {
        [SerializeField] private string _id;
        [SerializeField] private BaseEnemy _enemyPF;
        [SerializeField] private int _size;

        public string Id => _id;
        public BaseEnemy EnemyPf => _enemyPF;
        public int Size => _size;
    }

}