using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    /// <summary>
    ///                projectilePrefab - what is the thing being shoot?
    ///                firePoint - where is it being fired?
    ///                fireRate - how fast does it shoot?
    ///                nextFireTime - how long it takes to fire.
    /// </summary>
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 0.5f;
    float nextFireTime;

    // A reference to the SpriteRenderer component.
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component on the same GameObject at the start.
        Transform TheGun = transform.Find("toongun");
        spriteRenderer = TheGun.GetComponent<SpriteRenderer>();

        if (TheGun != null)
        {
            spriteRenderer = TheGun.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogError("Could not find 'toongun' child object.");
        }
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            var proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation).GetComponent<ProjectileMovement>();
            proj?.SetDirection(transform.right);
            nextFireTime = Time.time + fireRate;
        }

        FlipBasedOnRotation();
    }

    void FlipBasedOnRotation()
    {
        // Check the gun's X-axis direction.
        // If it's facing left (negative x), flip the sprite.
        if (transform.right.x < -0.1)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
    }
}