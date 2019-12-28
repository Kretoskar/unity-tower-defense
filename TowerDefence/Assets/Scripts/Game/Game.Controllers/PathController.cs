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
        [SerializeField]
        private List<Vector3> _wayPoints = null;

        public List<Vector3> WayPoints { get => _wayPoints; set => _wayPoints = value; }
    }
}