﻿using Game.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private LevelGeneratorSO _levelGeneratorSO = null;

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
        private GameObject _ground;
        private PathController _pathController;

        public GameObject Ground { get => _ground; set => _ground = value; }

        private void Start()
        {
            _pathController = FindObjectOfType<PathController>();

            _parent = new GameObject("Level");
            SpawnPath();
            SpawnGround();
            FindObjectOfType<CamerMovement>().transform.position = new Vector3(
                _ground.transform.position.x,
                999, //will be clamped in update
                _ground.transform.position.z);
        }

        private void SpawnGround()
        {
            Ground = Instantiate(_levelGeneratorSO.Ground, new Vector3(0, 0), Quaternion.identity);
            Ground.transform.position = new Vector3(_levelGeneratorSO.LevelWidth / 2 + 0.5f, -50, _minZ + (_maxZ - _minZ)/2);
            Ground.transform.localScale = new Vector3(_levelGeneratorSO.LevelWidth, 100,  _maxZ - _minZ + 5);
            Ground.transform.parent = _parent.transform;
            Ground.name = "Ground";
        }

        private void SpawnPath()
        {
            //Spawn path entrance
            _pathParent = new GameObject("Path");
            _pathParent.transform.parent = _parent.transform;
            _entrancePosition = new Vector3(1, _levelGeneratorSO.PathHeight, UnityEngine.Random.Range(1, _levelGeneratorSO.LevelHeight));
            _minZ = _entrancePosition.z;
            _maxZ = _minZ;
            GameObject entrance = Instantiate(_levelGeneratorSO.PathTile, _entrancePosition, Quaternion.identity);
            entrance.name = "Path entrance tile";
            entrance.transform.parent = _pathParent.transform;
            _currentX = 1;
            _currentZ = (int)_entrancePosition.z;
            _tileCount = 1;

            _pathController.WayPoints.Add(new Vector3(_currentX, 1 + _entrancePosition.y, _currentZ));
            bool shouldTurn = false;
            _currentPathWay = PathWay.Right;
            _desiredPathWay = PathWay.None;

            //Spawn path
            do
            {
                //Check if should turn
                if (_currentZ <= 2 && _currentPathWay != PathWay.Right)
                {
                    shouldTurn = true;
                    _desiredPathWay = PathWay.Right;
                }
                else if(_currentZ >= 99 && _currentPathWay != PathWay.Right)
                {
                    shouldTurn = true;
                    _desiredPathWay = PathWay.Right;
                }
                else
                {
                    int probability = _levelGeneratorSO.CurveChance;
                    int randomPercent = UnityEngine.Random.Range(1, 100);
                    shouldTurn = randomPercent <= probability ? true : false;
                }

                //Change path way if should turn
                if(shouldTurn)
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
                            if(_currentZ <= 2)
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
                switch(_currentPathWay)
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
            } while (_currentX < _levelGeneratorSO.LevelWidth);

            //Add last waypoint
            _pathController.WayPoints.Add(new Vector3(_currentX, 1 + _entrancePosition.y, _currentZ));
            if (_currentZ < _minZ)
                _minZ = _currentZ;
            if (_currentZ > _maxZ)
                _maxZ = _currentZ;
        }

        private void SpawnTile(Vector3 position)
        {
            GameObject tile = Instantiate(_levelGeneratorSO.PathTile, position, Quaternion.identity);
            tile.name = "Path tile: " + _tileCount;
            tile.transform.parent = _pathParent.transform;
            _tileCount++;
        }
    }

    public enum PathWay
    {
        Top,
        Right,
        Bottom,
        None
    }
}