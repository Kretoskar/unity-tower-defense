using Game.Controllers;
using Game.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    /// <summary>
    /// Spawns static level props
    /// </summary>
    public class PropsSpawner : MonoBehaviour
    {
        [SerializeField]
        private PropsSpawnerSO _propsSpawnerSO = null;

        private LevelGenerator _levelGenerator = null;

        //Injected from scriptable object
        private int _propOccurChance;
        private List<Prop> _props;

        private int _levelWidht = 0;
        private int _levelHeight = 0;
        private GameObject _parent;

        public int LevelWidht { get => _levelWidht; set => _levelWidht = value; }
        public int LevelHeight { get => _levelHeight; set => _levelHeight = value; }

        private void Start()
        {
            _parent = new GameObject("Props");
            _levelGenerator = FindObjectOfType<LevelGenerator>();
            _levelGenerator.LevelGenerated += SpawnProps;
            InjectDataFromScriptableObject();
        }


        private void SpawnProps()
        {
            Vector3 lowerLeftCornerPos = new Vector3(_levelGenerator.CornerPositions[0].x, _levelGenerator.CornerPositions[0].y, _levelGenerator.CornerPositions[0].z);
            Vector3 upperLeftCornerPos = new Vector3(_levelGenerator.CornerPositions[1].x, _levelGenerator.CornerPositions[1].y, _levelGenerator.CornerPositions[1].z);
            Vector3 upperRightCornerPos = new Vector3(_levelGenerator.CornerPositions[2].x, _levelGenerator.CornerPositions[2].y, _levelGenerator.CornerPositions[2].z);
            Vector3 lowerRightCornerPos = new Vector3(_levelGenerator.CornerPositions[3].x, _levelGenerator.CornerPositions[3].y, _levelGenerator.CornerPositions[3].z);

            for (int i = (int)lowerLeftCornerPos.x; i < (int)lowerRightCornerPos.x; i++)
            {
                for (int j = (int)lowerLeftCornerPos.z; j < (int)upperLeftCornerPos.z; j++)
                {
                    bool shouldSpawn = UnityEngine.Random.Range(1, 101) <= _propOccurChance ? true : false;
                    foreach(var pos in _levelGenerator.PathPositions)
                    {
                        if (pos.x == i && pos.z == j)
                            shouldSpawn = false;
                    }
                    if (shouldSpawn)
                    {
                        int whichProp = UnityEngine.Random.Range(0, _props.Count);
                        GameObject prop = Instantiate(_props[whichProp].gameObject, new Vector3(i, 0, j), Quaternion.identity);
                        prop.name = "Prop";
                        prop.transform.parent = _parent.transform;
                    }
                }
            }
        }

        /// <summary>
        /// Set this class's variables to the ones from scriptable object
        /// </summary>
        private void InjectDataFromScriptableObject()
        {
            _props = _propsSpawnerSO.Props;
            _propOccurChance = _propsSpawnerSO.PropOccurChance;
        }
    }
}