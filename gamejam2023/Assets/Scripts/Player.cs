using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 right;
    public Vector3 up;
    public Vector3 camVector;

    public GameObject visualFront;
    public GameObject visualBack;

    private PlayerUI playerUI;

    private CharacterController _controller;
    private Camera _camera;
    private Transform _visual;

    public float lookilook = 90f;

    public int LitterCount;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        playerUI = FindObjectOfType<PlayerUI>();
        _controller = GetComponent<CharacterController>();
        _visual = this.transform.GetChild(0);
        _SetPlayer();
        LitterCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 motion = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            motion += this.up * Time.deltaTime;
            visualBack.SetActive(true);
            visualFront.SetActive(false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            motion -= this.right * Time.deltaTime;
            visualBack.GetComponent<SpriteRenderer>().flipX = false;
            visualFront.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            motion -= this.up * Time.deltaTime;
            visualBack.SetActive(false);
            visualFront.SetActive(true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            motion += this.right * Time.deltaTime;
            visualBack.GetComponent<SpriteRenderer>().flipX = true;
            visualFront.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
            visualBack.SetActive(false);
            visualFront.SetActive(true);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Litter")
        {
            LitterCount++;
            Destroy(other.gameObject.transform.parent.gameObject);
            playerUI.SetLitterValue(LitterCount);
        }
    }
}
