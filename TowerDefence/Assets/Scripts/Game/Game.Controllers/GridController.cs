using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    public class GridController : MonoBehaviour
    {
        [SerializeField]
        private GridSO _gridSO = null;

        //Injected from scriptable object
        private int _cellX = 0;
        private int _cellY = 0;
        private int _gridX = 0;
        private int _gridY = 0;

        private void Awake()
        {
            InjectDataFromSciptableObject();
        }

        public Vector3 GetClosestGridPosition(Vector3 position)
        {
            if(position.x >= 11 || position.x <= -1 || position.y >= 11 || position.y <=-1)
            {
                Debug.Log("This position is out of range, it will be given (0,0) position");
                return Vector3.zero;
            }

            return new Vector3((float)Math.Round(position.x), (float)Math.Round(position.y), position.z);
        }

        private void InjectDataFromSciptableObject()
        {
            _cellX = _gridSO.CellX;
            _cellY = _gridSO.CellY;
            _gridX = _gridSO.GridX;
            _gridY = _gridSO.GridY;
        }
    }
}