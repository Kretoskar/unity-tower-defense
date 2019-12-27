using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Gameplay;

namespace Game.Controllers
{
    public class UIController : MonoBehaviour
    {
        private TowerSpawner _towerSpawner;

        private void Start()
        {
            _towerSpawner = FindObjectOfType<TowerSpawner>();
        }

        public void SelectTower(int index)
        {
            _towerSpawner.SelectedTower = index - 1;
        }
    }
}
