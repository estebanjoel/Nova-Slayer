using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyBody myParent;
    public Transform lifeFiller;
    private float currentFillAmount;
    private float xScale;
    // public EnemyBody myParent;
    // public Transform lifeFiller;
    // public float currentFillAmount;
    // public float fillAmount;
    void Start()
    {
        xScale = lifeFiller.localScale.x;
        currentFillAmount = (myParent.maxHealth / myParent.maxHealth) * xScale;
        lifeFiller.localScale = new Vector3(currentFillAmount, lifeFiller.localScale.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(myParent.maxHealth / myParent.maxHealth<=0) currentFillAmount = 0;
        else currentFillAmount = (myParent.health / myParent.maxHealth) * xScale;

        lifeFiller.localScale = new Vector3(currentFillAmount, lifeFiller.localScale.y, 0);
        if(currentFillAmount == 0) gameObject.SetActive(false);
        
    }
}
