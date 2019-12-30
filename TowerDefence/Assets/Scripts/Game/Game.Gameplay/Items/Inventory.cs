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
        public List<GameObject> AllItems { get => _allItems; set => _allItems = value; }

        private void Start()
        {
            foreach (var item in AllItems)
            {
                _itemsInInventory.Add(item.GetComponent<IItem>());
                FindObjectOfType<UIController>().AddItem(item.GetComponent<IItem>());
            }
        }

        public void RemoveItem(int itemID)
        {
            _itemsInInventory.RemoveAll(x => x.Id == itemID);
            SelectedItemID = -1;
            FindObjectOfType<UIController>().RemoveItem(itemID);
        }
    }
}