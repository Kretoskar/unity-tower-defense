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

        public int SelectedTower { get; set; }
        public TowerSO SelectedTowerSO
        {
            get
            {
                return _towers[SelectedTower];
            }
        }

        private void Start()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            _playerInput.PlayerClicked += SpawnTurret;
            SelectedTower = -1;
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
            int turretIndex = UnityEngine.Random.Range(0, _towers.Count);
            GameObject towerGO = Instantiate(_towers[SelectedTower].Prefab, new Vector3(position.x, 0.5f, position.z), Quaternion.identity);
            Tower tower = towerGO.GetComponentInChildren<Tower>();
            if (tower != null)
                tower.TowerSO = _towers[SelectedTower];
        }
    }
}