using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public int Stock = 100000;

    public Item shelfItem;

    private Collider shelfCollider;

    void Start()
    {
        shelfItem = null;
        shelfCollider = this.GetComponent<Collider>();
    }

    public void AddItem(Item item)
    {
        shelfItem  = item;
    }
}
