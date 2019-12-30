using Game.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Items
{
    public class Apple : MonoBehaviour, IItem
    {
        [SerializeField]
        private ItemSO _itemSO = null;

        public int Id => _itemSO.Id;
        public string Name => _itemSO.name;
        public string Desc => _itemSO.Desc;
        public GameObject ClickedObject { get; set; }
        public Vector3 ClickedPosition { get; set; }
        public Sprite Image { get => _itemSO.Image; }

        public void Choose()
        {
        }

        public void ConstEffect()
        {
        }

        public void Use()
        {
            FindObjectOfType<PlayerStats>().Hunger += 10;
            FindObjectOfType<Inventory>().RemoveItem(Id);
        }
    }
}
