using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Gameplay.Towers;


namespace Game.Controllers
{
    /// <summary>
    /// Contains methods for the UI behavioru
    /// </summary>
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Image _healthBar = null;
        [SerializeField]
        private Image _turretAvatar = null;
        [SerializeField]
        private Image _damageBar = null;
        [SerializeField]
        private Image _speedBar = null;
        [SerializeField]
        private Image _rangeBar = null;
        [SerializeField]
        private Image _towersPanel = null;
        [SerializeField]
        private TextMeshProUGUI _goldText = null;
        [SerializeField]
        private GameObject _towerButtonPrefab = null;

        private TowerSpawner _towerSpawner;
        private PlayerStats _playerStats;

        public Image TowersPanel { get => _towersPanel; set => _towersPanel = value; }

        private void Start()
        {
            _playerStats = FindObjectOfType<PlayerStats>();
            _towerSpawner = FindObjectOfType<TowerSpawner>();
            _playerStats.HealthChanged += UpdateHealthBar;
            _playerStats.GoldChanged += UpdateGold;
            _goldText.text = _playerStats.Gold.ToString();
            SpawnTowerButtons();
        }

        /// <summary>
        /// Update player gold UI
        /// </summary>
        /// <param name="gold">just here so that it matches the action</param>
        public void UpdateGold(int gold)
        {
            _goldText.text = _playerStats.Gold.ToString();
        }

        /// <summary>
        /// Update player health UI
        /// </summary>
        /// <param name="currHealth">current player health</param>
        public void UpdateHealthBar(int currHealth)
        {
            int maxHealth = _playerStats.MaxHealth;
            _healthBar.fillAmount = ((float)currHealth / (float)maxHealth);
        }

        /// <summary>
        /// Choose a tower to select
        /// </summary>
        /// <param name="index">index of the tower to select</param>
        public void SelectTower(int index)
        {
            _towerSpawner.SelectedTower = index - 1;
            TowerSO towerSO = _towerSpawner.SelectedTowerSO;
            _turretAvatar.sprite = towerSO.TowerImage;
            _damageBar.fillAmount = ((float)towerSO.Damage / (float)towerSO.MaxDamage);
            _speedBar.fillAmount = ((towerSO.MaxSpeed - (float)towerSO.ShotsPerSecond) / (float)towerSO.MaxSpeed);
            _rangeBar.fillAmount = ((float)towerSO.Range / (float)towerSO.MaxRange);
        }

        /// <summary>
        /// Spawns buttons with tower images,
        /// that are used to spawn concrete turrets
        /// </summary>
        private void SpawnTowerButtons()
        {
            foreach(var tower in _towerSpawner.Towers)
            {
                Sprite towerImage = tower.TowerImage;
                int id = tower.Id;
                GameObject towerBG = Instantiate(_towerButtonPrefab, _towersPanel.GetComponentInChildren<VerticalLayoutGroup>().transform); ;
                GameObject towerBTN = towerBG.GetComponentInChildren<Button>().gameObject;
                towerBTN.GetComponent<Image>().sprite = towerImage;
                towerBTN.GetComponent<Button>().onClick.AddListener(() => SelectTower(id));
            }
        }
    }
}
