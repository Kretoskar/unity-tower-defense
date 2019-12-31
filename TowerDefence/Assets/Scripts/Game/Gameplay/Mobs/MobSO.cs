using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Mobs
{
    /// <summary>
    /// Mob data
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Mob", fileName = "Mob")]
    public class MobSO : ScriptableObject
    {
        [SerializeField]
        [Range(1,10)]
        private float _speed = 5;

        [SerializeField]
        [Range(1,100000)]
        private float _health = 100;

        [SerializeField]
        [Range(1,100)]
        private int _damageDealtToPlayerOnPathExit = 10;

        [SerializeField]
        [Range(1, 1000)]
        private int _goldAddedToPlayerOnDeath = 10;

        [SerializeField]
        [Range(1, 1000)]
        private int _scoreAddedToPlayerOnDeath = 10;

        [SerializeField]
        private GameObject _prefab = null;

        public GameObject Prefab { get => _prefab; set => _prefab = value; }
        public float Health { get => _health; set => _health = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public int DamageDealtToPlayerOnPathExit { get => _damageDealtToPlayerOnPathExit; set => _damageDealtToPlayerOnPathExit = value; }
        public int GoldAddedToPlayerOnDeath { get => _goldAddedToPlayerOnDeath; set => _goldAddedToPlayerOnDeath = value; }
        public int ScoreAddedToPlayerOnDeath { get => _scoreAddedToPlayerOnDeath; set => _scoreAddedToPlayerOnDeath = value; }
    }
}