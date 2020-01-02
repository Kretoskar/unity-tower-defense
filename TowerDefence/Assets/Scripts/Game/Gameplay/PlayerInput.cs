using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Controllers;
using System;
using UnityEngine.EventSystems;
using Game.Gameplay.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// Provides an action with player's click position
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        private Camera _mainCamera;
        private GridController _gridController;
        private Inventory _inventory;

        public Action<Vector3> PlayerClicked;
        public Action<Prop> PlayerRightClicked;

        private void Start()
        {
            _gridController = FindObjectOfType<GridController>();
            _inventory = FindObjectOfType<Inventory>();
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
            else if (Input.GetMouseButtonDown(1))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                Vector3 clickPosition = new Vector3();
                GameObject clickedObject;

                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    clickPosition = hit.point;
                    clickPosition.y = 1f;
                    clickPosition = _gridController.GetClosestGridPosition(clickPosition);
                    clickedObject = hit.transform.gameObject;
                    if(_inventory.SelectedItem != null)
                    {
                        _inventory.SelectedItem.ClickedObject = clickedObject;
                        _inventory.SelectedItem.ClickedPosition = clickPosition;
                        _inventory.SelectedItem.Use();
                    }

                }
            }
        }
    }

}