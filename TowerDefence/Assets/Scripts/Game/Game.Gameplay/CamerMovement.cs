using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class CamerMovement : MonoBehaviour
    {
        [SerializeField]
        [Range(1,100)]
        private float _speed = 10;

        [SerializeField]
        [Range(1,100)]
        private float _scrollSpeed = 30;

        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float z = Input.mouseScrollDelta.y;
            transform.Translate(new Vector3(h * _speed, v * _speed, z * _scrollSpeed) * Time.deltaTime);
        }
    }
}
