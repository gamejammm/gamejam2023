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
    
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _controller = GetComponent<CharacterController>();
        _SetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 motion = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            motion += this.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            motion -= this.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            motion -= this.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            motion += this.right;
        }

        if (_controller != null)
        {
            _controller.Move(motion);
        }
        _SetCamera();
    }

    protected void _SetCamera()
    {
        Vector3 position = transform.position;
        _camera.transform.position = position + camVector;
        _camera.transform.LookAt(position);
    }
}
