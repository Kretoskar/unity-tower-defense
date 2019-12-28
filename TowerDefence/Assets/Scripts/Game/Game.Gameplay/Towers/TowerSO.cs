using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Towers
{
    /// <summary>
    /// Tower data
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Tower", fileName = "Tower")]
    public class TowerSO : ScriptableObject
    {
        [SerializeField]
        private int _id;

        [SerializeField]
        private string _name = "Tower";

        [SerializeField]
        [Range(1, 100)]
        private float _damage = 10;

        [SerializeField]
        [Range(0.1f, 1)]
        private float _timeBetweenShots = 1;

        [SerializeField]
        [Range(1, 20)]
        private float _range = 3;

        public float MaxDamage { get => 100; }
        public float MaxSpeed { get => 1; }
        public float MaxRange { get => 20; }

        [SerializeField]
        private Sprite _towerImage = null;

        [SerializeField]
        private GameObject _prefab = null;

        [SerializeField]
        private GameObject _projectilePrefab = null;

        public GameObject Prefab { get => _prefab; set => _prefab = value; }
        public Sprite TowerImage { get => _towerImage; set => _towerImage = value; }
        public float ShotsPerSecond { get => _timeBetweenShots; set => _timeBetweenShots = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public string Name { get => _name; set => _name = value; }
        public int Id { get => _id; set => _id = value; }
        public float Range { get => _range; set => _range = value; }
        public GameObject ProjectilePrefab { get => _projectilePrefab; set => _projectilePrefab = value; }
    }
}