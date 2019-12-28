using Game.Gameplay.Mobs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Handles projectile behaviour
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        private GameObject _target;

        private Vector3 _lastPosition;

        private Mob _targetMob;
        private bool _shouldShoot = true;
        private int _damage = 10;

        public GameObject Target { get => _target; set => _target = value; }
        public int Damage { get => _damage; set => _damage = value; }

        private void Start()
        {
            _targetMob = _target.GetComponent<Mob>();
            _targetMob.Death += StopShooting;
        }

        private void Update()
        {
            MoveToTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Mob mob = other.gameObject.GetComponent<Mob>();
                if (mob != null)
                {
                    mob.Health -= _damage;
                }
            }
        }

        private void OnDestroy()
        {
            _targetMob.Death -= StopShooting;
        }

        /// <summary>
        /// Move to the current mob target
        /// </summary>
        private void MoveToTarget()
        {
            if (_shouldShoot)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * 100);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _lastPosition, Time.deltaTime * 100);
            }
            if(transform.position == _lastPosition)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Set last position to the death position of current mob
        /// </summary>
        private void StopShooting()
        {
            _shouldShoot = false;
            _lastPosition = _target.transform.position;

        }
    }
}
