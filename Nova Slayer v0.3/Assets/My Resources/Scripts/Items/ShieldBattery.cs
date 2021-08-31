using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBattery : Item
{
    public override void UseItem(NovaSlayerBody target)
    {
        target.ActivateShield();
    }
}
