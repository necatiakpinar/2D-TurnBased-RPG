using System;
using Data.PersistentData;
using Misc;
using UnityEngine;

namespace Managers
{
    public class BattleManager : MonoBehaviour
    {
        private BattleData _battleData;
        
        private void OnEnable()
        {
            Action<object[]> onLevelFinished = (parameters) => OnBattleFinished((bool)parameters[0]);
            EventManager.Subscribe(ActionType.OnLevelFinished, onLevelFinished);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe(ActionType.OnLevelFinished);
        }

        private void Start()
        {
            _battleData = GameplayDataState.GameplayData.BattleData;
        }

        private void OnBattleFinished(bool isWin)
        {
            _battleData.IncreaseBattleLevel();
            GameplayDataState.SaveDataToDisk();
        }
    }
}