using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Mobs
{
    [CreateAssetMenu(menuName = "ScriptableObject/Mob Wave", fileName = "Mob Wave")]
    public class MobWaveSO : ScriptableObject
    {
        [SerializeField]
        private List<MobSO> _mobTypes = null;

        [SerializeField]
        [Range(1, 100)]
        private int _numberOfMobs = 10;

        [SerializeField]
        [Range(0.5f, 10)]
        private float _timeInSecondsBetweenSpawns = 1;

        public List<MobSO> MobTypes { get => _mobTypes; set => _mobTypes = value; }
        public int NumberOfMobs { get => _numberOfMobs; set => _numberOfMobs = value; }
        public float TimeInSecondsBetweenSpawns { get => _timeInSecondsBetweenSpawns; set => _timeInSecondsBetweenSpawns = value; }
    }
}