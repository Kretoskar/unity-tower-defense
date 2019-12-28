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
        private GameObject _ground;

        [SerializeField]
        private GameObject _pathTile;

        public int LevelWidth { get => _levelWidth; set => _levelWidth = value; }
        public int LevelHeight { get => _levelHeight; set => _levelHeight = value; }
        public GameObject Ground { get => _ground; set => _ground = value; }
        public GameObject PathTile { get => _pathTile; set => _pathTile = value; }
    }
}