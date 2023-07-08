using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public float Price;

    public string Name;

    public Collider itemCollider;

    // Start is called before the first frame update
    void Start()
    {
        itemCollider = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
