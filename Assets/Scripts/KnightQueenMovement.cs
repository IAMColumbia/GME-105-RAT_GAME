using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightQueenMovement : MonoBehaviour
{
    private float walkInput = 0;
    [SerializeField] private float speed = 50;
    [SerializeField] private float force = 50;
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isLocked;

    [SerializeField] private float smoothTime = 0.05f;
    private Vector2 refVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        walkInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.y, force);
            isGrounded = false;
        }


        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }
    }

    void FixedUpdate()
    {
        if (isLocked == false)
        {
            Vector2 targetVelocity = new Vector2(walkInput * speed, rb.velocity.y);
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, smoothTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {

            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != ("Ground"))
        {
            isGrounded = false;
        }
    }
}
