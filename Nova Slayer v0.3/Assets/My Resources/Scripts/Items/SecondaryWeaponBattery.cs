using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeaponBattery : Item
{
    public int selectedWeapon;
    public int minAmmo, maxAmmo;
    public override void UseItem(NovaSlayerBody target)
    {
        target.AddSecondaryBulletAmmo(selectedWeapon, Random.Range(minAmmo, maxAmmo));
        UICanvas.instance.secondaryWeaponsUI.SetAmmoText(selectedWeapon, NovaSlayer.instance.body.secondaryBulletAmmo[selectedWeapon]);
    }

}
