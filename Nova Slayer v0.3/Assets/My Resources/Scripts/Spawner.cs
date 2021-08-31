using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    Vector3 newPosition;

    public void AssignParent(GameObject newParent, GameObject newChild)
    {
        newChild.transform.SetParent(newParent.transform);
    }

    public void SetParentPosition(float xPosition, float yPosition)
    {
        newPosition = new Vector3(xPosition, yPosition, 0);
    }

    public Vector3 GetParentPosition()
    {
        return newPosition;
    }

    public void AssignPosition(GameObject target, Vector3 newPos)
    {
        target.transform.position += newPos;
    }

    public void Create(){

        GameObject newObject = GameObject.Instantiate(prefabToSpawn);
        if(newObject.layer != 11) newObject.transform.position = transform.position;
        
        if(newObject.layer==10)
        {
            AssignParent(transform.parent.gameObject, newObject);
            AssignPosition(newObject, GetParentPosition());
        }
    }
}
