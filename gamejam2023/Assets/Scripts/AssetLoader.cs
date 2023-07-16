using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{

    public List<GameObject> Items;

    public GameObject Entrance;

    public GameObject RegularShelf;

    public GameObject WallSide;

    public GameObject WallEdgeInside;

    public GameObject WallEdgeOutside;

    public GameObject FloorTile;

    public GameObject DepositMachine;


    private string PathToItemsFolder;

    // Start is called before the first frame update
    void Start()
    {
        PathToItemsFolder = Application.dataPath + "/Resources/Objects/Items";
        LoadItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadItems()
    {
        //Getting all Files in Folder
       // Items = new List<GameObject>();


        //Debug.LogError(PathToItemsFolder);
        string[] itemsFileNames = Directory.GetFiles(PathToItemsFolder);


        GameObject test = Resources.Load("regal_01fm", typeof(GameObject)) as GameObject;
        //GameObject[] instance = Resources.LoadAll<GameObject>("/Items") as GameObject[];
        foreach(string itemName in itemsFileNames)
        {
            string ttt = Path.GetFileNameWithoutExtension(itemName);

            // Debug.LogError(ttt);
            GameObject itemObject = Resources.Load("Props/" + ttt, typeof(GameObject)) as GameObject;
            if(itemObject != null)
            {
               // Items.Add(itemObject);
                Instantiate(itemObject);
            }
        }
    }

    public Item GetGroceryItem(GroceryType groceryType)
    {
        /// Reihenfolge/Index beachten

        Item item;
        switch (groceryType)
        {
            case GroceryType.Cucumber:
                item = Items[2].GetComponent<Item>();
                return item;
            case GroceryType.Strawberry:
                item = Items[1].GetComponent<Item>();
                return item;
            case GroceryType.Foot:
                item = Items[0].GetComponent<Item>();
                return item;
                default: return null;

        }
    }
}
