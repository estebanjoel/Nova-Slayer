using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public EnemyBody bossHealth;
    public Image bossHealthFill;
    private float FillAmount;

    void Update()
    {
        if(bossHealth != null)
        {
            FillAmount = (bossHealth.health / bossHealth.maxHealth);
            bossHealthFill.fillAmount = FillAmount;
        }
    }
}
