using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image Filler;
    public GameObject healthBarOwner;
    private float healthMax;
    
    // Start is called before the first frame update
    void Start()
    {
        Filler.fillAmount = 1f;
        if (healthBarOwner.tag == "Player") healthMax = NovaSlayer.instance.body.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBarOwner != null)
        {
            if (healthBarOwner.tag == "Player")Filler.fillAmount = ((healthBarOwner.GetComponent<NovaSlayerBody>().health * 1) / healthMax);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }
}
