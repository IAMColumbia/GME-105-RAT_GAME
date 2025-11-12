using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    ///                walkInput - is the input to move the character being pressed?
    ///                speed - how fast the player is moving?
    ///                force - the mommentum of the player.
    ///                rb - rigidbody of the player.
    ///                isGrounded - is the player touching the ground?
    ///                isLocked - is the player locked in position?
    ///                smoothTime - transition from start to stop time.
    ///                refVelocity - referencing the velocity of the player.    
    /// </summary>
    private float walkInput = 0;
    [SerializeField] private float speed = 50;
    [SerializeField] private float force = 50;
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isLocked;

    [SerializeField] private float smoothTime = 0.05f;
    private Vector2 refVelocity = Vector2.zero;

    private SpriteRenderer spriteRenderer;

    private PlayerHealth playerHealth;
    private Animator animatorController;

    // gets player's rigidbody.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();

        Transform ThePlayer = transform.Find("PlayerSprite");
        spriteRenderer = ThePlayer.GetComponent<SpriteRenderer>();
        animatorController = ThePlayer.GetComponent<Animator>();
    }

    // gets the horizontal input keys, and checks if the player is pressing it, if so then move the player as so.
    // if player is holding left shift, then  lock the player in place.
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

    // if player is not locked, then move player as so, then transition to stop as so.
    void FixedUpdate()
    {
        if (isLocked == false)
        {
            if (walkInput != 0)
            {
                Vector2 targetVelocity = new Vector2(walkInput * speed, rb.velocity.y);
                rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, smoothTime);

                animatorController.SetBool("isWalking", true);

                if (walkInput > 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (walkInput < 0)
                {
                    spriteRenderer.flipX = true;
                }
            }
            else 
            {
                animatorController.SetBool("isWalking", false);
            }
        }
        else
        {
            animatorController.SetBool("isWalking", false);
        }
    }

    // if player is touching the top of the ground, then set isGrounded to true.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            if (collision.contacts[0].normal.y > 0.7f)
            {
                isGrounded = true;
            }
        }

        if (collision.gameObject.tag == ("Enemy_0"))
        {
            playerHealth.TakeDamage(1);
        }
    }
    

    // otherwise no.
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != ("Ground"))
        {
            isGrounded = false;
        }
    }
}
