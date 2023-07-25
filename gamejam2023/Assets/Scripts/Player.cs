using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float Speed = 5f;

    public float PlayerHeight = 0.37f;

    public GameObject visualFront;
    public GameObject visualBack;

    private CharacterController _controller;
    private Camera _camera;
    private Transform _visual;

    public float lookilook = 90f;

    public int LitterCount;

    private GameManager _gameManager;

    InputAction.CallbackContext gamePadContext;



    bool modifierPressed;

    Vector2 motion;


    public void OnPlayerMove(InputAction.CallbackContext context)
    {
         gamePadContext = context;
    }


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
        if (Input.GetKey(KeyCode.W))
        {
            motion = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            motion = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            motion = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            motion = Vector2.right;
        }

        else if(gamePadContext.performed) 
        {
            motion = gamePadContext.ReadValue<Vector2>();
        }
        else if(Gamepad.current != null && Gamepad.current.leftStick.value != Vector2.zero)
        {
            motion = Gamepad.current.leftStick.value;
        }
        else
        {
            motion = Vector2.zero;
        }

        SetMovePlayer(motion);
        this.gameObject.transform.position = new Vector3(this.transform.position.x, PlayerHeight, this.transform.position.z);

    }

    private void SetMovePlayer(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return;
        }
        float degreeToMove = Vector2.SignedAngle(Vector2.up, direction);

        if (degreeToMove < 45 && degreeToMove >= -45)
        {
            motion = Vector2.up;
            visualBack.SetActive(true);
            visualFront.SetActive(false);
        }
        else if (degreeToMove < -45 && degreeToMove >= -135)
        {
            motion = Vector2.right;
            visualBack.GetComponent<SpriteRenderer>().flipX = true;
            visualFront.GetComponent<SpriteRenderer>().flipX = false;
            visualBack.SetActive(false);
            visualFront.SetActive(true);
        }
        else if (degreeToMove < 135 && degreeToMove >= 45)
        {
            motion = Vector2.left;
            visualBack.GetComponent<SpriteRenderer>().flipX = false;
            visualFront.GetComponent<SpriteRenderer>().flipX = true;
            visualBack.SetActive(false);
            visualFront.SetActive(true);
        }
        else
        {
            motion = Vector2.down;
            visualBack.SetActive(false);
            visualFront.SetActive(true);
        }

        _controller.Move(-new Vector3(motion.x, 0f, motion.y) * Time.deltaTime * Speed);

    }

    protected void _SetPlayer()
    {
        visualBack.SetActive(false);
        visualFront.SetActive(true);
        this.gameObject.transform.position = new Vector3(this.transform.position.x, PlayerHeight, this.transform.position.z);

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
