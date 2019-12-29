using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class Prop : MonoBehaviour
    {
        [SerializeField]
        private int _xSize;

        [SerializeField]
        private int _zSize;

        public int XSize { get => _xSize; set => _xSize = value; }
        public int ZSize { get => _zSize; set => _zSize = value; }
    }
}