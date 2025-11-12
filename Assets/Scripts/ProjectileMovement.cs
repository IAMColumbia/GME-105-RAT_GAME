using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    /// <summary>
    ///                speed - how fast is it moving?
    ///                lifetime - how long does it stay active?
    ///                direction - what direction is it firing at?
    /// </summary>
    [SerializeField] float speed = 10f;
    [SerializeField] float lifetime = 3f;

    Vector2 direction;


    // when the lifetime is achieved, destroy itself.
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // fire in the direction the that player picked at a constant speed.
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // picks the direction.
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }
}
