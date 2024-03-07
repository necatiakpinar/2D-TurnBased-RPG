using System.Collections.Generic;
using Abstractions;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [System.Serializable]
    public class GameVFX
    {
        [SerializeField] private string key;
        [SerializeField] private BaseVFXMono _vfxObject;
        [SerializeField] private bool _isPoolObject;
        [SerializeField] private int _poolSize;
        public string Key => key;
        public BaseVFXMono VfxObject => _vfxObject;
        public bool IsPoolObject => _isPoolObject;
        public int PoolSize => _poolSize;
    }

    [CreateAssetMenu(menuName = "NecatiAkpinar/Containers/VFXContainer", fileName = "VFXContainer_SO", order = 2)]
    public class VFXContainerSO : ScriptableObject
    {
        [SerializeField] private List<GameVFX> _gameplayVFXes;

        public List<GameVFX> GameplayVFXes => _gameplayVFXes;
    }
}