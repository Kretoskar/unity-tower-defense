using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class CamerMovement : MonoBehaviour
    {
        [SerializeField]
        private CameraSO _cameraSO = null;

        private float _speed;
        private float _scrollSpeed;
        private int _lowClamp;
        private int _highClamp;
        private int _lowScrollClamp;
        private int _highScrollClamp;

        private void Start()
        {
            _speed = _cameraSO.Speed;
            _scrollSpeed = _cameraSO.ScrollSpeed;
            _lowClamp = _cameraSO.LowClamp;
            _highClamp = _cameraSO.HighClamp;
            _lowScrollClamp = _cameraSO.ScrollLowClamp;
            _highScrollClamp = _cameraSO.ScrollHighClamp;
        }

        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float z = Input.mouseScrollDelta.y;
            transform.Translate(new Vector3(h * _speed, v * _speed, z * _scrollSpeed) * Time.deltaTime);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _lowClamp, _highClamp), 
                Mathf.Clamp(transform.position.y, _lowScrollClamp, _highScrollClamp), 
                Mathf.Clamp(transform.position.z, _lowClamp, _highClamp));
        }
    }
}
