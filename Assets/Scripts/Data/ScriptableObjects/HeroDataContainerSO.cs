using System.Collections.Generic;
using Abstractions;
using NecatiAkpinar.Abstractions;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "NecatiAkpinar/Containers/HeroDataContainer", fileName = "HeroDataContainer", order = 1)]
    public class HeroDataContainerSO : ScriptableObject
    {
        [SerializeField] private List<NormalHeroDataSO> _heroDataList;
        public List<NormalHeroDataSO> HeroDataList => _heroDataList;
    }
}