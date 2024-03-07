using System;
using UnityEngine;

namespace Abstractions
{
    public abstract class BaseCharacterDataSO : ScriptableObject
    {
        [SerializeField] private string _guid;
        [SerializeField] private string _name;
        [SerializeField] private int _health;
        [SerializeField] private int _attackPower;
        [SerializeField] private Color _color;
        
        public string Id => _guid;
        public string Name => _name;
        public int Health => _health;
        public int AttackPower => _attackPower;
        public Color Color => _color;

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(_guid))
                _guid = Guid.NewGuid().ToString();
        }
    }
}