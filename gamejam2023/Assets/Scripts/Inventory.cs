using System;
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
        public Dictionary<int, Item> inventoryItems = new Dictionary<int, Item>();


        private void Start()
        {
            _inventoryUI = FindObjectOfType<InventoryUI>();
            _inventoryUI.InitInventory(InventorySize, defaultSlotSprite);
            inventoryItems = new Dictionary<int, Item>();
            //for(int i=0;i<InventorySize;i++)
            //{
            //    inventoryItems.Add(i, new Item());
            //}
        }

        public bool IsInventoryFull()
        {
            if (inventoryItems.Count >= InventorySize)
            {
                return true;
            }

            return false;
        }

        public void NewItemCollected(Item item)
        {
            if (inventoryItems.Count >= InventorySize)
            {
                Debug.LogError("Inventory Full");
                return;
            }
            int nextIndex = GetNextFreeInventoryItemIndex();
            if (nextIndex == -1)
            {
                Debug.LogError("Kann nicht sein, Index richtig setztn?");
                return;
            }
            inventoryItems.Add(nextIndex, item);
            _inventoryUI.SetInventorySlot(nextIndex, item);
        }

        public float DropItemAndreturnCostValue(int index, bool destroyItems)
        {
            float returnCost = 0f;
            if (!inventoryItems.ContainsKey(index))
            {
                Debug.LogError("Inventory does not contain Key: " + index);
                return 0f;
            }
            GameObject itemObject = inventoryItems[index].gameObject;
            returnCost = inventoryItems[index].Price;
            _inventoryUI.ResetInventarySlot(index);
            if (destroyItems)
            {
                DestroyItem(index);
            }
            return returnCost;
        }

        public float DropAllBottlesAndGetReturnCost()
        {
            float returnCost = 0f;
            List<int> itemsToDestroy = new List<int>();

            foreach (KeyValuePair<int, Item> itemPair in inventoryItems)
            {
                if (itemPair.Value.ItemType == ItemType.Bottle)
                {
                    itemsToDestroy.Add(itemPair.Key);
                    returnCost += DropItemAndreturnCostValue(itemPair.Key, false);
                }
            }

            foreach (int indexToDestroy in itemsToDestroy)
            {
                DestroyItem(indexToDestroy);
            }

            return returnCost;
        }

        private void DestroyItem(int index)
        {
            Item item = inventoryItems[index];
            inventoryItems.Remove(index);
            Destroy(item);
        }

        private int GetNextFreeInventoryItemIndex()
        {
            for (int i = 0; i < InventorySize; i++)
            {
                if (!inventoryItems.ContainsKey(i))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}