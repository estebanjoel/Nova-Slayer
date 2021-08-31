using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaSlayer : MonoBehaviour
{
    public static NovaSlayer instance;
    public NovaSlayerBody body;
    public NovaSlayerBrain brain;
    public EffectsManager effectsManager;
    // Start is called before the first frame update
    void Start()
    {
        #region Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        #endregion
        body.SetInitialElements();
    }

    public void UpgradeStats()
    {
        switch(GameManager.instance.currentDifficulty)
        {
            case 0:
                body.maxHealth += 5;
                body.bulletPower++;
                for(int i = 0; i < body.secondaryBullets.Length; i++)
                {
                    body.secondaryBullets[i].GetComponent<Bullet>().power += 2.5f;
                }
                body.plasmaBombPower += 2.5f;
                break;
            case 1:
                body.maxHealth += 8;
                body.bulletPower++;
                for(int i = 0; i < body.secondaryBullets.Length; i++)
                {
                    body.secondaryBullets[i].GetComponent<Bullet>().power += 3f;
                }
                body.plasmaBombPower += 4f;
                break;
            case 2:
                body.maxHealth += 10;
                body.bulletPower += 2.5f;
                for(int i = 0; i < body.secondaryBullets.Length; i++)
                {
                    body.secondaryBullets[i].GetComponent<Bullet>().power += 5f;
                }
                body.plasmaBombPower += 10f;
                break;
        }
        
    }
}
