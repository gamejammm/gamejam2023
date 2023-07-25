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



    bool modifierPressed;

    Vector2 motion;


    public void OnPlayerMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            motion = context.ReadValue<Vector2>();

        }
        else
        {
            motion  = Vector2.zero;
        }
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
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            SetMovePlayer(Vector2.zero);
        }


        if (Input.GetKey(KeyCode.W))
        {
            SetMovePlayer(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            SetMovePlayer(Vector2.left);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetMovePlayer(Vector2.down);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SetMovePlayer(Vector2.right);
        }

        else
        {
            Gamepad gamepad = Gamepad.current;
            // The return value of `.current` can be null.
            if (gamepad != null && gamepad.leftStick.value != Vector2.zero)
            {
                motion = gamepad.leftStick.value;
            }

            SetMovePlayer(motion);

        }

        this.gameObject.transform.position = new Vector3(this.transform.position.x, PlayerHeight, this.transform.position.z);

    }

    private void SetMovePlayer(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            motion = Vector2.zero;
            return;
        }

        float degreeToMove = Vector2.Angle(Vector2.up, direction);

        Debug.LogError(Vector2.Angle(Vector2.up,direction));

        if (direction == Vector2.up)
        {
            motion = Vector2.up;
            visualBack.SetActive(true);
            visualFront.SetActive(false);
        }
        else if (direction == Vector2.left)
        {
            motion = Vector2.left;
            visualBack.GetComponent<SpriteRenderer>().flipX = false;
            visualFront.GetComponent<SpriteRenderer>().flipX = true;
            visualBack.SetActive(false);
            visualFront.SetActive(true);
        }
        else if (direction == Vector2.right)
        {
            motion = Vector2.right;
            visualBack.GetComponent<SpriteRenderer>().flipX = true;
            visualFront.GetComponent<SpriteRenderer>().flipX = false;
            visualBack.SetActive(false);
            visualFront.SetActive(true);
        }
        else if (direction == Vector2.down)
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
