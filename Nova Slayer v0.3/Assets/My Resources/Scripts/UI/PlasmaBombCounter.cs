using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlasmaBombCounter : MonoBehaviour
{
    public Text counterText;

    public void SetCounter(int ammo)
    {
        counterText.text = "X"+ammo;
    }
}
