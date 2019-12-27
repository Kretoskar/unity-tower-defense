using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class TowerSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<TowerSO> _towers = null;

        private PlayerInput _playerInput = null;

        public int SelectedTower { get; set; }

        private void Start()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            _playerInput.PlayerClicked += SpawnTurret;
            SelectedTower = -1;
        }

        private void SpawnTurret(Vector3 position)
        {
            if (position.x == 0 && position.z == 0)
                return;
            if (SelectedTower == -1)
                return;
            int turretIndex = UnityEngine.Random.Range(0, _towers.Count);
            Instantiate(_towers[SelectedTower].Prefab, position, Quaternion.identity);
        }
    }
}