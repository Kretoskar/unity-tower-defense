using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Controllers
{
    /// <summary>
    /// Player stats data
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Player", fileName = "Player")]
    public class PlayerStatsSO : ScriptableObject
    {
        [SerializeField]
        private string _name = "Tadeusz";

        [SerializeField]
        private Image _avatar = null;

        [SerializeField]
        private int _startingGold = 100;

        [SerializeField]
        private int _maxHealth = 100;

        [SerializeField]
        private int _maxHunger = 100;

        [SerializeField]
        private int _hungerDecreasePerTenSeconds = 10;

        public string Name { get => _name; set => _name = value; }
        public Image Avatar { get => _avatar; set => _avatar = value; }
        public int StartingGold { get => _startingGold; set => _startingGold = value; }
        public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public int MaxHunger { get => _maxHunger; set => _maxHunger = value; }
        public int HungerDecreasePerSecond { get => _hungerDecreasePerTenSeconds; set => _hungerDecreasePerTenSeconds = value; }
    }
}