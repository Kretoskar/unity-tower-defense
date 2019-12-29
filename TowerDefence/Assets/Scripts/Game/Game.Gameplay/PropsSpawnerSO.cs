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
        private List<GameObject> _props = null;

        [SerializeField]
        [Range(0, 10)]
        private int _propsPerTenUnits = 3;
    }
}