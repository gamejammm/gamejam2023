using DefaultNamespace;
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

    private CharacterController _controller;
    private Camera _camera;
    private Transform _visual;

    public float lookilook = 90f;

    public int LitterCount;

    private GameManager _gameManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _gameManager = FindObjectOfType<GameManager>();
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
        if (other.gameObject.transform.parent.tag == "Bottle" || other.gameObject.transform.parent.tag == "Shelf")
        {
            Item item;
            if (other.gameObject.transform.parent.tag == "Shelf")
            {
                Shelf shelf = other.gameObject.transform.parent.GetComponent<Shelf>();
                if (shelf == null)
                {
                    Debug.LogError("shelf does not have shelf Component");
                    return;
                }
                item = shelf.shelfItem;
                if (item != null)
                {
                    _gameManager.ItemCollected(item);
                }
            }

            else if (other.gameObject.transform.parent.tag == "Bottle")
            {
                item = other.gameObject.transform.parent.GetComponent<Item>();
                //if (item == null)
                //{
                //    item = other.gameObject.GetComponent<Item>();
                //}
                if (item == null)
                {
                    Debug.LogError("Item does not have Item class attached");
                    return;
                }
                bool isItemCollected = _gameManager.ItemCollected(item);
                if (isItemCollected)
                    item.gameObject.SetActive(false);

            }
        }

        else if (other.gameObject.transform.parent.tag == "DragonHead")
        {
            //Check if inventory has a receipe;
            _gameManager.DropReceipe();
        }

        else if (other.gameObject.transform.parent.tag == "BottleDeposit")
        {
            _gameManager.DropAllBottles();
        }
    }
}
