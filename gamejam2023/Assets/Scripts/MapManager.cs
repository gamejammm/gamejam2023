using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Map Managing for alrerady existing map
/// </summary>
public class MapManager : MonoBehaviour
{
    //bool: has item, shelf: shelf
    List<Shelf> allShelfs;

    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        allShelfs = FindObjectsOfType<Shelf>().ToList();
        PlaceItemsRandom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int[] UniqeRandomList(int numbercount)
    {
        var nums = Enumerable.Range(0, numbercount).ToArray();
        var rnd = new System.Random();

        // Shuffle the array
        for (int i = 0; i < nums.Length; ++i)
        {
            int randomIndex = rnd.Next(nums.Length);
            int temp = nums[randomIndex];
            nums[randomIndex] = nums[i];
            nums[i] = temp;
        }

        return nums;

    }

    private void PlaceItemsRandom()
    {
        int ShelfCount =  allShelfs.Count;

        List<Shelf> emptyShelfs = allShelfs.Where(x => x.shelfItem == null).ToList();

        List<GameObject> allItems = gameManager.AssetLoader.Items;
        if(emptyShelfs == null || emptyShelfs.Count() < allItems.Count())
        {
            Debug.LogError("No or too less shelfs are free");
            return;
        }

        int[] randomIndex = UniqeRandomList(allItems.Count);
        for(int i = 0;i < randomIndex.Length; ++i) 
        {
            GameObject instantiadedItem = Instantiate(allItems[i]);
            emptyShelfs[randomIndex[i]].SetItem(instantiadedItem.GetComponent<Item>());
        }


    }
}
