using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    Dictionary<int, InventorySlot> inventorySlots;

    private VisualElement inventoryStack;

    private Sprite DefaultSprite;


    [SerializeField]
    private UIDocument m_UIDocument;

    // Start is called before the first frame update
    void OnEnable()
    {
        var rootElement = m_UIDocument.rootVisualElement;
        inventoryStack = rootElement.Q<VisualElement>("InventoryStack");
        inventorySlots = new Dictionary<int, InventorySlot>();

    }

    public void InitInventory(int size, Sprite sprite)
    {
        DefaultSprite = sprite;
        for (int i = 0;i<size;i++)
        {
            InventorySlot newSlot = new InventorySlot(64,4, DefaultSprite);
            inventorySlots.Add(i, newSlot);
            inventoryStack.Add(newSlot);

        }
    }

    public void SetInventorySlot(int index, Item item)
    {
        inventorySlots[index].SetImage(item.itemSprite);
    }

    public void ResetInventarySlot(int index)
    {
        inventorySlots[index].SetDefault();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
