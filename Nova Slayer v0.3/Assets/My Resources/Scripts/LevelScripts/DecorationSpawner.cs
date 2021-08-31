using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSpawner : MonoBehaviour
{
    public Decoration[] decorationToSpawn;
    public float decorationLifeTime;
    public float minScaleSize, maxScaleSize;
    public int minDecorationsToSpawn, maxDecorationsToSpawn;
    public float minYPosition, maxYPosition;
    public float minXSpeed, maxXSpeed, minYSpeed, maxYSpeed;
    public float spawnRate; 
    public float remianingTimeToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        SetTimeRate();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.victoryCondition && !GameManager.instance.failCondition)
        {
            if(remianingTimeToSpawn <= 0)
            {
                StartCoroutine(SpawnDecorations());
                SetTimeRate();
            }
            else
            {
                remianingTimeToSpawn -= Time.deltaTime;
            }
        }
    }

    public void SetTimeRate()
    {
        remianingTimeToSpawn = spawnRate;
    }

    public int SetDecorationsToSpawn()
    {
        return Random.Range(minDecorationsToSpawn, maxDecorationsToSpawn);
    }

    public IEnumerator SpawnDecorations()
    {
        int decorations = SetDecorationsToSpawn();
        while(decorations > 0)
        {
            InstantiateDecoration();
            decorations--;
            yield return new WaitForSeconds(Random.Range(1f,3f));
        }
    }

    public void InstantiateDecoration()
    {
        Decoration newDecoration = GameObject.Instantiate<Decoration>(decorationToSpawn[Random.Range(0, decorationToSpawn.Length)], transform.position, transform.rotation);
        // float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
        // float targetXPos = horzExtent - horzExtent / 4f - transform.position.x / 16f;
        // float distance = targetXPos + transform.position.x;
        // float xSpeed = Random.Range(minXSpeed, maxXSpeed);
        // decorationLifeTime = distance / xSpeed;
        // newDecoration.SetLifeTime(decorationLifeTime);
        float scale = Random.Range(minScaleSize, maxScaleSize);
        newDecoration.transform.localScale = new Vector3(scale, scale, 1);
        newDecoration.SetInitialYPosition(Random.Range(minYPosition, maxYPosition));
        newDecoration.SetXSpeed(Random.Range(minXSpeed, maxXSpeed));
        newDecoration.SetYSpeed(Random.Range(minYSpeed, maxYSpeed));
    }
}
