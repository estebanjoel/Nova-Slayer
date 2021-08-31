using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Spawner itemSpawn;
    public GameObject[] itemsToSpawn;
    public float minYPosition, maxYPosition;
    public float minSpawnRate, maxSpawnRate, currentSpawnRate;
    public float remainingTimeToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        SetRemainingTimeToSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingTimeToSpawn <= 0)
        {
            int i = Random.Range(0, itemsToSpawn.Length);
            itemSpawn.prefabToSpawn = itemsToSpawn[i];
            itemSpawn.prefabToSpawn.transform.position = new Vector3(itemSpawn.prefabToSpawn.transform.position.x, Random.Range(minYPosition, maxYPosition), 0);
            itemSpawn.Create();
            SetRemainingTimeToSpawn();
        }
        else
        {
            remainingTimeToSpawn -= Time.deltaTime;
        }
    }

    public void SetRemainingTimeToSpawn()
    {
        currentSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        remainingTimeToSpawn = currentSpawnRate;
    }
}
