using Game.Controllers;
using Game.Gameplay.Mobs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Towers
{
    public class Tower : MonoBehaviour
    {
        private Mob _currentMob;
        private List<Mob> _mobs;
        private TowerSO _towerSO;
        private PathController _pathController;
        private Vector3 _lastWaypointPosition;

        public TowerSO TowerSO { get => _towerSO; set => _towerSO = value; }

        private void Start()
        {
            _pathController = FindObjectOfType<PathController>();
            _lastWaypointPosition = _pathController.WayPoints[_pathController.WayPoints.Count - 1];
            StartCoroutine(ShootCoroutine());
        }

        private void Update() { 
            FindMob();
            LookAtMob();
        }

        private void LookAtMob()
        {
            if (_currentMob != null)
            {
                Vector3 lookAtPosition = new Vector3(_currentMob.transform.position.x, transform.position.y, _currentMob.transform.position.z);
                transform.LookAt(_currentMob.transform);
            }
        }

        private void FindMob()
        {
            _mobs = new List<Mob>(FindObjectsOfType<Mob>());
            foreach (var mob in _mobs)
            {
                if (_currentMob == null)
                {
                    _currentMob = mob;
                    continue;
                }
                if (Vector3.Distance(transform.position, mob.gameObject.transform.position) <= _towerSO.Range)
                {
                    if(Vector3.Distance(mob.transform.position, _lastWaypointPosition)
                        < Vector3.Distance(_currentMob.transform.position, _lastWaypointPosition))
                    {
                        _currentMob = mob;
                    }
                }
            }
        }

        private IEnumerator ShootCoroutine()
        {
            if (_currentMob != null)
            {
                GameObject projectileGO = Instantiate(_towerSO.ProjectilePrefab, transform.position, Quaternion.identity);
                Projectile projectile = projectileGO.GetComponent<Projectile>();
                if (projectile != null)
                {
                    projectile.Target = _currentMob.gameObject;
                    projectile.Damage = (int)_towerSO.Damage;
                }
            }
            yield return new WaitForSeconds(_towerSO.ShotsPerSecond);
            StartCoroutine(ShootCoroutine());
        }
    }
}
