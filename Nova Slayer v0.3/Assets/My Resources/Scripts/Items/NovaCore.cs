using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaCore : Item
{
    public override void UseItem(NovaSlayerBody target)
    {
        target.lives++;
        UICanvas.instance.lifeCounter.ChangeLifeCounterText(target.lives.ToString());
    }
}
