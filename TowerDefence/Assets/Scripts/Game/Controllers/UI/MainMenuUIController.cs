using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers.UI
{
    /// <summary>
    /// Handles main menu buttons behaviour
    /// </summary>
    public class MainMenuUIController : MonoBehaviour
    {
        /// <summary>
        /// Load the game scene
        /// </summary>
        public void StartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        /// <summary>
        /// Load the high score scene
        /// </summary>
        public void ShowHighScore()
        {
            print("Show high score");
        }

        /// <summary>
        /// Quit the game to desktop
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
