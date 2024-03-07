using System;
using System.Threading.Tasks;
using Abstractions;
using Addressables;
using Data.PersistentData;
using Data.ScriptableObjects;
using Managers;
using Misc;
using UI.Widgets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Windows
{
    public class HeroSelectionWindow : BaseWindow
    {
        [SerializeField] private Transform _heroesParent;
        [SerializeField] private BaseButton _enterBattleButton;

        private PlayerInventory _playerInventory;

        private async void Start()
        {
            await Init();
        }

        private async Task Init()
        {
            var allHeroes = DataManager.Instance.HeroDataContainer.HeroDataList;
            var heroCardPf =
                await AddressableLoader.LoadComponentAsync<HeroCardWidget>(
                    AddressableKeys.GetKey(AddressableKeys.AssetKeys.HeroCardWidget));

            for (int i = 0; i < allHeroes.Count; i++)
            {
                var heroData = allHeroes[i];
                var heroCard = Instantiate(heroCardPf, _heroesParent);
                heroCard.Init(heroData);
            }

            _playerInventory = GameplayDataState.GameplayData.PlayerInventory;
            _playerInventory.SelectedHeroes.Clear();
            await GameplayDataState.SaveDataToDisk();

            _enterBattleButton.Init(StartBattle);
        }

        private void StartBattle()
        {
            var canEnterBattle = _playerInventory.SelectedHeroes.Count == Constants.HERO_SELECTION_COUNT;

            if (canEnterBattle)
                SceneManager.LoadScene(Constants.SCENE_GAMEPLAY);
            
                //todo(necatiakpinar): Add feedback for not selecting enough heroes
        }
    }
}