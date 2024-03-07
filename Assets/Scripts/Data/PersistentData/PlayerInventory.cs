using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.PersistentData
{
    [Serializable]
    public class PlayerInventory
    {
        [SerializeField] private List<HeroAttributes> _ownedHeroes;
        [SerializeField] private List<HeroAttributes> _selectedHeroes;

        public List<HeroAttributes> OwnedHeroes => _ownedHeroes;
        public List<HeroAttributes> SelectedHeroes => _selectedHeroes;


        public PlayerInventory()
        {
            _ownedHeroes = new List<HeroAttributes>();
            _selectedHeroes = new List<HeroAttributes>();
        }

        public void AddHero(HeroAttributes hero)
        {
            if (!_ownedHeroes.Contains(hero))
                _ownedHeroes.Add(hero);
        }

        public void RemoveHero(HeroAttributes hero)
        {
            if (_ownedHeroes.Contains(hero))
                _ownedHeroes.Remove(hero);
        }

        public void AddSelectedHero(string guid)
        {
            var hero = _ownedHeroes.FirstOrDefault(hero => hero.Guid == guid);
            if (hero != null)
                _selectedHeroes.Add(hero);
        }

        public void RemoveSelectedHero(string guid)
        {
            var hero = _selectedHeroes.FirstOrDefault(hero => hero.Guid == guid);
            if (hero != null)
                _selectedHeroes.Remove(hero);
        }

        public HeroAttributes GetSelectedHero(string guid)
        {
            var attributes =
                _selectedHeroes.FirstOrDefault(hero =>
                    hero.Guid == guid);
            return attributes;
        }

        public HeroAttributes GetOwnedHero(string guid)
        {
            var attributes =
                _ownedHeroes.FirstOrDefault(hero =>
                    hero.Guid == guid);
            return attributes;
        }
    }
}