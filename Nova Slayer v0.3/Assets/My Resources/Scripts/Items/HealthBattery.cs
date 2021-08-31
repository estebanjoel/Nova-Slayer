using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBattery : Item
{
    public int healthPlus;

    public override void UseItem(NovaSlayerBody target)
    {
        target.health += healthPlus;
        if(target.health > target.maxHealth) target.health = target.maxHealth;
    }    
}
