using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleShotBattery : Item
{
    public override void UseItem(NovaSlayerBody target)
    {
        target.ActivateMultipleFire();
    }
}
