using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Items
{
    [CreateAssetMenu(menuName = "ScriptableObject/Item", fileName = "Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField]
        private int _id;

        [SerializeField]
        private Sprite _image;

        [SerializeField]
        private string _name;

        [SerializeField]
        private string _desc;

        public Sprite Image { get => _image; set => _image = value; }
        public string Name { get => _name; set => _name = value; }
        public string Desc { get => _desc; set => _desc = value; }
        public int Id { get => _id; set => _id = value; }
    }
}