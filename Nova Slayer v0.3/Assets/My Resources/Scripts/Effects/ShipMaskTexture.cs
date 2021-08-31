using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMaskTexture : MonoBehaviour
{
    public SpriteMask spriteMask;
    public Sprite parentSprite;
    // Start is called before the first frame update
    void Start()
    {
        parentSprite = transform.parent.GetComponent<SpriteRenderer>().sprite;
        spriteMask.sprite = parentSprite;
    }
    
}
