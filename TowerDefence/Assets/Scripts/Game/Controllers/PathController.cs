using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    /// <summary>
    /// Sets up path's waypoints
    /// </summary>
    public class PathController : MonoBehaviour
    {
        private List<Vector3> _wayPoints;

        public List<Vector3> WayPoints { get => _wayPoints; }

        private void Awake()
        {
            _wayPoints = new List<Vector3>();
        }
    }
}