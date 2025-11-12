using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] float spawnDelay = 3f; // time to wait after enemy is killed before respawning

    private GameObject currentSpawn;
    private bool isSpawning = false;

    void Update()
    {
        // If there’s no active enemy and we’re not already waiting, start the timer
        if (currentSpawn == null && !isSpawning)
        {
            StartCoroutine(SpawnAfterDelay());
        }
    }

    IEnumerator SpawnAfterDelay()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);

        currentSpawn = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        isSpawning = false;
    }
}
