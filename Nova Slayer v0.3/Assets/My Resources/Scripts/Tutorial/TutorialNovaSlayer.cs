using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TutorialNovaSlayer : MonoBehaviour
{
     public float speed;
    public float leftBound, rightBound, topBound, bottomBound;
    //Nova Slayer's fire rate
    public float fireRate;
    //Nova Slayer's remaining time to fire
    public float remainingTimeToFire;
    //Nova Slayer's bullet speed
    public float bulletSpeed;
    //Nova Slayer's bullet power
    public float bulletPower;
    [SerializeField] Spawner mainFireSpawner;
    [SerializeField] Spawner SecondaryFireSpawner;
    [SerializeField] GameObject bullet;
    public GameObject[] secondaryBullets;
    public float[] secondaryBulletFireRate;
    public float[] secondaryBulletRemainingTimeToFire;
    public bool[] secondaryBulletActive;
    public GameObject currentSecondaryBullet;
    public GameObject plasmaBomb;
    public Spawner plasmaBombSpawner;
    //Plasma Bomb Power
    public float plasmaBombPower;
    public float plasmaBombFireRate;
    //Plasma Bomb Remaining Time To Fire
    public float plasmaBombRemainingTimeToFire;
    public Rigidbody2D rb;
    public bool canCheckKeys, canMove, canFireMainBullet, canFireSecondaryBullet, canFirePlasmaBomb;
    public bool hasMoved, hasShot;
     float horizontal, vertical;
    bool hadFired = false;
    public KeyCode[] alphas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetCameraBounds();
        remainingTimeToFire = fireRate;
        mainFireSpawner.prefabToSpawn = bullet;
        mainFireSpawner.prefabToSpawn.layer = 9;
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        bullet.GetComponent<Bullet>().power = bulletPower;
        plasmaBomb.GetComponent<PlasmaBomb>().power = plasmaBombPower;
        plasmaBombSpawner.prefabToSpawn = plasmaBomb;
        secondaryBulletActive[0] = true;
        SetCurrentSecondaryBullet();
    }

    // Update is called once per frame
    void Update()
    {
        if(canCheckKeys)
        {
            if(canMove)
            {
                horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
                vertical = CrossPlatformInputManager.GetAxis("Vertical");
                //Checks horizontal and vertical axis
                if(horizontal!=0 || vertical!=0)
                {
                    Move(horizontal, vertical);
                    hasMoved = true;
                }
                else
                {
                    if(rb.velocity!= Vector2.zero) rb.velocity= Vector2.zero;
                    hasMoved = false;
                }
            }

            if(canFireMainBullet)
            {
                if(CrossPlatformInputManager.GetAxis("Fire1")>0)
                {
                    if(remainingTimeToFire <= 0)
                    {
                        FireBullet();
                    }
                    else
                    {
                        hasShot = false;
                    }
                }
            }

            for(int i = 0; i < alphas.Length; i++)
            {
                if(Input.GetKeyDown(alphas[i]))
                {
                    if(getActiveSecondaryBullet() != i) setActiveSecondaryBullet(i);
                    SetCurrentSecondaryBullet();
                }
            }

            if(canFireSecondaryBullet)
            {
                if(CrossPlatformInputManager.GetAxis("Fire2") > 0)
                {
                    Debug.Log(getActiveSecondaryBullet());
                    if(secondaryBulletRemainingTimeToFire[getActiveSecondaryBullet()] >= secondaryBulletFireRate[getActiveSecondaryBullet()]) FireSecondaryBullet();
                }
            }

            if(canFirePlasmaBomb)
            {
                if(CrossPlatformInputManager.GetAxis("Fire3") > 0)
                {
                   if(plasmaBombRemainingTimeToFire <= 0) FirePlasmaBomb();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(remainingTimeToFire>0) remainingTimeToFire-=Time.deltaTime;
        if(secondaryBulletRemainingTimeToFire[getActiveSecondaryBullet()] <= secondaryBulletFireRate[getActiveSecondaryBullet()]) secondaryBulletRemainingTimeToFire[getActiveSecondaryBullet()] += Time.deltaTime;
        if(plasmaBombRemainingTimeToFire > 0) plasmaBombRemainingTimeToFire -= Time.deltaTime;
    }

    public void SetCameraBounds()
    {
        float verExtent = Camera.main.orthographicSize;
        float horzExtent = verExtent * Screen.width / Screen.height;
        rightBound = horzExtent - horzExtent / 4f - transform.position.x / 16f;
        leftBound = transform.position.x / 16f - horzExtent + horzExtent / 8f;
        bottomBound = (transform.position.y / 2f - verExtent) + 1.05f;
        topBound = (verExtent - transform.position.y / 2f) - 1.05f;
    }
    public void Move(float hAxis,float vAxis)
    {
        Vector3 movement = new Vector3(hAxis * speed * Time.deltaTime, vAxis * speed * Time.deltaTime,0);
        Vector3 newPos = transform.position + movement;
        newPos = CheckIfMovementExceedsBounds(newPos);
        rb.MovePosition(newPos);
    }

    public Vector3 CheckIfMovementExceedsBounds(Vector3 pos)
    {
        bool exceedLeftBound = false;
        bool exceedRightBound = false;
        bool exceedTopBound = false;
        bool exceedBottomBound = false;
        if(pos.x < leftBound) exceedLeftBound = true;
        if(pos.x > rightBound) exceedRightBound = true;
        if(pos.y > topBound) exceedTopBound = true;
        if(pos.y < bottomBound) exceedBottomBound = true;
        if(exceedLeftBound)
        {
            if(exceedTopBound) pos = new Vector3(leftBound, topBound, 0);
            else if(exceedBottomBound) pos = new Vector3(leftBound, bottomBound, 0);
            else pos = new Vector3(leftBound,pos.y, 0);
        }
        else if(exceedRightBound)
        {
            if(exceedTopBound) pos = new Vector3(rightBound, topBound, 0);
            else if(exceedBottomBound) pos = new Vector3(rightBound, bottomBound, 0);
            else pos = new Vector3(rightBound,pos.y, 0);
        }
        else
        {
            if(exceedTopBound) pos = new Vector3(pos.x, topBound, 0);
            else if(exceedBottomBound) pos = new Vector3(pos.x, bottomBound, 0);
            else pos = new Vector3(pos.x,pos.y, 0);
        }
        return pos;
    }

    public void FireBullet()
    {
        mainFireSpawner.Create();
        remainingTimeToFire=fireRate;
        hasShot = true;
    }

     public void FireSecondaryBullet()
    {
        SecondaryFireSpawner.Create();
        secondaryBulletRemainingTimeToFire[getActiveSecondaryBullet()] = 0;
    }

    public void FirePlasmaBomb()
    {
        plasmaBombSpawner.Create();
        plasmaBombRemainingTimeToFire = plasmaBombFireRate;
    }

    public void SetCurrentSecondaryBullet()
    {
        currentSecondaryBullet = secondaryBullets[getActiveSecondaryBullet()];
        SecondaryFireSpawner.prefabToSpawn = secondaryBullets[getActiveSecondaryBullet()];
    }

    public void setActiveSecondaryBullet(int i)
    {
        DeactivateAllSecondaryBullets();
        secondaryBulletActive[i] = true;
    }

    public int getActiveSecondaryBullet()
    {
        for(int i = 0; i < secondaryBulletActive.Length; i++)
        {
            if(secondaryBulletActive[i]) return i;
        }
        return -1;
    }

    public void DeactivateAllSecondaryBullets()
    {
       for(int i = 0; i < secondaryBulletActive.Length; i++)
       {
           secondaryBulletActive[i] = false;
       }
    }
}
