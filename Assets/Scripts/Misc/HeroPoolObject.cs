using System;
using Abstractions;
using UnityEngine;

namespace Misc
{
    [Serializable]
    public class HeroPoolObject
    {
        [SerializeField] private string _id;
        [SerializeField] private BaseHero _heroPF;
        [SerializeField] private int _size;

        public string Id => _id;
        public BaseHero HeroPf => _heroPF;
        public int Size => _size;
    }

}