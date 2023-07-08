using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shelf : MonoBehaviour
{
    public int Stock = 100000;

    #nullable enable
    public Item? shelfItem = null;
    #nullable disable

    public int itemIndicatorZOffset;

    private Collider shelfCollider;

    void Start()
    {
        shelfItem = null;
        shelfCollider = this.GetComponent<Collider>();
    }

    public void SetItem(Item item)
    {
        if (shelfItem != null) {
            Destroy(shelfItem);
        }

        item.transform.position = transform.position + Vector3.up * itemIndicatorZOffset;        
        shelfItem = item;
    }
}
