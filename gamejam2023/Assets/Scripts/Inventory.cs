using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Inventory : MonoBehaviour
    {

        public int InventorySize = 6;

        public Sprite defaultSlotSprite;

        private InventoryUI _inventoryUI;

        private GameManager gameManager;

        /// <summary>
        /// index, Item
        /// </summary>
        public Dictionary<int, Item> inventoryItems = new Dictionary<int, Item>();


        private void Start()
        {
            _inventoryUI = FindObjectOfType<InventoryUI>();
            _inventoryUI.InitInventory(InventorySize, defaultSlotSprite);
            this.gameManager = GetComponent<GameManager>();
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


        public Dictionary<int,GroceryItem> GetAllInventoryGroceries()
        {
            var allGroceries = inventoryItems.Where(x => x.Value.ItemType == ItemType.Grocery);
            Dictionary<int, GroceryItem> allGroceriesItems = new Dictionary<int, GroceryItem>();
            foreach (var itemPair in allGroceries)
            {
                allGroceriesItems.Add(itemPair.Key, (GroceryItem)itemPair.Value);
            }
            return allGroceriesItems;
        }

        public List<int> GetAllgroceriesOfReceipeType(RecipeType receipeType)
        {
            List<GroceryType> groceries = gameManager.recipeManager.GetGroceryList(receipeType);

           // Dictionary<int, GroceryItem> groceriesToGet = new Dictionary<int, GroceryItem>();
            Dictionary<int, GroceryItem> allGroceries = GetAllInventoryGroceries();
            List<int> groceryIndex = new List<int>();


            foreach (GroceryType gType in groceries)
            {
                var groceryIndexPair = allGroceries.FirstOrDefault(x => x.Value.GroceryType == gType);
                if(groceryIndexPair.Value == null)
                {
                    //already failed cause this grocery is missing
                    return null;
                }
                else
                {
                    groceryIndex.Add(groceryIndexPair.Key);
                }
            }

            return groceryIndex;
        }

        public void DropRecipe(List<int> groceriesToRemoveAtIndex)
        {
            foreach(int index in groceriesToRemoveAtIndex) 
            {
                inventoryItems.Remove(index);
                _inventoryUI.ResetInventarySlot(index);
            }
        }
    }
}