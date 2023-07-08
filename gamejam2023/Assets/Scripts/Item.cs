using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public float Price;

    public string Name;

    public Collider itemCollider;

    private float discount = 0;

    // Start is called before the first frame update
    void Start()
    {
        itemCollider = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetTotalPrice() {
        return discount * (1 - discount);
    }

    public void SetDiscount(float discount) {
        this.discount = discount;
    }
}
