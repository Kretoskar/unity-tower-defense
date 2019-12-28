using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private LevelGeneratorSO _levelGeneratorSO;

        private GameObject _parent;
        private GameObject _ground;

        private void Start()
        {
            _parent = new GameObject("Level");
            _ground = Instantiate(_levelGeneratorSO.Ground, new Vector3(0,0), Quaternion.identity);
            _ground.transform.localScale = new Vector3(_levelGeneratorSO.LevelWidth, 1, _levelGeneratorSO.LevelHeight);
            _ground.transform.parent = _parent.transform;
            _ground.name = "Ground";
            _ground.transform.position = new Vector3(_levelGeneratorSO.LevelWidth / 2 + 0.5f, 0, _levelGeneratorSO.LevelHeight / 2 + 0.5f);
        }
    }
}