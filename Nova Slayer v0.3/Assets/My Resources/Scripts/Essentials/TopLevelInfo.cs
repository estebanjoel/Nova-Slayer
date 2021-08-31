using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLevelInfo : MonoBehaviour
{
    public int beginningPlayerLives;
    public int[] beginningPlayerSecondaryAmmo;
    public int beginningPlayerPlasmaBombAmmo;

    public void SetBeginningPlayerLives(int playerLives)
    {
        beginningPlayerLives = playerLives;
    }

    public int GetBeginningPlayerLives()
    {
        return beginningPlayerLives;
    }

    public void SetBeginningSecondaryAmmo(int[] playerSecondaryAmmo)
    {
        for(int i = 0; i < playerSecondaryAmmo.Length; i++)
        {
            beginningPlayerSecondaryAmmo[i] = playerSecondaryAmmo[i];
        }
    }
    public int[] GetBeginningSecondaryAmmo()
    {
        return beginningPlayerSecondaryAmmo;
    }

    public void SetBeginningPlasmaBombAmmo(int ammo)
    {
        beginningPlayerPlasmaBombAmmo = ammo;
    }

    public int GetBeginningPlasmaBombAmmo()
    {
        return beginningPlayerPlasmaBombAmmo;
    }
}
