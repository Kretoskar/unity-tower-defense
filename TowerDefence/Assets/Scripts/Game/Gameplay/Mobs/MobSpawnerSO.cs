﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Mobs
{
    /// <summary>
    /// List of mob waves and how to spawn them
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Mob Spawner", fileName = "Mob Spawner")]
    public class MobSpawnerSO : ScriptableObject
    {
        [SerializeField]
        private List<MobWaveSO> _mobWaves = null;

        [SerializeField]
        [Range(1, 10)]
        private int _timeBetweenWaveSpawns = 3;

        public List<MobWaveSO> MobWaves { get => _mobWaves; set => _mobWaves = value; }
        public int TimeBetweenWaveSpawns { get => _timeBetweenWaveSpawns; set => _timeBetweenWaveSpawns = value; }
    }
}