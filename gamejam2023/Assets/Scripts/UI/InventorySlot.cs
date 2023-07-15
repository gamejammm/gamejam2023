using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{

    public Sprite InventoryIcon;

    private Sprite _placeholderSprite;


    VisualElement imageHolder;

    public InventorySlot(int size, int margin, Sprite placeHolderImage)
    {
        imageHolder = new VisualElement();
        imageHolder.style.marginBottom = margin;
        imageHolder.style.marginLeft = margin;
        imageHolder.style.marginRight = margin;
        imageHolder.style.marginRight = margin;
        imageHolder.style.color= Color.red;
        _placeholderSprite = placeHolderImage;
        imageHolder.style.backgroundImage = new StyleBackground(placeHolderImage);
        this.style.height= size;
        this.style.width = size;
        this.style.maxHeight = size;
        this.style.maxWidth = size;
        imageHolder.style.height = size;
        imageHolder.style.width = size;
        imageHolder.style.maxHeight = size;
        imageHolder.style.maxWidth = size;
        imageHolder.style.minHeight = size;
        imageHolder.style.minWidth = size;
        this.Add(imageHolder);
    }

    public void SetImage(Sprite Image)
    {
        imageHolder.style.backgroundImage = new StyleBackground(Image);
    }

    public void SetDefault()
    {
        imageHolder.style.backgroundImage = new StyleBackground(_placeholderSprite);
    }
}
