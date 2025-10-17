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

    public GameObject gameCam;

    public float offsetty = 0.2f;

    public float talkDistance = 0.5f;

    private Vector2 currentPos;

    private Rigidbody2D rbody;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastMoveDir = Vector2.down;

    private PlayerHealth playerHealth;

    private enum ratDoing
    {
        idle,
        moving,
        talking,
        hurting,
    }

    private ratDoing doingThis = ratDoing.idle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Transform TheRat = transform.Find("The_Rat");
        anim = TheRat.GetComponent<Animator>();
        spriteRenderer = TheRat.GetComponent<SpriteRenderer>();
        playerMappings2D = new();
        move = playerMappings2D.Player.Move;
        playerHealth = Object.FindFirstObjectByType<PlayerHealth>();

        interact = playerMappings2D.Player.Interact;
        interact.performed += Interact;

        pickUp = playerMappings2D.Player.PickUp;
        pickUp.performed += PickUp;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;

        if (Input.GetKeyDown("e"))
        {
            if (GameObject.Find("UI_Dialogue(Clone)") == false)
            {
                TalkingTime();
                doingThis = ratDoing.talking;
            }
            else
            {

            }
        }
    }

    void FixedUpdate()
    {
        if (doingThis != ratDoing.talking)
        {
            HandleMovement();
        }
    }


    // uses velocity to and input to check if the player is moving in horizontal or vertical and sets floats for them.
    // if player is not moving then it saves the last values it got.
    void HandleMovement()
    {
        Vector2 input = move.ReadValue<Vector2>();
        rb.linearVelocity = input * speed;

        bool isMoving = input.sqrMagnitude > 0.01f;
        anim.SetBool("isWalking", isMoving);

        if (isMoving)
        {
            Vector2 moveDir = input.normalized;

            if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y))
            {
                moveDir.y = 0;
            }
            else
            {
                moveDir.x = 0;
            }

            anim.SetFloat("Horizontal", moveDir.x);
            anim.SetFloat("Vertical", moveDir.y);

            lastMoveDir = moveDir;
            doingThis = ratDoing.moving;
        }
        else
        {
            anim.SetFloat("Horizontal", lastMoveDir.x);
            anim.SetFloat("Vertical", lastMoveDir.y);
            doingThis = ratDoing.idle;
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

        Debug.Log("this is the rat trigger.");

        CameraTrigger camtrig = other.GetComponent<CameraTrigger>();
        CameraMove justdoit = gameCam.GetComponent<CameraMove>();

        if (camtrig != null)
        {
            justdoit.MoveCamera(camtrig.cameraIncX, camtrig.cameraIncY);

            BoxCollider2D col = this.GetComponent<BoxCollider2D>();
            Vector3 offsetTime = col.size;
            Vector3 myPos = transform.position;

            myPos.x += (offsetTime.x + offsetty) * camtrig.cameraIncX;
            myPos.y -= (offsetTime.y + offsetty) * camtrig.cameraIncY;

            transform.position = myPos;

            camtrig.flipCameraInc();
        }
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

    public void TalkingTime()
    {

        RaycastHit2D talkToYou = Physics2D.Raycast(currentPos, lastMoveDir, talkDistance);

        if (talkToYou && talkToYou.collider.gameObject != gameObject)
        {
            Debug.Log("Hit " + talkToYou.collider.name);
            NPC bitch = talkToYou.collider.GetComponent<NPC>();
            if (bitch != null)
            {
                bitch.SpeakUp();
            }
            else
            {
                Debug.LogWarning("Hit object has no NPC component!");
            }
        }
        else
        {
            Debug.LogWarning("No object hit by raycast.");
        }
    }


    // if collision on touching is detected with a foot tag, deal damage to player.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Foot"))
        {
            playerHealth.TakeDamage(1);
        }
    }
}
