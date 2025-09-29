using UnityEngine;

public class CubeMover : MonoBehaviour
{

    public GameObject gameCam;

    Rigidbody2D rb;

    public float speed = 1f;

    public float offsetty = 0.2f;

    private float walkInputHor;

    private float walkInputVert;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        walkInputHor = Input.GetAxisRaw("Horizontal");
        walkInputVert = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {

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
}
