using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Mobs
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField]
        private MobSpawnerSO _mobSpawnerSO;

        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            foreach(var wave in _mobSpawnerSO.MobWaves)
            {
                for(int i = 0; i < wave.NumberOfMobs; i++)
                {
                    int mobIndex = UnityEngine.Random.Range(0, wave.MobTypes.Count);
                    Instantiate(wave.MobTypes[mobIndex].Prefab, new Vector3(0, i), Quaternion.identity);
                }
            }
        }
    }
}