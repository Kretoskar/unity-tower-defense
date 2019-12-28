using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Controllers;
using System;

namespace Game.Gameplay.Mobs
{
    /// <summary>
    /// Mob behaviour
    /// </summary>
    public class Mob : MonoBehaviour
    {
        protected float _waypointTolerance = .1f;
        private int _currentWaypointIndex;
        private int _health;
        private int _maxHealth;
        protected MobSO _mobSO;
        protected PathController _pathController = null;

        public Action Death;

        public MobSO MobSO { get => _mobSO; set => _mobSO = value; }
        public int Health {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
                if(_health <= 0)
                {
                    Die();
                }
            }
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            Move();
        }

        /// <summary>
        /// Initialize properties 
        /// </summary>
        protected virtual void Initialize()
        {
            Health = (int)_mobSO.Health;
            _pathController = FindObjectOfType<PathController>();
            _currentWaypointIndex = 0;
            transform.position = _pathController.WayPoints[_currentWaypointIndex];
        }

        /// <summary>
        /// Move through waypoints
        /// </summary>
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

        /// <summary>
        /// Behaviour on reaching end of the path 
        /// </summary>
        protected void ReachedEndOfPath()
        {
            Die();
        }

        /// <summary>
        /// Check if current position is close enough 
        /// to a waypoint, to change the waypoint
        /// </summary>
        /// <returns></returns>
        protected virtual bool ReachedWaypoint()
        {
            return Vector3.Distance(transform.position, _pathController.WayPoints[_currentWaypointIndex]) < _waypointTolerance;
        }

        /// <summary>
        /// Invoke death action and destroy itself on death
        /// </summary>
        private void Die()
        {
            Death?.Invoke();
            Destroy(gameObject);
        }
    }

}