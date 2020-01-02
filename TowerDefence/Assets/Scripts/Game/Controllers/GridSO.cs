using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    /// <summary>
    /// Data for the grid
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Grid", fileName = "Grid")]
    public class GridSO : ScriptableObject
    {
        [SerializeField]
        [Range(1, 10)]
        private int _cellX = 1;

        [SerializeField]
        [Range(1, 10)]
        private int _cellY = 1;

        [SerializeField]
        [Range(10, 100)]
        private int _gridX = 10;

        [SerializeField]
        [Range(10, 100)]
        private int _gridY = 10;

        [SerializeField]
        [Range(0.1f, 0.5f)]
        private float _missTolerance = 0.5f;

        public int CellX { get => _cellX; set => _cellX = value; }
        public int CellY { get => _cellY; set => _cellY = value; }
        public int GridX { get => _gridX; set => _gridX = value; }
        public int GridY { get => _gridY; set => _gridY = value; }
        public float MissTolerance { get => _missTolerance; set => _missTolerance = value; }
    }
}
