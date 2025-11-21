using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    /// <summary>
    ///             moveSpeed - how fast the enemy?
    ///             movingRight - is moving?
    ///             rb - rigidbody of enemy.
    ///             lifetime - how long it lives for?
    ///             healthPoint - how much it deals?
    ///             moveDir - which direction it is moving?
    /// </summary>
    public float moveSpeed = 5f;
    private bool movingRight = true;
    private Rigidbody2D rb;

    public float lifetime = 2.0f;

    [SerializeField] int healthPoint = 2;
    private Vector2 moveDir;


    // sets up stuff, destroys object when time is reached, picks a random direction then moves when spawned.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        Destroy(gameObject, lifetime);

        PickNewDirection();
    }

    // move constantly.
    void FixedUpdate()
    {
        rb.linearVelocity = moveDir * moveSpeed;
    }

    // did it collide with something?
    void OnCollisionEnter2D(Collision2D collision)
    {
        PickNewDirection();
    }


    // if collided with something, pick a random new direction, then move.
    void PickNewDirection()
    {
        Vector2[] dirs = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        Vector2 newDir;
        do
        {
            newDir = dirs[Random.Range(0, dirs.Length)];
        }
        while (newDir == moveDir);

        moveDir = newDir;
    }

}
