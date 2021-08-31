using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBars : MonoBehaviour
{
    public BossHealthBar[] healthBars;
    private int currentHealthBarsCount;
    private string scene;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthBars()
    {
        HideHealthBars();
        scene = GameManager.instance.levelManager.currentScene;
        switch(scene)
        {
            case "Lvl1":
                currentHealthBarsCount = GameObject.FindObjectsOfType<GeminiShip>().Length;
                for(int i = 0; i < currentHealthBarsCount; i++)
                {
                    healthBars[i].bossHealth = GameObject.FindObjectsOfType<GeminiShip>()[i];
                    healthBars[i].gameObject.SetActive(true);
                }
                break;

            case "Lvl2":
                healthBars[0].bossHealth = GameObject.FindObjectOfType<Onslaught>();
                healthBars[0].gameObject.SetActive(true);
                break;

            case "Lvl3":
                if(GameObject.FindObjectOfType<VDL1976>() != null) healthBars[0].bossHealth = GameObject.FindObjectOfType<VDL1976>();
                else if(GameObject.FindObjectOfType<Hercules8999>() != null) healthBars[0].bossHealth = GameObject.FindObjectOfType<Hercules8999>();
                else if(GameObject.FindObjectOfType<LaBellezaDeCordera>() != null) healthBars[0].bossHealth = GameObject.FindObjectOfType<LaBellezaDeCordera>();
                healthBars[0].gameObject.SetActive(true);
                break;
            case "Lvl4":
                if(GameObject.FindObjectOfType<GeminiShip>() != null)
                {
                    currentHealthBarsCount = GameObject.FindObjectsOfType<GeminiShip>().Length;
                    for(int i = 0; i < currentHealthBarsCount; i++)
                    {
                        healthBars[i].bossHealth = GameObject.FindObjectsOfType<GeminiShip>()[i];
                        healthBars[i].gameObject.SetActive(true);
                    }
                    break;
                }
                else if(GameObject.FindObjectOfType<Onslaught>() != null)
                {
                    healthBars[0].bossHealth = GameObject.FindObjectOfType<Onslaught>();
                    healthBars[0].gameObject.SetActive(true);
                }
                else if(GameObject.FindObjectOfType<VDL1976>() != null && GameObject.FindObjectOfType<Hercules8999>() != null)
                {
                    healthBars[0].bossHealth = GameObject.FindObjectOfType<VDL1976>();
                    healthBars[0].gameObject.SetActive(true);
                    healthBars[1].bossHealth = GameObject.FindObjectOfType<Hercules8999>();
                    healthBars[1].gameObject.SetActive(true);
                }
                else
                {
                    healthBars[0].bossHealth = GameObject.FindObjectOfType<GreatNovaShip>();
                    healthBars[0].gameObject.SetActive(true);
                }
                break;
        }
    }

    public void HideHealthBars()
    {
        for(int i = 0; i < healthBars.Length; i++)
        {
            healthBars[i].gameObject.SetActive(false);
        }
    }
}
