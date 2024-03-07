using System;
using Abstractions;
using Data.PersistentData;
using Misc;
using NecatiAkpinar.Abstractions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class HeroCardWidget : BaseWidget, IPointerDownHandler, IPointerUpHandler, IUpdateSelectedHandler
    {
        [SerializeField] private TMP_Text _heroName;
        [SerializeField] private Image _heroImage;
        [SerializeField] private Image _selectedBorder;
        [SerializeField] private HeroCardInfoWidget _infoWidget;

        private Button _button;
        private bool _isSelected;
        private bool _isActivated;
        private bool _isButtonPressed;
        private bool _isInfoPopupOpened;
        private float _pressStartTime;
        
        private BaseHeroDataSO _heroData;
        private PlayerInventory _playerInventory;

        private void Awake()
        {
            var buttonExist = transform.TryGetComponent(out _button);
            if (buttonExist)
                _button.onClick.AddListener(OnButtonClicked);
        }

        public void Init(BaseHeroDataSO heroData)
        {
            _heroData = heroData;
            var colorTest = _heroData.Color;
            _heroName.text = _heroData.Name;
            _heroImage.material.color = colorTest;

            _playerInventory = GameplayDataState.GameplayData.PlayerInventory;
            var currentBattleLevel = GameplayDataState.GameplayData.BattleData.CurrentBattleLevel;
            
            _isActivated = _heroData.UnlockedBattleLevel <= currentBattleLevel;
            _button.interactable = _isActivated;

            if (_isActivated)
            {
                var heroAttributes = _playerInventory.GetOwnedHero(_heroData.Id);
                _infoWidget.Init(heroAttributes );    
            }
        }

        private void OnButtonClicked()
        {
            if (!_isActivated)
                return;

            var isMaxHeroSelected = _playerInventory.SelectedHeroes.Count == Constants.HERO_SELECTION_COUNT;

            if (!_isSelected && isMaxHeroSelected)
                return;
            
            _isSelected = !_isSelected;
            _selectedBorder.gameObject.SetActive(_isSelected);

            if (_isSelected)
                _playerInventory.AddSelectedHero(_heroData.Id);
            else
                _playerInventory.RemoveSelectedHero(_heroData.Id);

            GameplayDataState.SaveDataToDisk();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isActivated)
                return;
            
            _isButtonPressed = true;
            _pressStartTime = Time.time;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isActivated)
                return;
            
            _isButtonPressed = false;
            _pressStartTime = 0;
            _infoWidget.gameObject.SetActive(false);
            _isInfoPopupOpened = false;
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            if (!_isButtonPressed)
                return;

            if (Time.time - _pressStartTime >= Constants.HERO_INFO_HOLD_DURATION && !_isInfoPopupOpened)
            {
                _isInfoPopupOpened = true;
                _infoWidget.gameObject.SetActive(true);
            }
        }
    }
}