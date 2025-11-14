using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour
{
    /// <summary>
    ///             prefabToSpawn - what is going to spawn?
    ///             minSpawnDelay - lowest spawn rate.
    ///             maxSpawnDelay - maximum spawn rate.
    ///             spawnRadius - how big is the area to spawn?
    ///             currentSpawn - current prefab that spawned.
    ///             isSpawning - can it spawn?
    /// </summary>
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] float spawnRadius = 5f;

    private GameObject currentSpawn;
    private bool isSpawning = false;


    void Start()
    {
        enabled = false;
    }

    // if no other prefab is spawned, then run the spawn method.
    void Update()
    {
        if (currentSpawn == null && !isSpawning)
        {
            StartCoroutine(SpawnAfterDelay());
        }
    }


    // picks a random delay between min and max, then picks a random position in radius, then spawn the prefab.
    IEnumerator SpawnAfterDelay()
    {
        isSpawning = true;
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(delay);

        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        currentSpawn = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        isSpawning = false;
    }

    // shows spawn radius.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    public void EnableSpawning()
    {
        enabled = true;
    }


    public void DisableSpawning()
    {
        enabled = false;
    }
}
