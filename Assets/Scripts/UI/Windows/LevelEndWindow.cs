using Abstractions;
using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Windows
{
    public class LevelEndWindow : BaseWindow
    {
        [SerializeField] private TMP_Text titleLabel;
        [SerializeField] private BaseButton _returnToHeroSelectionButton;

        private readonly string _levelFinishedText = "Level Finished";
        private readonly string _levelFailedText = "Level Failed";

        private readonly Color _levelFinishedTitleColor = Color.green;
        private readonly Color _levelFailedTitleColor = Color.red;
        
        public void Init(bool isPlayerWon)
        {
            _returnToHeroSelectionButton.Init(LoadGameplayScene);
            
            if (isPlayerWon)
                WinScreen();
            else
                LoseScreen();
        }

        private void WinScreen()
        {
            _returnToHeroSelectionButton.gameObject.SetActive(true);
            titleLabel.text = _levelFinishedText;
            titleLabel.color = _levelFinishedTitleColor;
        }

        private void LoseScreen()
        {
            _returnToHeroSelectionButton.gameObject.SetActive(true);
            titleLabel.text = _levelFailedText;
            titleLabel.color = _levelFailedTitleColor;
        }

        private void LoadGameplayScene()
        {
            SceneManager.LoadScene(Constants.SCENE_HEROSELECTION);
        }
    }
}