using UnityEngine;

namespace Abstractions
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _visual;
        [SerializeField] protected BaseCharacterDataSO _baseData;

        public virtual void Init()
        {
            _visual.material.color = _baseData.Color;
        }

        public virtual void ResetObject()
        {
            gameObject.SetActive(false);
        }
    }    
}

