using Game.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay.Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _allItems = new List<GameObject>();

        private List<IItem> _itemsInInventory = new List<IItem>();

        private int _selectedItemID = -1;

        public IItem SelectedItem {
            get
            {
                if (SelectedItemID >= 0)
                {
                   return _itemsInInventory.Select(n => n).Where(b => b.Id == _selectedItemID).ToList()[0];
                }
                else
                {
                    return null;
                }
            }
        }
        public int SelectedItemID { get => _selectedItemID; set => _selectedItemID = value; }

        private void Start()
        {
            _itemsInInventory.Add(_allItems[0].GetComponent<IItem>());
            FindObjectOfType<UIController>().AddItem(_itemsInInventory[0]);
        }
    }
}