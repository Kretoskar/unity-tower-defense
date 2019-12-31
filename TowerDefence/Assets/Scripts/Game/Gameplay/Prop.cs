using Game.Controllers;
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

        [SerializeField]
        private int _costToDestroy = 100;

        public int XSize { get => _xSize; set => _xSize = value; }
        public int ZSize { get => _zSize; set => _zSize = value; }
        public int CostToDestroy { get => _costToDestroy; set => _costToDestroy = value; }

        public void Sell()
        {
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            if (playerStats.Gold >= _costToDestroy)
            {
                playerStats.Gold -= _costToDestroy;
                Destroy(gameObject);
            }
        }
    }
}