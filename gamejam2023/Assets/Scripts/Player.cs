using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 right;
    public Vector3 up;
    public Vector3 camVector;

    private CharacterController _controller;
    private Camera _camera;
    private Transform _visual;

    public float lookilook = 90f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _controller = GetComponent<CharacterController>();
        _visual = this.transform.GetChild(0);
        _SetPlayer();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 motion = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            motion += this.up * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            motion -= this.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            motion -= this.up * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            motion += this.right * Time.deltaTime;
        }

        if (_controller != null)
        {
            _controller.Move(motion);
        }

        _SetPlayer();
    }

    protected void _SetPlayer()
    {
        this.gameObject.transform.position = new Vector3(this.transform.position.x, 0.37f, this.transform.position.z);
    }
}
