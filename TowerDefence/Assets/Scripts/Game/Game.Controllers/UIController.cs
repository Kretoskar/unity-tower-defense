using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Gameplay.Towers;
using Game.Gameplay.Items;

namespace Game.Controllers
{
    /// <summary>
    /// Contains methods for the UI behavioru
    /// </summary>
    public class UIController : MonoBehaviour
    {
        [Header("Player UI")]
        [SerializeField]
        private Image _healthBar = null;
        [SerializeField]
        private Image _hungerBar = null;

        [Header("Tower UI")]
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
        private GameObject _towerButtonPrefab = null;

        [Header("Item UI")]
        [SerializeField]
        private Image _itemsPanel = null;
        [SerializeField]
        private Image _choosenItemImage = null;
        [SerializeField]
        private Text _choosenItemName = null;
        [SerializeField]
        private Text _choosenItemDesc = null;
        [SerializeField]
        private GameObject _itemButtonPrefab = null;

        [Header("Gold UI")]
        [SerializeField]
        private TextMeshProUGUI _goldText = null;


        private TowerSpawner _towerSpawner;
        private PlayerStats _playerStats;
        private Inventory _inventory;

        public Image TowersPanel { get => _towersPanel; set => _towersPanel = value; }

        private void Start()
        {
            _inventory = FindObjectOfType<Inventory>();
            _playerStats = FindObjectOfType<PlayerStats>();
            _towerSpawner = FindObjectOfType<TowerSpawner>();

            _playerStats.HealthChanged += UpdateHealthBar;
            _playerStats.GoldChanged += UpdateGold;
            _playerStats.HungerChanged += UpdateHunger;

            _goldText.text = _playerStats.Gold.ToString();
            SpawnTowerButtons();
        }

        /// <summary>
        /// Update player hunger UI
        /// </summary>
        public void UpdateHunger(int currHunger)
        {
            int maxHunger = _playerStats.MaxHealth;
            _hungerBar.fillAmount = ((float)currHunger / (float)maxHunger);
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
        /// Choose a item to select and update it's UI
        /// </summary>
        public void SelectItem(int index)
        {
            _inventory.SelectedItemID = index;
            print(index);
            IItem selectedItem = _inventory.SelectedItem;
            _choosenItemImage.sprite = selectedItem.Image;
            _choosenItemName.text = selectedItem.Name;
            _choosenItemDesc.text = selectedItem.Desc;
        }

        /// <summary>
        /// Add item to item panel
        /// </summary>
        /// <param name="item">what item to add</param>
        public void AddItem(IItem item)
        {
            GameObject itemBG = Instantiate(_itemButtonPrefab, _itemsPanel.GetComponentInChildren<VerticalLayoutGroup>().transform);
            GameObject itemBTN = itemBG.GetComponentInChildren<Button>().gameObject;
            itemBTN.GetComponent<Image>().sprite = item.Image;
            itemBTN.GetComponent<Button>().onClick.AddListener(() => SelectItem(item.Id));
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
