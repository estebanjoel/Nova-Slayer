using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneOffsetMovement : MonoBehaviour
{
    Material material;
    Vector2 offset;
    public float xSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2(xSpeed, 0);
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
