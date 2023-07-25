using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shelf : MonoBehaviour
{
    public int Stock = 100000;

    public Item shelfItem;

    public int itemIndicatorZOffset;

    public GameObject shelfActivateObject;

    private Collider shelfCollider;

    public Transform ItemSpawnPosition;

    void Start()
    {
        shelfCollider = this.GetComponent<Collider>();
    }

    public void SetItem(Item item)
    {
        if (shelfItem != null) {
            Destroy(shelfItem);
        }
        item.transform.position = ItemSpawnPosition.position;
        //item.transform.position = transform.position + Vector3.up * itemIndicatorZOffset;        
        shelfItem = item;
        item.transform.SetParent(ItemSpawnPosition.transform);
    }

    public void ActivateShelf(bool activate)
    {
        shelfActivateObject.SetActive(activate);

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.LogError("ENTE");
    }
}
