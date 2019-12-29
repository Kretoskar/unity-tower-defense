using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Props spawner data
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/PropsSpawner", fileName = "PropsSpawner")]
    public class PropsSpawnerSO : ScriptableObject
    {
        [SerializeField]
        private List<Prop> _props = null;

        [SerializeField]
        [Range(0, 100)]
        private int _propOccurChance = 3;

        public List<Prop> Props { get => _props; set => _props = value; }
        public int PropOccurChance { get => _propOccurChance; set => _propOccurChance = value; }
    }
}