using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class RecipeUI : MonoBehaviour
{
    [SerializeField]
    private UIDocument m_UIDocument;

    private VisualElement recipeStack;

    private Dictionary<Recipe, VisualElement> allrecipeElements;


    // Start is called before the first frame update
    void Start()
    {
        var rootElement = m_UIDocument.rootVisualElement;
        recipeStack = rootElement.Q<VisualElement>("RecipeStack");
        allrecipeElements= new Dictionary<Recipe, VisualElement>();
    }

    public void SetRecipe(Recipe itemRecipe,string recipeName)
    {
        VisualElement newRecipeCard = new RecipeCard(itemRecipe, 32, recipeName);
        recipeStack.Add(newRecipeCard);
        allrecipeElements.Add(itemRecipe, newRecipeCard);
    }

    public void RemoveRecipe(RecipeType recipeType)
    {
        Debug.LogError("TODO: Remocve recipe in UI");
        var cardToDelete = allrecipeElements.FirstOrDefault(x => x.Key.RecipeType == recipeType);
        allrecipeElements.Remove(cardToDelete.Key);
        recipeStack.Remove(cardToDelete.Value);
    }
}
