using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Mobs
{
    [CreateAssetMenu(menuName = "ScriptableObject/Mob Spawner", fileName = "Mob Spawner")]
    public class MobSpawnerSO : ScriptableObject
    {
        [SerializeField]
        private List<MobWaveSO> _mobWaves = null;

        public List<MobWaveSO> MobWaves { get => _mobWaves; set => _mobWaves = value; }
    }
}