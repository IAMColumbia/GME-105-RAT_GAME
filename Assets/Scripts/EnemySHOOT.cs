using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySHOOT : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootDelay = 2f;

    private void Start()
    {
        StartCoroutine(ShootLoop());
    }

    private IEnumerator ShootLoop()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // get the script and set direction based on firePoint's "right"
            EnemyProjectile projectile = proj.GetComponent<EnemyProjectile>();
            if (projectile != null)
            {
                projectile.SetDirection(firePoint.right);
            }
        }
    }
}
