using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{

    public float recipeCreationInSec = 5f;

    private bool IsRecipeCreating;

    private GameManager gameManager;

    private RecipeUI recipeUI;

    public List<Recipe> CurrentRecipes;

   
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        recipeUI = FindObjectOfType<RecipeUI>();
        CurrentRecipes = new List<Recipe>();
    }

    // Update is called once per frame
    void Update()
    { 
        if(!IsRecipeCreating) 
        {
            IsRecipeCreating = true;
            StartCoroutine(CreateRecipe());
        }
    }


    IEnumerator CreateRecipe()
    {
        //Print the time of when the function is first called.
        Debug.Log("Start Cooldown for recipe Creation : " + Time.time);
        IsRecipeCreating = true;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(recipeCreationInSec);

        //Test Rezept
        List<GroceryItem> items = GetRecipe(RecipeType.StrawCumber);
        Recipe recipe = new Recipe(items, RecipeType.StrawCumber);
        CurrentRecipes.Add(recipe);
        gameManager.CreateRecipe(items);
        recipeUI.SetRecipe(recipe, "Erdbeer-Gurken-Dings");

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished recipe : ");
        IsRecipeCreating = false;

    }

    private List<GroceryItem> GetRecipe(RecipeType recipeType)
    {
        List<GroceryItem> items = new List<GroceryItem>();
        switch(recipeType)
        {
            case RecipeType.FootBerry:
                items.Add(gameManager.AssetLoader.GetGroceryItem(GroceryType.Strawberry));
                items.Add(gameManager.AssetLoader.GetGroceryItem(GroceryType.Foot));
                return items;
            case RecipeType.StrawCumber:
                items.Add(gameManager.AssetLoader.GetGroceryItem(GroceryType.Strawberry));
                items.Add(gameManager.AssetLoader.GetGroceryItem(GroceryType.Cucumber));
                return items;
            default: return null;
        }
    }

    public List<GroceryType> GetGroceryList(RecipeType recipeType)
    {
        List<GroceryType> groceries = new List<GroceryType>();
        List<GroceryItem> items = GetRecipe(recipeType);
        foreach(GroceryItem item in items)
        {
            groceries.Add(item.GroceryType);
        }
        return groceries;
    }

    public void RemoveRecipe(Recipe recipeType)
    {
        Debug.LogError("Remove Recipe ");
        CurrentRecipes.Remove(recipeType);
        recipeUI.RemoveRecipe(recipeType.RecipeType);
    }
}

public class Recipe
{
    public List<GroceryItem> GroceryItems;
    public RecipeType RecipeType;

    public Recipe (List<GroceryItem> _groceryItems, RecipeType _recipeType)
    {
        GroceryItems = _groceryItems;
        RecipeType = _recipeType;
    }
}

public enum RecipeType
{
    StrawCumber,
    FootBerry
}