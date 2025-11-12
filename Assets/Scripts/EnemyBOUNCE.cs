using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBOUNCE : MonoBehaviour
{
    /// <summary>
    ///                moveSpeed = how fast it moves.
    ///                jumpForce = how high it jumps.
    ///                jumpDelay = how long til' next jump.
    ///                rb = rigidbody of enemy.
    ///                isGrounded = is the enemy grounded?
    /// </summary>
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float jumpDelay = 1f;

    private Vector2 lastPosition;
    private float currentDirection = -1;

    private Rigidbody2D rb;
    private bool isGrounded = false;


    [SerializeField] int healthPoint = 2;

    // gets the rigidbody and starts JumpLoop().
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(JumpLoop());
    }

    // checks if player is grounded.
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

    // checks if player is NOT grounded.
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }

    // jumps to the left at a set distance, and height, at a certain time on a loop.
    System.Collections.IEnumerator JumpLoop()
    {
        lastPosition = rb.position;

        while (true)
        {
            if (Mathf.Abs(rb.position.x - lastPosition.x) < 0.01f)
            {
                currentDirection *= -1;
            }

            lastPosition = rb.position;

            if (isGrounded)
            {
                rb.velocity = new Vector2(currentDirection * moveSpeed, jumpForce);
            }

            yield return new WaitForSeconds(jumpDelay);
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}
