using Abstractions;
using Data.PersistentData;
using NecatiAkpinar.Abstractions;
using TMPro;
using UnityEngine;

namespace UI.Widgets
{
    public class HeroCardInfoWidget : BaseWidget
    {
        [SerializeField] private TMP_Text _heroNameLabel;
        [SerializeField] private TMP_Text _heroLevelLabel;
        [SerializeField] private TMP_Text _heroHealthLabel;
        [SerializeField] private TMP_Text _heroAttackPowerLabel;
        [SerializeField] private TMP_Text _heroExpLabel;
        
        public void Init(HeroAttributes heroData)
        {
            _heroNameLabel.text = $"Name: {heroData.Name}";
            _heroLevelLabel.text = $"Level: {heroData.Level}";
            _heroHealthLabel.text = $"Health: {heroData.Health}";
            _heroAttackPowerLabel.text = $"Attack Power: {heroData.AttackPower}";
            _heroExpLabel.text = $"Exp: {heroData.Experience}";
        }
    }
}