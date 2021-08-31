using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeaponButton : MonoBehaviour
{
    public int secondaryTarget;

    public void Click()
    {
        if(NovaSlayer.instance.body.secondaryBulletAmmo[secondaryTarget] > 0)
        {
            UICanvas.instance.uIAudio.PlaySecondaryWeaponSelectAudio();
            NovaSlayer.instance.body.setActiveSecondaryBullet(secondaryTarget);
        } 
    }
}
