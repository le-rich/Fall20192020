using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timeToSpawn;
    public float timeLastSpawned;
    public GameObject objectToSpawn;
    public float spawnBounds;

    // Start is called before the first frame update
    void Start()
    {
        timeLastSpawned = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeLastSpawned + timeToSpawn)
        {
            GameObject newEnemy = Instantiate(objectToSpawn, transform.position, transform.rotation);
            float randomOffset = Random.Range(-spawnBounds, spawnBounds);
            newEnemy.transform.Translate(Vector2.left * randomOffset);

            timeLastSpawned = Time.time;
        }
    }
}
