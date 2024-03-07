using System;
using Abstractions;
using UnityEngine;

namespace Misc
{
    [Serializable]
    public class WorldCharacterPlacement
    {
        [SerializeField] private Transform _placementTransform;
        [SerializeField] private BaseCharacter _character;

        public BaseCharacter Character => _character;

        public void PlaceCharacter(BaseCharacter character)
        {
            _character = character;
            _character.transform.parent = _placementTransform;
            _character.transform.localPosition = Vector2.zero;
        }
        
        public void RemoveCharacter()
        {
            _character = null;
        }
    }
}