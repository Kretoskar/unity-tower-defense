using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private LevelGeneratorSO _levelGeneratorSO;

        private int _tileCount = 0;
        private int _currentX;
        private int _currentZ;
        private Vector3 _entrancePosition;
        private GameObject _parent;
        private GameObject _pathParent;
        private GameObject _ground;

        private void Start()
        {
            _parent = new GameObject("Level");
            _ground = Instantiate(_levelGeneratorSO.Ground, new Vector3(0,0), Quaternion.identity);
            _ground.transform.localScale = new Vector3(_levelGeneratorSO.LevelWidth, 1, _levelGeneratorSO.LevelHeight);
            _ground.transform.parent = _parent.transform;
            _ground.name = "Ground";
            _ground.transform.position = new Vector3(_levelGeneratorSO.LevelWidth / 2 + 0.5f, 0, _levelGeneratorSO.LevelHeight / 2 + 0.5f);
            SpawnPath();
        }

        private void SpawnPath()
        {
            //Spawn path entrance
            _pathParent = new GameObject("Path");
            _pathParent.transform.parent = _parent.transform;
            _entrancePosition = new Vector3(1, _levelGeneratorSO.PathHeight, UnityEngine.Random.Range(1, _levelGeneratorSO.LevelHeight));
            GameObject entrance = Instantiate(_levelGeneratorSO.PathTile, _entrancePosition, Quaternion.identity);
            entrance.name = "Path entrance tile";
            entrance.transform.parent = _pathParent.transform;
            _currentX = 1;
            _currentZ = (int)_entrancePosition.z;
            _tileCount = 1;

            bool shouldTurn = false;

            //Spawn path
            do
            {
                if (_currentZ == 1)
                {
                    shouldTurn = true;
                }
                else if(_currentZ == 100)
                {
                    shouldTurn = true;
                }
                else
                {
                    int probability = _levelGeneratorSO.CurveChance;
                    int randomPercent = UnityEngine.Random.Range(1, 100);
                    shouldTurn = randomPercent <= probability ? true : false;
                }
                SpawnTile(new Vector3(_currentX, _entrancePosition.y, _currentZ));
                _currentX++;
                shouldTurn = false;
            } while (_currentX <= _levelGeneratorSO.LevelWidth);
        }

        private void SpawnTile(Vector3 position)
        {
            GameObject tile = Instantiate(_levelGeneratorSO.PathTile, position, Quaternion.identity);
            tile.name = "Path tile: " + _tileCount;
            tile.transform.parent = _pathParent.transform;
            _tileCount++;
        }
    }
}