using Game.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay
{
    /// <summary>
    /// Camera movement and zoom 
    /// </summary>
    public class CamerMovement : MonoBehaviour
    {
        [SerializeField]
        private CameraSO _cameraSO = null;
        [SerializeField]
        private Camera _childCamera = null;

        //Injected from scriptable object
        private float _speed;
        private float _scrollSpeed;
        private int _lowClamp;
        private int _highClamp;
        private int _lowScrollClamp;
        private int _highScrollClamp;
        private int _maxRotation;
        private float _percentOfRotation;

        //Input
        private float h;
        private float v;
        private float y;

        private RectTransform _towersPanel;
        private LevelGenerator _levelGenerator;

        private void Start()
        {
            InjectDataFromScriptableObject();
            _towersPanel = FindObjectOfType<UIController>().TowersPanel.rectTransform;
        }

        private void Update()
        {
            GetInput();
            MoveCamera();
            RotateCamera();
        }

        /// <summary>
        /// get user input
        /// </summary>
        private void GetInput()
        {
            Vector2 mousePosition = Input.mousePosition;
            if (!RectTransformUtility.RectangleContainsScreenPoint(_towersPanel, mousePosition))
            {
                y = Input.mouseScrollDelta.y;
            }
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }

        /// <summary>
        /// Move tha camera on x and z axis according 
        /// user's input
        /// </summary>
        private void MoveCamera()
        {
            transform.Translate(new Vector3(h * _speed, -y * _scrollSpeed, v * _speed) * Time.deltaTime);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _lowClamp, _highClamp),
                Mathf.Clamp(transform.position.y, _lowScrollClamp, _highScrollClamp),
                Mathf.Clamp(transform.position.z, _lowClamp, _highClamp));
        }

        /// <summary>
        /// Roatate camera according to y position
        /// the closer the camera is to the ground, 
        /// the more it rotates
        /// </summary>
        private void RotateCamera()
        {
            float percentOfRotation = (transform.position.y - _lowScrollClamp) / (_highScrollClamp - _lowScrollClamp);
            float rot = 90 - _maxRotation * (1 - percentOfRotation);
            _childCamera.transform.rotation = Quaternion.Euler(rot, 0, 0);
        }

        /// <summary>
        /// Set class's variables to the ones from scriptable object
        /// </summary>
        private void InjectDataFromScriptableObject()
        {
            _speed = _cameraSO.Speed;
            _scrollSpeed = _cameraSO.ScrollSpeed;
            _lowClamp = _cameraSO.LowClamp;
            _highClamp = _cameraSO.HighClamp;
            _lowScrollClamp = _cameraSO.ScrollLowClamp;
            _highScrollClamp = _cameraSO.ScrollHighClamp;
            _maxRotation = _cameraSO.MaxRotation;
        }
    }
}
