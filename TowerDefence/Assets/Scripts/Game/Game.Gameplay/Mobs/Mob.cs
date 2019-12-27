using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Controllers;
using System;

namespace Game.Gameplay.Mobs
{
    public class Mob : MonoBehaviour
    {
        protected float _waypointTolerance = .1f;
        private int _currentWaypointIndex;
        protected MobSO _mobSO;
        protected PathController _pathController = null;

        public MobSO MobSO { get => _mobSO; set => _mobSO = value; }
        public Action Death;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            Move();
        }

        protected virtual void Initialize()
        {
            _pathController = FindObjectOfType<PathController>();
            _currentWaypointIndex = 0;
            transform.position = _pathController.WayPoints[_currentWaypointIndex];
        }

        protected virtual void Move()
        {
            if (ReachedWaypoint())
            {
                _currentWaypointIndex++;
                if(_currentWaypointIndex == _pathController.WayPoints.Count)
                {
                    ReachedEndOfPath();
                    return;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, _pathController.WayPoints[_currentWaypointIndex], _mobSO.Speed * Time.deltaTime);

        }

        private void ReachedEndOfPath()
        {
            Death?.Invoke();
            Destroy(gameObject);
        }

        protected virtual bool ReachedWaypoint()
        {
            return Vector3.Distance(transform.position, _pathController.WayPoints[_currentWaypointIndex]) < _waypointTolerance;
        }
    }

}