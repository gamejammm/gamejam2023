using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public float Price;

    public string Name;

    public Collider itemCollider;

    private float discount = 0;

    private Camera _camera;

    private Transform visual;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        visual = this.transform.GetChild(0);
        LookAtCamera();
        itemCollider = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCamera();
    }

    public float GetTotalPrice() {
        return discount * (1 - discount);
    }

    public void SetDiscount(float discount) {
        this.discount = discount;
    }

    protected void LookAtCamera()
    {
        Vector3 position = transform.position;
        visual.LookAt(_camera.transform.position);
        visual.Rotate(Vector3.right, 90);
    }
}
