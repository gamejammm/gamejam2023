using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Map ShopMap;

    public Player ShopPlayer;

    public float StartMoney = 50f;

    private float currentMonetas;

    private Inventory inventory;

    public RecipeManager recipeManager;

    private StatusUI statusUI;

    public AssetLoader AssetLoader;

    public bool IsBottlesDropping;

    private bool isRecipeDropping;


    // Start is called before the first frame update
    void Start()
    {
        inventory = this.GetComponent<Inventory>();
        recipeManager = this.GetComponent<RecipeManager>();
        ShopMap = this.GetComponent<Map>();
        AssetLoader = this.GetComponent<AssetLoader>();
        statusUI = FindObjectOfType<StatusUI>();

        if (ShopMap.initialized == true)
        {
            SetPlayer();
        }

        else
        {
            ShopMap.isInitializationDone.AddListener(SetPlayer);
        }

        currentMonetas = StartMoney;
        statusUI.SetMonetasValue(currentMonetas);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool ItemCollected(Item item)
    {
        if (inventory.IsInventoryFull())
        {
            return false;
        }

        if (item.ItemType != ItemType.Bottle)
        {
            if (SetMonetas(item.Price))
            {
                inventory.NewItemCollected(item);
                return true;
            }
            else
            {
                Debug.LogError("Item is Too expensive");
                return false;
            }
        }
        else
        {
            inventory.NewItemCollected(item);
        }
        return true;
    }

    public void DropAllBottles()
    {
        if (IsBottlesDropping)
        {
            return;
        }
        Debug.LogError("Drop All Bottles");
        IsBottlesDropping = true;
        float monetasToGet = inventory.DropAllBottlesAndGetReturnCost();
        SetMonetas(monetasToGet);
        Debug.LogError("Money to Pay: +" + monetasToGet);
        IsBottlesDropping = false;
    }


    public void CreateRecipe(List<GroceryItem> recipe)
    {

    }

    public void DropReceipe()
    {
        if (isRecipeDropping)
            return;

        isRecipeDropping = true;
        List<Recipe> validRecipes = new List<Recipe>(); 
        foreach (Recipe recipeType in recipeManager.CurrentRecipes)
        {
            List<int> inventoryGroceries = inventory.GetAllgroceriesOfReceipeType(recipeType.RecipeType);
            if(inventoryGroceries!= null)
            {
                //There is a complete Recipe to Drop
                validRecipes.Add(recipeType);
                inventory.DropRecipe(inventoryGroceries);
                Debug.LogError("RECIPE DROP");
            }
        }
        foreach(Recipe recipeType in validRecipes)
        {
            recipeManager.RemoveRecipe(recipeType);
        }
        isRecipeDropping = false;

    }

    private void SetPlayer()
    {
        ShopPlayer.transform.position = ShopMap.GetEntrance();
        Debug.LogError(ShopPlayer.transform.position);
    }

    private bool SetMonetas(float priceToPay)
    {
        if (priceToPay > currentMonetas)
        {
            Debug.LogError("Not enough Money");
            statusUI.SetMonetasValue(currentMonetas);
            return false;
        }

        else
        {
            currentMonetas -= priceToPay;
        }
        statusUI.SetMonetasValue(currentMonetas);
        return true;
    }
}
