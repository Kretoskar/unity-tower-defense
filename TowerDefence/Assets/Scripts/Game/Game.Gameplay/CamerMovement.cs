using Game.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class CamerMovement : MonoBehaviour
    {
        [SerializeField]
        private CameraSO _cameraSO = null;
        [SerializeField]
        private Camera _childCamera = null;

        private float _speed;
        private float _scrollSpeed;
        private int _lowClamp;
        private int _highClamp;
        private int _lowScrollClamp;
        private int _highScrollClamp;
        private int _maxRotation;
        private float _percentOfRotation;

        private LevelGenerator _levelGenerator;

        private void Start()
        {
            InjectDataFromScriptableObject();
        }

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

        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float y = Input.mouseScrollDelta.y;

            transform.Translate(new Vector3(h * _speed, -y * _scrollSpeed, v * _speed) * Time.deltaTime);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _lowClamp, _highClamp), 
                Mathf.Clamp(transform.position.y, _lowScrollClamp, _highScrollClamp), 
                Mathf.Clamp(transform.position.z, _lowClamp, _highClamp));

            float percentOfRotation = (transform.position.y - _lowScrollClamp) / (_highScrollClamp - _lowScrollClamp);
            float rot = 90 - _maxRotation * (1 - percentOfRotation);
            _childCamera.transform.rotation = Quaternion.Euler(rot, 0, 0);
        }
    }
}
