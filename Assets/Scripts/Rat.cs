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

    public ShowThatBox help;

    public float offsetty = 0.2f;

    public float talkDistance = 0.5f;

    private Vector2 currentPos;

    private Vector2 prevDir;

    private NPC talkingToYou = null;

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
        help = GameObject.Find("Main Camera").GetComponent<ShowThatBox>();

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
        currentPos = transform.position;

        if (Input.GetKeyDown("e"))
        {
            if (GameObject.Find("UI_Dialogue(Clone)") == false && doingThis != ratDoing.talking)
            {
                TalkingTime();
                doingThis = ratDoing.talking;
            } else
            {
                TalkMore();
            }
        }
    }

    void FixedUpdate()
    {
        if ( doingThis != ratDoing.talking)
        {
            HandleMovement();
        }       
    }

    void HandleMovement()
    {
        Vector2 input = move.ReadValue<Vector2>();
        Vector2 direction = (input.x * transform.right) + (transform.up * input.y);
        transform.position += (Vector3)(Time.deltaTime * speed * direction);
        if (input.x != 0 || input.y != 0)
        {
            doingThis = ratDoing.moving;

            prevDir = direction;
        }
        else
        {
            doingThis = ratDoing.idle;
        }

        Debug.Log(direction);
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
        WarpTrigger warptrig = other.GetComponent<WarpTrigger>();
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

        if (warptrig != null)
        {
            justdoit.WarpCamera(warptrig.cameraWarpX, warptrig.cameraWarpY);

            Vector3 myPos = transform.position;

            myPos.x = warptrig.spawnPosX;
            myPos.y = warptrig.spawnPosY;

            transform.position = myPos;
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

        if (talkingToYou == null)
        {
            RaycastHit2D talkToYou = Physics2D.Raycast(currentPos, prevDir, talkDistance);

            talkingToYou = talkToYou.collider.gameObject.GetComponent<NPC>();
        }

        string thisLine = talkingToYou.SpeakUp();

        Debug.Log(thisLine);

        if (thisLine == "")
        {
            talkingToYou.LineReset();
            talkingToYou = null;
            doingThis = ratDoing.idle;
            help.DestroyText();
        }
        else
        {
            help.DisplayText(thisLine);
        }
        
    }

    public void TalkMore()
    {

        //TODO: line changes even if you're just pressing e to make all text of current line appear. Fix.
        talkingToYou.NextLine();
        TalkingTime();
    }
}
