using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Controllers;

namespace Game.Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        private Camera _mainCamera;
        private GridController _gridController;

        private void Start()
        {
            _gridController = FindObjectOfType<GridController>();
            _mainCamera = Camera.main;
        }

        void Update()
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

                GameObject spawnedCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                spawnedCube.transform.position = clickPosition;
            }
        }
    }

}