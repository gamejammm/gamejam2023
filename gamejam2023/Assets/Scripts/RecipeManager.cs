using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{

    public float recipeCreationInSec = 5f;

    private bool IsRecipeCreating;

    private GameManager gameManager;

    private RecipeUI recipeUI;

   
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        recipeUI = FindObjectOfType<RecipeUI>();
    }

    // Update is called once per frame
    void Update()
    { 
        if(!IsRecipeCreating) 
        {
            IsRecipeCreating = true;
            StartCoroutine(Createrecipe());
        }
    }


    IEnumerator Createrecipe()
    {
        //Print the time of when the function is first called.
        Debug.Log("Start Cooldown for recipe Creation : " + Time.time);
        IsRecipeCreating = true;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(recipeCreationInSec);

        //Test Rezept
        List<Item> items = new List<Item>();
        items.Add(gameManager.AssetLoader.GetGroceryItem(GroceryType.Strawberry));
        items.Add(gameManager.AssetLoader.GetGroceryItem(GroceryType.Cucumber));

        gameManager.CreateRecipe(items);
        recipeUI.SetRecipe(items, "Erdbeer-Gurken-Dings");
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished recipe : ");
        IsRecipeCreating = false;

    }
}
