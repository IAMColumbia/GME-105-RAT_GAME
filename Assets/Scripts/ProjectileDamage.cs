using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private PlayerPoints playerPoints;
    private GameObject player0;

    void Start()
    {
        player0 = GameObject.FindGameObjectWithTag("Player");
        playerPoints = player0.GetComponent<PlayerPoints>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    // upon colliding with an object with the tag Enemy_0, or Ground, it does different things.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_0"))
        {
            EnemyRUN enemy = collision.gameObject.GetComponent<EnemyRUN>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            playerPoints.AddScore(100);
            playerPoints.AddCombo(0.5f);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
