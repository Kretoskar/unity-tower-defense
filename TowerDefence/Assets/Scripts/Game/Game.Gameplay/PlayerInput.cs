using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Controllers;
using System;

namespace Game.Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        private Camera _mainCamera;
        private GridController _gridController;

        public Action<Vector3> PlayerClicked;

        private void Start()
        {
            _gridController = FindObjectOfType<GridController>();
            _mainCamera = Camera.main;
        }

        void Update()
        {
            GetClickedCellPosition();
        }

        private void GetClickedCellPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickPosition = new Vector3();

                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    clickPosition = hit.point;
                }

                clickPosition.y = 0.5f;
                clickPosition = _gridController.GetClosestGridPosition(clickPosition);
                PlayerClicked?.Invoke(clickPosition);
            }
        }
    }

}