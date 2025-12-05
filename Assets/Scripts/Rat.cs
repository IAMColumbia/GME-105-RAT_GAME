using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.UIElements;
using TMPro;
using UnityEditor;


public class Rat : MonoBehaviour
{
    /// <summary>
    ///             speed - how fast the player is moving?
    ///             ItemSprite - item being held?
    ///             playerMappings2D - what are the controls for player?
    ///             rat - what is the sprite for rat?
    ///             move - move keys?
    ///             interact - interact key?
    ///             pickUp - pick up key?
    ///             rb - where is the rigidbody for player?
    ///             gameCam - where is the main camera?
    ///             offsetty - the offset?
    ///             talkDistance - how far can the player be to talk?
    ///             currentPos - current possition for player?
    ///             rbody - where is the rigidbody for player?
    ///             anim - where is animation controller for player?
    ///             spriteRenderer - where is sprite renderer for player?
    ///             lastMoveDir - what was the last move direction of player?
    ///             talkingToYou - where is the npc talking script?
    ///             prevDir - what was the previous move direction of player?
    ///             help - errr the talking box?
    ///             playerHealth - where is the player health script?
    /// </summary>
    


    [SerializeField] float speed = 5.0f;
    GameObject ItemSprite;

    PlayerControllerMappings playerMappings2D;

    [SerializeField] GameObject Inventory;
    SpriteRenderer inventoryLayer;
    Sprite rat;

    [SerializeField] GameObject RatItem;
    List<GameObject> RatItems = new List<GameObject>();
    [SerializeField] int ratItems = 0;
    SpriteRenderer ratItem;
    float yPos = 0;
    SpriteRenderer itemSprite;

    [SerializeField] GameObject DropOff;
    [SerializeField] GameObject DroppedOffItem;
    List<GameObject> DroppedOffItems = new List<GameObject>();

    InputAction move;
    InputAction interact;
    InputAction pickUp;
    InputAction drop;

    Rigidbody2D rb;

    public GameObject gameCam;

    public ShowThatBox help;

    public float offsetty = 0.2f;

    public float talkDistance = 0.5f;

    private Vector2 currentPos;

    private Rigidbody2D rbody;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastMoveDir = Vector2.down;

    public Vector3 foodUp = new Vector3(0.013f, 0.095f, -1);
    public Vector3 foodDown = new Vector3(0.005f, -0.024f, -1);
    public Vector3 foodLeft = new Vector3(-0.158f, -0.04f, 1);
    public Vector3 foodRight = new Vector3(0.158f, -0.04f, 1);

    private NPC talkingToYou = null;

    private Vector2 prevDir;


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
        RatItem.transform.SetParent(Inventory.transform, false);
        //RatItem.transform.localPosition = Vector3.zero;
        ratItem = RatItem.GetComponent<SpriteRenderer>();
        inventoryLayer = Inventory.GetComponent<SpriteRenderer>();
        spriteRenderer = TheRat.GetComponent<SpriteRenderer>();
        help = GameObject.Find("Main Camera").GetComponent<ShowThatBox>();
        playerMappings2D = new();
        move = playerMappings2D.Player.Move;
        playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        help = GameObject.Find("Main Camera").GetComponent<ShowThatBox>();

        interact = playerMappings2D.Player.Interact;
        interact.performed += Interact;

        pickUp = playerMappings2D.Player.PickUp;
        pickUp.performed += PickUp;

        drop = playerMappings2D.Player.Drop;
        drop.performed += Drop;
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
            prevDir = moveDir;
            if (moveDir == Vector2.up)
            {
                Inventory.transform.localPosition = foodDown;
                foreach (GameObject item in RatItems)
                {
                    item.GetComponent<SpriteRenderer>().sortingOrder = 35;
                }
                print("35");
                //inventoryLayer.sortingOrder = 2; 
            }
            if (moveDir == Vector2.down)
            {
                Inventory.transform.localPosition = foodUp;
                foreach (GameObject item in RatItems)
                {
                    item.GetComponent<SpriteRenderer>().sortingOrder = 20;
                }
                //inventoryLayer.sortingOrder = 1;
            }
            if (moveDir == Vector2.left)
            {
                Inventory.transform.localPosition = foodRight;
                foreach (GameObject item in RatItems)
                {
                    item.GetComponent<SpriteRenderer>().sortingOrder = 20;
                }
                //inventoryLayer.sortingOrder = 1;
            }
            if (moveDir == Vector2.right)
            {
                Inventory.transform.localPosition = foodLeft;
                foreach (GameObject item in RatItems)
                {
                    item.GetComponent<SpriteRenderer>().sortingOrder = 20;
                }
                //inventoryLayer.sortingOrder = 1;
            }
            doingThis = ratDoing.moving;

