using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Controllers
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatsSO _playerStatsSO = null;

        private int _currHealth;
        private int _maxHealth;
        private int _currGold;
        private int _startingGold;
        private int _currHunger;
        private int _maxHunger;

        public Action<int> HealthChanged; 
        public Action<int> HungerChanged; 
        public Action<int> GoldChanged; 

        public int Health {
            get
            {
                return _currHealth;
            }
            set
            {
                _currHealth = value;
                if (_currHealth <= 0)
                {
                    _currHealth = 0;
                    Die();
                }
                HealthChanged?.Invoke(_currHealth);
            }
        }

        private void Die()
        {
            SceneManager.LoadScene(0);
        }

        public int Hunger {
            get
            {
                return _currHunger;
            }
            set
            {
                _currHunger = value;
                if (_currHunger < 0)
                    _currHunger = 0;
                HungerChanged?.Invoke(_currHunger);
            }
        }
        public int Gold {
            get
            {
                return _currGold;
            }
            set
            {
                _currGold = value;
                if (_currGold < 0)
                    _currGold = 0;
                GoldChanged?.Invoke(_currGold);
            }
        }
        public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public int StartingGold { get => _startingGold; set => _startingGold = value; }
        public int MaxHunger { get => _maxHunger; set => _maxHunger = value; }

        private void Awake()
        {
            InjectDataFromScriptableObject();
            _currHealth = MaxHealth;
            _currGold = StartingGold;
            _currHunger = MaxHunger;
        }

        private void InjectDataFromScriptableObject()
        {
            MaxHealth = _playerStatsSO.MaxHealth;
            StartingGold = _playerStatsSO.StartingGold;
            MaxHunger = _playerStatsSO.MaxHunger;
        }
    }

}