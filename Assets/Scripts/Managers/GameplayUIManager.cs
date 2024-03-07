using System;
using Misc;
using UI.Windows;
using UnityEngine;

namespace Managers
{
    public class GameplayUIManager: MonoBehaviour
    {
        [SerializeField] private GameplayWindow _gameplayWindow;
        [SerializeField] private LevelEndWindow _levelEndWindow;

        private void OnEnable()
        {
            Action<object[]> onLevelFinished = (parameters) => ActivateLevelEndWindow((bool)parameters[0]);
            EventManager.Subscribe(ActionType.OnLevelFinished, onLevelFinished);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe(ActionType.OnLevelFinished);
        }

        private void Start()
        {
            _gameplayWindow.gameObject.SetActive(true);
            _levelEndWindow.gameObject.SetActive(false);
        }

        private void ActivateLevelEndWindow(bool isWin)
        {
            _gameplayWindow.gameObject.SetActive(false);
            _levelEndWindow.gameObject.SetActive(true);
            
            _levelEndWindow.Init(isWin);
        }
    }
}