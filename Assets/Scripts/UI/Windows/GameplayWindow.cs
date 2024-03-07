using Abstractions;
using Data.PersistentData;
using TMPro;
using UnityEngine;

namespace UI.Windows
{
    public class GameplayWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _battleTitleLabel;

        private void Start()
        {
            SetLabels();
        }

        private void SetLabels()
        {
            var currentBattleLevel = GameplayDataState.GameplayData.BattleData.CurrentBattleLevel;
            _battleTitleLabel.text = $"Battle : {currentBattleLevel}";
        }
    }
}