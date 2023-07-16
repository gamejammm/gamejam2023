using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class RecipeCard : VisualElement
{

    Label RecipeNameLabel;
    VisualElement GroceryStack;

    public RecipeCard(List<Item> ingredients, int ingedientSize, string recipeName)
    {
        GroceryStack = new VisualElement();
        this.Add(GroceryStack);
        this.style.flexDirection = FlexDirection.Column;
        RecipeNameLabel = new Label(recipeName);
        this.Add(RecipeNameLabel);
        GroceryStack.style.flexDirection = FlexDirection.Row;
        foreach(Item ingedient in ingredients) 
        {
            GroceryStack.Add(CreateIngerdientCard(ingedient, ingedientSize));
        }
        this.Add(GroceryStack);
        this.style.backgroundColor = new StyleColor(new UnityEngine.Color(0.5f,0.5f,0.5f, 0.5f));
    }

    private VisualElement CreateIngerdientCard(Item item, int size)
    {
        VisualElement ingedientCard =  new VisualElement();
        ingedientCard.style.height = size;
        ingedientCard.style.width = size;
        ingedientCard.style.maxHeight = size;
        ingedientCard.style.maxWidth = size;
        ingedientCard.style.minHeight = size;
        ingedientCard.style.minWidth = size;
        ingedientCard.style.backgroundImage = new StyleBackground(item.itemSprite);

        return ingedientCard;
    }

}
