using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public Text lifeCounterText;

    public void ChangeLifeCounterText(string lives)
    {
        lifeCounterText.text = "x"+lives;
    }
}
