using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RecipeUI : MonoBehaviour
{
    [SerializeField]
    private UIDocument m_UIDocument;

    private VisualElement recipeStack;


    // Start is called before the first frame update
    void Start()
    {
        var rootElement = m_UIDocument.rootVisualElement;
        recipeStack = rootElement.Q<VisualElement>("RecipeStack");
    }

    public void SetRecipe(List<Item> itemRecipe,string recipeName)
    {
        recipeStack.Add(new RecipeCard(itemRecipe, 32, recipeName));
    }
}
