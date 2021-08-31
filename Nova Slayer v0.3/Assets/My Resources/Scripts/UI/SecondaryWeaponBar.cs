using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryWeaponBar : MonoBehaviour
{
    public int secondaryTarget;
    public Image fillImage;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(NovaSlayer.instance.body.secondaryBulletActive[secondaryTarget])
        {
            float amount = NovaSlayer.instance.body.secondaryBulletRemainingTimeToFire[secondaryTarget] / NovaSlayer.instance.body.secondaryBulletFireRate[secondaryTarget];
            fillImage.fillAmount = amount;
        }
    }
}
