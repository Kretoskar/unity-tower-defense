using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Controllers;
using System;
using UnityEngine.EventSystems;

namespace Game.Gameplay
{
    /// <summary>
    /// Provides an action with player's click position
    /// </summary>
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

        /// <summary>
        /// Get closest grid cell's position to the user's click position
        /// </summary>
        private void GetClickedCellPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                Vector3 clickPosition = new Vector3();

                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    clickPosition = hit.point;
                }

                if (hit.transform.gameObject.tag == "Ground")
                {
                    clickPosition.y = 1f;
                    clickPosition = _gridController.GetClosestGridPosition(clickPosition);
                    PlayerClicked?.Invoke(clickPosition);
                }
            }
        }
    }

}