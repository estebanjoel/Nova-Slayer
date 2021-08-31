using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBombContainer : Item
{
    public override void UseItem(NovaSlayerBody target)
    {
        target.plasmaBombAmmo++;
        UICanvas.instance.plasmaBombCounter.SetCounter(target.plasmaBombAmmo);
    }
}
