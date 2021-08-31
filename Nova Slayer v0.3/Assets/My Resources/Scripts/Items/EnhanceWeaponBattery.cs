using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceWeaponBattery : Item
{
    public override void UseItem(NovaSlayerBody target)
    {
        target.ActivateWeaponEnhancement();
    }
}
