using Game.Controllers;
using Game.Gameplay.Mobs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Towers
{
    /// <summary>
    /// Tower related behaviour
    /// </summary>
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

        /// <summary>
        /// Look at the currently aimed mob
        /// </summary>
        private void LookAtMob()
        {
            if (_currentMob != null && Vector3.Distance(transform.position, _currentMob.gameObject.transform.position) <= _towerSO.Range)
            {
                Vector3 lookAtPosition = new Vector3(_currentMob.transform.position.x, transform.position.y, _currentMob.transform.position.z);
                transform.LookAt(_currentMob.transform);
            }
            else
            {
                FindMob();
            }
        }

        /// <summary>
        /// Find a mob to look at
        /// </summary>
        private void FindMob()
        {
            _mobs = new List<Mob>(FindObjectsOfType<Mob>());
            foreach (var mob in _mobs)
            {
                if (_towerSO == null)
                    continue;
                if (Vector3.Distance(transform.position, mob.gameObject.transform.position) <= _towerSO.Range)
                {
                    _currentMob = mob;
                }
                if (Vector3.Distance(transform.position, mob.gameObject.transform.position) <= _towerSO.Range)
                {
                    if(Vector3.Distance(mob.transform.position, _lastWaypointPosition)
                        < Vector3.Distance(_currentMob.transform.position, _lastWaypointPosition))
                    {
                        print("x");
                        _currentMob = mob;
                    }
                }
            }
        }

        /// <summary>
        /// Shoot continously
        /// </summary>
        private IEnumerator ShootCoroutine()
        {
            if (_currentMob != null && Vector3.Distance(transform.position, _currentMob.gameObject.transform.position) <= _towerSO.Range)
            {
                GameObject projectileGO = Instantiate(_towerSO.ProjectilePrefab, transform.position, Quaternion.identity);
                Projectile projectile = projectileGO.GetComponent<Projectile>();
                if (projectile != null)
                {
                    projectile.Target = _currentMob.gameObject;
                    projectile.Damage = (int)_towerSO.Damage;
                }
            }
            if (_towerSO != null)
            {
                yield return new WaitForSeconds(_towerSO.ShotsPerSecond);
            }
            else
            {
                yield return new WaitForSeconds(10);
            }
            StartCoroutine(ShootCoroutine());
        }
    }
}
