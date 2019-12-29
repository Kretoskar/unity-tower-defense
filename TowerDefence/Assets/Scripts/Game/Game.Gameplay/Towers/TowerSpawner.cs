using Game.Controllers;
using Game.Gameplay.Towers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Towers
{
    /// <summary>
    /// Provides method for spawning towers on the grid
    /// </summary>
    public class TowerSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<TowerSO> _towers = null;

        private PlayerInput _playerInput = null;
        private PlayerStats _playerStats = null;

        public int SelectedTower { get; set; }
        public TowerSO SelectedTowerSO
        {
            get
            {
                return Towers[SelectedTower];
            }
        }

        public List<TowerSO> Towers { get => _towers; set => _towers = value; }

        private void Start()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            _playerStats = FindObjectOfType<PlayerStats>();
            _playerInput.PlayerClicked += SpawnTurret;
            SelectedTower = -1;
        }

        /// <summary>
        /// Set tower index, 
        /// used by UI
        /// </summary>
        /// <param name="index">index of selected tower</param>
        public void SetTower(int index)
        {
            SelectedTower = index;
        }

        /// <summary>
        /// Instantiate a tower at the closest 
        /// grid cell's position to player's click position
        /// </summary>
        /// <param name="position">click position</param>
        private void SpawnTurret(Vector3 position)
        {
            if (position.x == 0 && position.z == 0)
                return;
            if (SelectedTower == -1)
                return;
            if (_playerStats.Gold < Towers[SelectedTower].Cost)
                return;
            _playerStats.Gold -= Towers[SelectedTower].Cost;
            int turretIndex = UnityEngine.Random.Range(0, Towers.Count);
            GameObject towerGO = Instantiate(Towers[SelectedTower].Prefab, new Vector3(position.x, 0.5f, position.z), Quaternion.identity);
            Tower tower = towerGO.GetComponentInChildren<Tower>();
            if (tower != null)
                tower.TowerSO = Towers[SelectedTower];
        }
    }
}