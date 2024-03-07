using System.Collections.Generic;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [System.Serializable]
    public class GameSFX
    {
        [SerializeField] private string key;
        [SerializeField] private AudioClip _clip;
        public string Name => key;
        public AudioClip Clip => _clip;
    }

    [CreateAssetMenu(fileName = "SFXContainer_SO", menuName = "NecatiAkpinar/Containers/SFXContainer", order = 3)]
    public class SFXContainerSO : ScriptableObject
    {
        [SerializeField] private List<GameSFX> _gameplaySFXes;

        public List<GameSFX> GameplaySFXes => _gameplaySFXes;
    }
}