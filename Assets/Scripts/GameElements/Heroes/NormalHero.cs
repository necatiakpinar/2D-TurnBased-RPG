using Abstractions;
using Data.PersistentData;
using Data.ScriptableObjects;
using Interfaces;
using UnityEngine;

namespace GameElements.Heroes
{
    public class NormalHero : BaseHero
    {
        private NormalHeroDataSO _data;
        
        public NormalHeroDataSO Data => _data;
        
        private void Awake()
        {
            _data = (NormalHeroDataSO)_baseData;
        }

        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();
            _attributes = GameplayDataState.GameplayData.PlayerInventory.GetSelectedHero(_data.Id);
            
            if (_attributes == null)
            {
                Debug.LogError("Attributes is null");
                return;
            }
            
            _currentHealth = _attributes.Health;
        }
        
    }
}