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

        private void Start()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            _playerInput.PlayerClicked += SpawnTurret;
        }

        private void SpawnTurret(Vector3 position)
        {
            int turretIndex = UnityEngine.Random.Range(0, _towers.Count);
            Instantiate(_towers[turretIndex].Prefab, position, Quaternion.identity);
        }
    }
}