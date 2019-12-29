using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    /// <summary>
    /// Sets up a grid for towers to be placed 
    /// </summary>
    public class GridController : MonoBehaviour
    {
        [SerializeField]
        private GridSO _gridSO = null;

        //Injected from scriptable object
        private int _cellX = 0;
        private int _cellY = 0;
        private int _gridX = 0;
        private int _gridY = 0;
        private float _missTolerance = 0;

        private void Awake()
        {
            InjectDataFromSciptableObject();
        }

        /// <summary>
        /// Given a Vector3, the method returns 
        /// the closest grid cell's position to the given position
        /// </summary>
        /// <param name="position">Closest grid cell's position to given position</param>
        /// <returns></returns>
        public Vector3 GetClosestGridPosition(Vector3 position)
        {
            if(position.x >= _gridX + _missTolerance || position.x <= -_missTolerance || position.y >= _gridY + _missTolerance|| position.y <= -_missTolerance)
            {
                Debug.Log("This position is out of range, it will be given (0,0) position");
                return Vector3.zero;
            }

            return new Vector3((float)Math.Round(position.x), position.y, Mathf.Round(position.z));
        }

        /// <summary>
        /// Set this class's variables to the ones from scriptable object
        /// </summary>
        private void InjectDataFromSciptableObject()
        {
            _cellX = _gridSO.CellX;
            _cellY = _gridSO.CellY;
            _gridX = _gridSO.GridX;
            _gridY = _gridSO.GridY;
            _missTolerance = _gridSO.MissTolerance;
        }
    }
}