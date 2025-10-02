using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMover : MonoBehaviour
{

    public GameObject gameCam;

    Rigidbody2D rb;

    public float speed = 1f;

    public float offsetty = 0.2f;

    public float talkDistance = 0.5f;

    private float walkInputHor;

    private float walkInputVert;

    private Vector2 currentPos;

    private enum ratDoing
    {
        idle,
        moving,
        talking,
        hurting,
    }

    private ratDoing doingThis = ratDoing.idle;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        walkInputHor = Input.GetAxisRaw("Horizontal");
        walkInputVert = Input.GetAxisRaw("Vertical");

        currentPos = transform.position;

        if (Input.GetKeyDown("e"))
        {
            TalkingTime();
        }

    }

    private void FixedUpdate()
    {
        if (walkInputHor != 0 || walkInputVert != 0)
        {
            doingThis = ratDoing.moving;
        } else
        {
            doingThis = ratDoing.idle;
        }

        rb.linearVelocity = new Vector2(walkInputHor, walkInputVert) * speed;


    }

    void OnTriggerEnter2D(Collider2D other)
    {
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

    public void TalkingTime()
    {

        RaycastHit2D talkToYou = Physics2D.Raycast(currentPos, Vector2.left, talkDistance);

        if (talkToYou == true)
        {
            Debug.Log(talkToYou.collider.gameObject.name);
        }
    }
}