            if (lastMoveDir != Vector2.zero)
                prevDir = lastMoveDir;
            doingThis = ratDoing.idle;
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


    // triggers camera, and item pick up collision.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Item")
        {
            interact.Enable();
            pickUp.Enable();
            ItemSprite = other.gameObject;
        }
        else if (other.transform.tag == "DropOff")
        {
            drop.Enable();
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
        else if (other.transform.tag == "DropOff")
        {
            drop.Disable();
        }
        print("Exit");
    }

    void Interact(InputAction.CallbackContext context)
    {
        print("interact");
    }

    void PickUp(InputAction.CallbackContext context)
    {
        if (ItemSprite == null) return;

        if (ratItems == 0)
        {
            ratItems += 1;
            Item itmScript = ItemSprite.GetComponent<Item>();
            itmScript.PickedUp();
            GameObject NewRatItem = Instantiate(RatItem, Inventory.transform);
            NewRatItem.transform.localPosition = Vector3.zero;
            SpriteRenderer newRatItem = NewRatItem.GetComponent<SpriteRenderer>();
            itemSprite = ItemSprite.GetComponent<SpriteRenderer>();
            newRatItem.sprite = itemSprite.sprite;
            speed -= 0.1f;
            RatItems.Add(NewRatItem);
        }
        else if (ratItems >= 1)
        {
            print(yPos.ToString());
            yPos += 0.14f;
            ratItems += 1;
            Item itmScript = ItemSprite.GetComponent<Item>();
            itmScript.PickedUp();
            GameObject NewRatItem = Instantiate(RatItem, Inventory.transform);
            NewRatItem.transform.localPosition = new Vector3(RatItem.transform.localPosition.x, RatItem.transform.localPosition.y + yPos, RatItem.transform.localPosition.z);
            SpriteRenderer newRatItem = NewRatItem.GetComponent<SpriteRenderer>();
            itemSprite = ItemSprite.GetComponent<SpriteRenderer>();
            newRatItem.sprite = itemSprite.sprite;
            speed -= 0.1f;
            RatItems.Add(NewRatItem);
        }
    }

    void Drop(InputAction.CallbackContext context)
    {
        while (RatItems.Count != 0)
        {
            foreach (GameObject RI in RatItems)
            {
                RatItems.Remove(RI);
                GameObject DOI = Instantiate(DroppedOffItem);
                DOI.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                SpriteRenderer doi = DOI.GetComponent<SpriteRenderer>();
                doi.sortingOrder = 2;
                SpriteRenderer ri = RI.GetComponent<SpriteRenderer>();
                doi.sprite = ri.sprite;
                ri.sprite = null;
                Destroy(RI);
                DroppedOffItems.Add(DOI);
                print($"{DroppedOffItems}");
            }
        }
    }

    public void MountObject()
    {
        
    }

    void PickUpItem(Item item)
    {
        item.PickedUp();
        //playerHealth.heldItem = item;
    }

    public void TalkingTime()
    {
        if (talkingToYou == null)
        {
            Debug.DrawRay(currentPos, lastMoveDir * talkDistance, Color.green, 0.5f);

            RaycastHit2D talkToYou = Physics2D.Raycast(currentPos, lastMoveDir, talkDistance);

            Debug.Log($"Ray hit: {talkToYou.collider.name} on layer {talkToYou.collider.gameObject.layer}");



            talkingToYou = talkToYou.collider.GetComponentInParent<NPC>();
            if (talkingToYou == null) return;
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
        talkingToYou.NextLine();
        TalkingTime();
    }


    // if collision on touching is detected with a foot tag, deal damage to player.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Foot"))
        {
            playerHealth.TakeDamage(1);

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

    }

}
