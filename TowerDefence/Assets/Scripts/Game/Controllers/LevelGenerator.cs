using Game.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    /// <summary>
    /// Generates the ground for the towers to be placed 
    /// and path for mobs
    /// </summary>
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private LevelGeneratorSO _levelGeneratorSO = null;

        //Injected from the scriptable object 
        private int _levelWidth = 0;
        private int _levelHeight = 0;
        private int _percentOfChanceForACurveToOccur = 0;
        private float _pathHeight = 0;
        private float _levelDepth = 0;
        private GameObject _ground = null;
        private GameObject _pathTile = null;

        private int _tileCount = 0;
        private int _currentX;
        private int _currentZ;
        private float _minZ;
        private float _maxZ;
        private PathWay _currentPathWay;
        private PathWay _desiredPathWay;
        private Vector3 _entrancePosition;
        private GameObject _parent;
        private GameObject _pathParent;
        private PathController _pathController;
        private List<Vector3> _pathPositions;

        public GameObject Ground { get => _ground; set => _ground = value; }
        public List<Vector3> PathPositions { get => _pathPositions; set => _pathPositions = value; }
        /// <summary>
        /// Corner positions, 
        /// 0 - Lower left
        /// 1 - Upper left 
        /// 2 - Upper right
        /// 3 - Lower right
        /// </summary>
        public List<Vector3> CornerPositions { get; set; }
        public int LevelWidth { get => _levelWidth; set => _levelWidth = value; }
        public int LevelHeight { get => _levelHeight; set => _levelHeight = value; }

        public Action LevelGenerated;

        private void Start()
        {
            CornerPositions = new List<Vector3>();

            InjectDataFromScriptableObject();

            _pathPositions = new List<Vector3>();
            _pathController = FindObjectOfType<PathController>();

            SpawnParent();
            SpawnPath();
            SpawnGround();

            //Set camera position on top of the ground
            FindObjectOfType<CamerMovement>().transform.position = new Vector3(
                _ground.transform.position.x,
                999, //will be clamped in update
                _ground.transform.position.z);
        }

        /// <summary>
        /// Spawn the parent GameObject for the ground and the tiles
        /// </summary>
        private void SpawnParent()
        {
            _parent = new GameObject("Level");
        }

        /// <summary>
        /// Proceduraly spawn the path for the mobs to move at
        /// also set waypoints for the path controller so that 
        /// the mobs know when to turn 
        /// </summary>
        private void SpawnPath()
        {
            //Spawn path entrance
            _pathParent = new GameObject("Path");
            _pathParent.transform.parent = _parent.transform;
            _entrancePosition = new Vector3(1, _pathHeight, UnityEngine.Random.Range(1, LevelHeight));
            _minZ = _entrancePosition.z;
            _maxZ = _minZ;
            GameObject entrance = Instantiate(_pathTile, _entrancePosition, Quaternion.identity);
            entrance.name = "Path entrance tile";
            entrance.transform.parent = _pathParent.transform;
            _currentX = 1;
            _currentZ = (int)_entrancePosition.z;
            _tileCount = 1;
            _pathPositions.Add(_entrancePosition);

            //Add first waypoint
            _pathController.WayPoints.Add(new Vector3(_currentX, 1 + _entrancePosition.y, _currentZ));

            //Serup current vars for the loop 
            //to know what to do 
            bool shouldTurn = false;
            _currentPathWay = PathWay.Right;
            _desiredPathWay = PathWay.None;

            //Spawn path
            do
            {
                //Check if should turn depending on the chances to turn 
                if (_currentZ <= 2 && _currentPathWay != PathWay.Right)
                {
                    shouldTurn = true;
                    _desiredPathWay = PathWay.Right;
                }
                else if (_currentZ >= 99 && _currentPathWay != PathWay.Right)
                {
                    shouldTurn = true;
                    _desiredPathWay = PathWay.Right;
                }
                else
                {
                    int probability = _percentOfChanceForACurveToOccur;
                    int randomPercent = UnityEngine.Random.Range(1, 100);
                    shouldTurn = randomPercent <= probability ? true : false;
                }

                //Change path way if should turn
                if (shouldTurn)
                {
                    //Add a waypoint, so that the mobs know when to turn
                    _pathController.WayPoints.Add(new Vector3(_currentX, 1 + _entrancePosition.y, _currentZ));

                    //Sanity check for going out of ground
                    if (_desiredPathWay != PathWay.None)
                    {
                        _currentPathWay = _desiredPathWay;
                    }
                    else
                    {
                        if (_currentPathWay == PathWay.Top || _currentPathWay == PathWay.Bottom)
                        {
                            _currentPathWay = PathWay.Right;
                        }
                        else
                        {
                            int rand = UnityEngine.Random.Range(0, 2);
                            _currentPathWay = rand == 0 ? PathWay.Top : PathWay.Bottom;
                            if (_currentZ <= 2)
                            {
                                _currentPathWay = PathWay.Top;
                            }
                            else if (_currentZ >= 99)
                            {
                                _currentPathWay = PathWay.Bottom;
                            }
                        }
                    }
                }

                Vector3 position = Vector3.zero;

                //Set position according to current way
                switch (_currentPathWay)
                {
                    case PathWay.Right:
                        _currentX += 1;
                        break;
                    case PathWay.Top:
                        _currentZ += 1;
                        break;
                    case PathWay.Bottom:
                        _currentZ -= 1;
                        break;
                }

                //Spawn the tile
                SpawnTile(new Vector3(_currentX, _entrancePosition.y, _currentZ));

                //Reset the vars
                shouldTurn = false;
                _desiredPathWay = PathWay.None;

                if (_currentZ < _minZ)
                    _minZ = _currentZ;
                if (_currentZ > _maxZ)
                    _maxZ = _currentZ;
            } while (_currentX < LevelWidth);

            //Add last waypoint
            _pathController.WayPoints.Add(new Vector3(_currentX, 1 + _entrancePosition.y, _currentZ));
            if (_currentZ < _minZ)
                _minZ = _currentZ;
            if (_currentZ > _maxZ)
                _maxZ = _currentZ;
        }

        /// <summary>
        /// Spawn the ground, where turrets are meant to be spawned
        /// </summary>
        private void SpawnGround()
        {
            Ground = Instantiate(_ground, new Vector3(0, 0), Quaternion.identity);
            Vector3 groundPos = new Vector3(
                LevelWidth / 2 + 0.5f,     // + 0.5f so that the tiles have integer positions
                (-1) * _levelDepth / 2,     // make the center according to the level depth
                _minZ + (_maxZ - _minZ) / 2); // set the ground position according to path coordinates
            Ground.transform.position = groundPos;
            _levelHeight = (int)(_maxZ - _minZ + 5);
            Ground.transform.localScale = new Vector3(LevelWidth, _levelDepth,  _levelHeight);    //shrink the ground according to the path coordinates
            Ground.transform.parent = _parent.transform;
            Ground.name = "Ground";
            Ground.tag = "Ground";

            //Add corners
            CornerPositions.Add(new Vector3(1, 0, groundPos.z - _levelHeight /2 + 0.5f));
            CornerPositions.Add(new Vector3(1, 0, groundPos.z + _levelHeight /2 - 0.5f));
            CornerPositions.Add(new Vector3(_levelWidth, 0, groundPos.z + _levelHeight /2 - 0.5f));
            CornerPositions.Add(new Vector3(_levelWidth, 0, groundPos.z - _levelHeight /2 + 0.5f));
            LevelGenerated?.Invoke();
        }

        /// <summary>
        /// Spawn a single path tile
        /// </summary>
        /// <param name="position">position, where to spawn</param>
        private void SpawnTile(Vector3 position)
        {
            GameObject tile = Instantiate(_pathTile, position, Quaternion.identity);
            tile.name = "Path tile: " + _tileCount;
            tile.transform.parent = _pathParent.transform;
            _tileCount++;
            _pathPositions.Add(position);
        }

        /// <summary>
        /// Set this class's variables to be equal to the one from 
        /// the scriptable object 
        /// </summary>
        private void InjectDataFromScriptableObject()
        {
            LevelWidth = _levelGeneratorSO.LevelWidth;
            LevelHeight = _levelGeneratorSO.LevelHeight;
            _percentOfChanceForACurveToOccur = _levelGeneratorSO.CurveChance;
            _pathHeight = _levelGeneratorSO.PathHeight;
            _ground = _levelGeneratorSO.Ground;
            _pathTile = _levelGeneratorSO.PathTile;
            _levelDepth = _levelGeneratorSO.LevelDepth;
        }
    }

    /// <summary>
    /// Where is the path currenlty heading
    /// </summary>
    public enum PathWay
    {
        Top,
        Right,
        Bottom,
        None
    }
}