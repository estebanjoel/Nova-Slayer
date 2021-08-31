using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryWeaponsUI : MonoBehaviour
{
    public GameObject[] secondaryWeaponBars;
    public Button[] secondaryWeaponButton;
    public Text[] secondaryWeaponAmmo;
    public GameObject[] secondaryWeaponButtonDisable;

    void Update()
    {
        for(int i = 0; i < secondaryWeaponAmmo.Length; i++)
        {
            if(int.Parse(secondaryWeaponAmmo[i].text) == 0) DisableButton(i);
            else EnableButton(i);
        }
    }

    public void SetAmmoText(int i, int ammo)
    {
        secondaryWeaponAmmo[i].text = ammo.ToString();
    }

    public void HideButton(int i)
    {
        secondaryWeaponButton[i].gameObject.SetActive(false);
    }

    public void ShowButton(int i)
    {
        secondaryWeaponButton[i].gameObject.SetActive(true);
    }

    public void HideBar(int i)
    {
        secondaryWeaponBars[i].gameObject.SetActive(false);
    }

    public void ShowBar(int i)
    {
        secondaryWeaponBars[i].gameObject.SetActive(true);
    }

    public void DisableButton(int i)
    {
        secondaryWeaponButton[i].enabled = false;
        secondaryWeaponButtonDisable[i].SetActive(true);
    }
    public void EnableButton(int i)
    {
        secondaryWeaponButton[i].enabled = true;
        secondaryWeaponButtonDisable[i].SetActive(false);
    }
}
