using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Gameplay.Towers;


namespace Game.Controllers
{
    /// <summary>
    /// Contains methods for the UI buttons
    /// </summary>
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Image _healthBar;

        private TowerSpawner _towerSpawner;
        private PlayerStats _playerStats;

        private void Start()
        {
            _playerStats = FindObjectOfType<PlayerStats>();
            _towerSpawner = FindObjectOfType<TowerSpawner>();
            _playerStats.HealthChanged += UpdateHealthBar;
        }

        public void UpdateHealthBar(int currHealth)
        {
            int maxHealth = _playerStats.MaxHealth;
            float fillAmount = ((float)currHealth / (float)maxHealth);
            print(fillAmount);
            _healthBar.fillAmount = fillAmount;
        }

        /// <summary>
        /// Choose a tower to select
        /// </summary>
        /// <param name="index">index of the tower to select</param>
        public void SelectTower(int index)
        {
            _towerSpawner.SelectedTower = index - 1;
        }
    }
}
