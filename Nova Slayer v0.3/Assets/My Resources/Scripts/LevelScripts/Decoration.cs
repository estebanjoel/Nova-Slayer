using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    public float lifeTime;
    public float speedX;
    public float speedY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime > 0)
        {
            transform.position += new Vector3(1 * speedX * Time.deltaTime, 1 * speedY * Time.deltaTime, 0);
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetInitialYPosition(float yPos)
    {
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    public void SetXSpeed(float speed)
    {
        speedX = speed;
    }

    public void SetYSpeed(float speed)
    {
        speedY = speed;
    }

    public void SetLifeTime(float lTime)
    {
        lifeTime = lTime;
    }
}
