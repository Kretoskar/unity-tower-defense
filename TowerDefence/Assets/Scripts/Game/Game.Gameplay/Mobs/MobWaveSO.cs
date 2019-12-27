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

        public List<MobSO> MobTypes { get => _mobTypes; set => _mobTypes = value; }
        public int NumberOfMobs { get => _numberOfMobs; set => _numberOfMobs = value; }
    }
}