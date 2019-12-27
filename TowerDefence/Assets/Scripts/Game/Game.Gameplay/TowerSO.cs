using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Tower", fileName = "Tower")]
public class TowerSO : ScriptableObject
{
    [SerializeField]
    private int _id;

    [SerializeField]
    private string _name = "Tower";

    [SerializeField]
    private float _damage = 10;

    [SerializeField]
    private float _shotsPerSecond = 1;

    [SerializeField]
    private Image _towerImage = null;

    [SerializeField]
    private GameObject _prefab = null;

    public GameObject Prefab { get => _prefab; set => _prefab = value; }
    public Image TowerImage { get => _towerImage; set => _towerImage = value; }
    public float ShotsPerSecond { get => _shotsPerSecond; set => _shotsPerSecond = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public string Name { get => _name; set => _name = value; }
    public int Id { get => _id; set => _id = value; }
}
