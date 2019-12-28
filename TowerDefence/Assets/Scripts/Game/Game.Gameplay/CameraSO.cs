using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "ScriptableObject/Camera", fileName = "Camera")]
    public class CameraSO : ScriptableObject
    {
        [SerializeField]
        [Range(1,100)]
        private int _speed = 10;
        [SerializeField]
        [Range(1, 100)]
        private int _scrollSpeed = 50;
        [SerializeField]
        [Range(-200,200)]
        private int _lowClamp = 1;
        [SerializeField]
        [Range(-200, 200)]
        private int _highClamp = 100;
        [SerializeField]
        [Range(1, 100)]
        private int _scrollLowClamp = 10;
        [SerializeField]
        [Range(1, 100)]
        private int _scrollHighClamp = 100;
        [SerializeField]
        [Range(1, 50)]
        private int _maxRotation = 20;

        public int Speed { get => _speed; set => _speed = value; }
        public int ScrollSpeed { get => _scrollSpeed; set => _scrollSpeed = value; }
        public int LowClamp { get => _lowClamp; set => _lowClamp = value; }
        public int HighClamp { get => _highClamp; set => _highClamp = value; }
        public int ScrollLowClamp { get => _scrollLowClamp; set => _scrollLowClamp = value; }
        public int ScrollHighClamp { get => _scrollHighClamp; set => _scrollHighClamp = value; }
        public int MaxRotation { get => _maxRotation; set => _maxRotation = value; }
    }
}
