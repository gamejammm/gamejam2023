using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Inventory : MonoBehaviour
    {

        public int InventorySize = 6;

        public Sprite defaultSlotSprite;

        private InventoryUI _inventoryUI;

        /// <summary>
        /// index, Item
        /// </summary>
        public Dictionary<int,Item> inventoryItems = new Dictionary<int,Item>();
        

        private void Start()
        {
            _inventoryUI = FindObjectOfType<InventoryUI>();
            _inventoryUI.InitInventory(InventorySize, defaultSlotSprite);
            inventoryItems = new Dictionary<int, Item>();
        }

        public void NewItemCollected(Item item)
        {
            if(inventoryItems.Count>= InventorySize)
            {
                Debug.LogError("Inventory Full");
                return;
            }
            int nextIndex = GetNextFreeInventoryItemIndex();
            if(nextIndex == -1)
            {
                Debug.LogError("Kann nicht sein, Index richtig setztn?");
                return;
            }
            inventoryItems.Add(nextIndex, item);
            _inventoryUI.SetInventorySlot(nextIndex, item);
        }

        public void DropItem(int index)
        {
            if(!inventoryItems.ContainsKey(0))
            {
                Debug.LogError("Inventory does not contain Key: " + index);
                return;
            }
            GameObject itemObject = inventoryItems[index].gameObject;
            inventoryItems.Remove(index);
            _inventoryUI.ResetInventarySlot(index);
            Destroy(itemObject);
        }

        private int GetNextFreeInventoryItemIndex()
        {
            for(int i=0;i<InventorySize;i++) 
            { 
                if(!inventoryItems.ContainsKey(i) ) 
                {
                    return i;
                }
            }
            
            return -1;
        }
    }
}