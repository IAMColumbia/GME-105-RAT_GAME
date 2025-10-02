using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.UIElements;
using TMPro;


public class Rat : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] GameObject ItemSprite;

    PlayerControllerMappings playerMappings2D;

    Sprite rat;

    InputAction move;
    InputAction interact;
    InputAction pickUp;

    Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMappings2D = new();
        move = playerMappings2D.Player.Move;

        interact = playerMappings2D.Player.Interact;
        interact.performed += Interact;

        pickUp = playerMappings2D.Player.PickUp;
        pickUp.performed += PickUp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector2 input = move.ReadValue<Vector2>();
        Vector2 direction = (input.x * transform.right) + (transform.up * input.y);
        transform.position += (Vector3)(Time.deltaTime * speed * direction);
        if (input.x >= 0)
        {

        }
    }

    

    void OnEnable()
    {
        move.Enable();   
    }

    void OnDisable()
    {
        move.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Item")
        {
            interact.Enable();
            pickUp.Enable();
            ItemSprite = other.gameObject;
        } 
        print("Enter");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Item")
        {
            interact.Disable();
            pickUp.Disable();
        }
        print("Exit");
    }

    void Interact(InputAction.CallbackContext context)
    {
        print("interact");
    }

    void PickUp(InputAction.CallbackContext context)
    {
        Item itmScript = ItemSprite.GetComponent<Item>();
        itmScript.PickedUp();
    }
}
