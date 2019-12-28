using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Gameplay.Towers;


namespace Game.Controllers
{
    /// <summary>
    /// Contains methods for the UI buttons
    /// </summary>
    public class UIController : MonoBehaviour
    {
        private TowerSpawner _towerSpawner;

        private void Start()
        {
            _towerSpawner = FindObjectOfType<TowerSpawner>();
        }

        /// <summary>
        /// Choose a tower to select
        /// </summary>
        /// <param name="index">index of the tower to select</param>
        public void SelectTower(int index)
        {
            _towerSpawner.SelectedTower = index - 1;
        }
    }
}
