using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Controllers;

namespace Game.Gameplay.Items
{
    public class Axe : MonoBehaviour, IItem
    {
        [SerializeField]
        private ItemSO _itemSO = null;

        public Sprite Image { get => _itemSO.Image; }
        public int Id => _itemSO.Id;
        public string Name => _itemSO.name;
        public string Desc => _itemSO.Desc;
        public GameObject ClickedObject { get; set; }
        public Vector3 ClickedPosition { get; set; }

        public void Choose()
        {            
        }

        public void ConstEffect()
        {
        }

        public void Use()
        {
            if(ClickedObject.tag == "Prop")
            {
                Destroy(ClickedObject);
            }
        }
    }
}
