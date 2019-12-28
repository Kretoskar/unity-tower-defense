using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    [CreateAssetMenu(menuName = "ScriptableObject/Level generator", fileName = "Level generator")]
    public class LevelGeneratorSO : ScriptableObject
    {
        [SerializeField]
        [Range(10,100)]
        private int _levelWidth;

        [SerializeField]
        [Range(10, 100)]
        private int _levelHeight;

        [SerializeField]
        [Range(10, 100)]
        private int _percentOfChanceForACurveToOccur = 10;

        [SerializeField]
        [Range(0.01f, 1)]
        private float _pathHeight = 0.1f;

        [SerializeField]
        private GameObject _ground;

        [SerializeField]
        private GameObject _pathTile;

        public int LevelWidth { get => _levelWidth; set => _levelWidth = value; }
        public int LevelHeight { get => _levelHeight; set => _levelHeight = value; }
        public GameObject Ground { get => _ground; set => _ground = value; }
        public GameObject PathTile { get => _pathTile; set => _pathTile = value; }
        public float PathHeight { get => _pathHeight; set => _pathHeight = value; }
        public int CurveChance { get => _percentOfChanceForACurveToOccur; set => _percentOfChanceForACurveToOccur = value; }
    }
}