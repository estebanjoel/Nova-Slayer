using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner : MonoBehaviour
{
    //Spawner
    public Spawner mySpawn;
    //Enemies array
    [SerializeField] GameObject[] enemies;
    // Boss GameObject
    public GameObject boss;
    //Y positions array
    public float[] yPositions;
    //X positions array
    public float[] xPositions;
    //Array to check if a position is occupied
    public List<bool> occupiedPositions = new List<bool>();
    public int quantityOfEnemiesToSpawn;
    //Actual enemies wave
    public int actualWave=0;
    //Enemy waves
    public int waves;
    //Bool if boss is dead
    public bool bossIsDead;
    public bool canCheckBoss;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameManager.instance.victoryCondition && !GameManager.instance.failCondition)
        {
            if(!LookingForEnemies())
            {
                if(actualWave<=waves) spawnWave();
                else if(!CheckForBoss()) bossIsDead = true;
            }
        }
    }

    public bool LookingForEnemies()
    {
        if(GameObject.FindGameObjectWithTag("Spaceship")==null && GameObject.FindGameObjectWithTag("Boss") == null) return false;
        else return true;
    }

    public bool CheckForBoss()
    {
        if(GameObject.FindGameObjectWithTag("Boss") != null) return true;
        else return false;
    }

    public abstract void spawnWave();
    public abstract void spawnBoss();

    public bool CheckPosition(int xPos, int yPos)
    {
        int firstPos = ((yPos + 1) * xPositions.Length) - xPositions.Length;
        List<bool> SelectedPartOfList = occupiedPositions.GetRange(firstPos, xPositions.Length);
        for(int i = 0; i < SelectedPartOfList.Count; i++)
        {
            if(!SelectedPartOfList[i]) return false;
        }
        return true;
    }

    public bool AssignPosition(EnemyBody enemy, int xPos, int yPos)
    {
        if(!CheckPosition(xPos, yPos))
        {
            enemy.xPosition = xPositions[xPos];
            enemy.yPosition = yPositions[yPos];
            int posToOccupy = yPos * xPositions.Length + xPos;
            occupiedPositions[posToOccupy] = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpawnEnemies(int enemyQuant, int enemy)
    {
        mySpawn.prefabToSpawn=enemies[enemy];
        while(enemyQuant>0)
        {
            if(AssignPosition(mySpawn.prefabToSpawn.GetComponent<EnemyBody>(), Random.Range(0, xPositions.Length), Random.Range(0,yPositions.Length)))
            {
                mySpawn.Create();
                enemyQuant--;
            }
        }
    }

    public void SpawnEnemiesRandom(int enemyQuant, int minEnemy, int maxEnemy)
    {
        while(enemyQuant>0)
        {
            mySpawn.prefabToSpawn=enemies[(int)Random.Range(minEnemy,maxEnemy)];
            if(AssignPosition(mySpawn.prefabToSpawn.GetComponent<EnemyBody>(), Random.Range(0,xPositions.Length), Random.Range(0,yPositions.Length)))
            {
                mySpawn.Create();
                enemyQuant--;
            }
        }
    }

    public int SetQuantityOfEnemiesToSpawn(int easyQuantity, int mediumQuantity, int hardQuantity)
    {
        int quantity = 0;
        switch(GameManager.instance.currentDifficulty)
        {
            case 0:
                quantity = easyQuantity;
                break;
            case 1:
                quantity = mediumQuantity;
                break;
            case 2:
                quantity = hardQuantity;
                break;
            
        }
        return quantity;
    }
}
