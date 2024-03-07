using System.Collections.Generic;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "NecatiAkpinar/Containers/EnemyDataContainer", fileName = "EnemyDataContainer", order = 1)]
    public class EnemyDataContainerSO : ScriptableObject
    {
        [SerializeField] private List<NormalEnemyDataSO> _enemyDataList;
        public List<NormalEnemyDataSO> EnemyDataList => _enemyDataList;
    }
}