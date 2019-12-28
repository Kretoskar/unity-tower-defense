using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Controllers;

namespace Game.Gameplay.Mobs
{
    /// <summary>
    /// Handles spawning wave of mobs
    /// </summary>
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField]
        private MobSpawnerSO _mobSpawnerSO = null;

        private PathController _pathController;

        private bool _waveEnded = true;

        private void Start()
        {
            _waveEnded = true;
            _pathController = FindObjectOfType<PathController>();
        }

        private void Update()
        {
            if(_waveEnded)
            {
                _waveEnded = false;
                SpawnWave();
            }
        }

        /// <summary>
        /// Spawn a random wave of mobs
        /// </summary>
        public void SpawnWave()
        {
            int waveIndex = UnityEngine.Random.Range(0, _mobSpawnerSO.MobWaves.Count);
            StartCoroutine(StartSpawningCoroutine(waveIndex));
        }

        /// <summary>
        /// Start spawning waves
        /// </summary>
        /// <param name="waveIndex">index of the wave to spawn</param>
        private IEnumerator StartSpawningCoroutine(int waveIndex)
        {
            
            yield return new WaitForSeconds(_mobSpawnerSO.TimeBetweenWaveSpawns);
           StartCoroutine( SpawnCoroutine(_mobSpawnerSO.MobWaves[waveIndex], _mobSpawnerSO.MobWaves[waveIndex].NumberOfMobs));
        }

        /// <summary>
        /// Spawn a single wave of mobs
        /// </summary>
        /// <param name="currentWave">current wave type</param>
        /// <param name="mobCount">no. of mobs to spawn</param>
        /// <param name="currentMob">index of current mob</param>
        /// <returns></returns>
        private IEnumerator SpawnCoroutine(MobWaveSO currentWave, int mobCount, int currentMob = 0)
        {
            _waveEnded = false;
            int mobIndex = UnityEngine.Random.Range(0, currentWave.MobTypes.Count);
            GameObject mobGO = Instantiate(currentWave.MobTypes[mobIndex].Prefab, Vector3.zero, Quaternion.identity);
            Mob mob = mobGO.GetComponent<Mob>();
            mobGO.name = mobGO.name + currentMob;
            if(mob != null)
            {
                mob.MobSO = currentWave.MobTypes[mobIndex];
            }
            yield return new WaitForSeconds(currentWave.TimeInSecondsBetweenSpawns);
            currentMob++;
            if (currentMob <= mobCount)
            {
                StartCoroutine(SpawnCoroutine(currentWave, mobCount, currentMob));
            }
            else
            {
                _waveEnded = true;
            }
        }
    }
}