using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float initialXPos, initialYPos, xMovement, yMovement;
    public float xSpeed;
    public float ySpeed;
    public float lifeTime;
    public float impactPower;
    public Rigidbody2D rb;
    public GameObject explosion;
    public float explosionScale;
    public AudioClip explosionClip;
    void Start()
    {
        explosion.transform.localScale = new Vector3(explosionScale, explosionScale, 1);
        AssignInitialPosition();
    }

    public void AssignInitialPosition()
    {
        transform.position = new Vector3(initialXPos, initialYPos, 0);
    }

    public void AssignMovement()
    {
        if(initialXPos >= 0) xMovement = -1 * xSpeed * Time.deltaTime;
        else xMovement = 1 * xSpeed * Time.deltaTime;
        if(initialYPos >= 0) yMovement = -1 * ySpeed * Time.deltaTime;
        else yMovement = 1 * ySpeed * Time.deltaTime;
    }

    public void Move()
    {
        AssignMovement();
        Vector3 Movement = new Vector3(xMovement, yMovement, 0);
        Movement = transform.position + Movement;
        rb.MovePosition(Movement);
        // Quaternion newRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + 1 * xSpeed * Time.deltaTime, transform.rotation.w);
        // if(newRotation.z > 180) newRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + 0, transform.rotation.w);
        // rb.MoveRotation(newRotation);
        transform.Rotate(transform.forward * xSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime > 0)
        {
            Move();
            // lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Explode()
    {
        GameObject.Instantiate(explosion, transform.position, transform.rotation);
        if(!AudioManager.instance.sfxSources[7].isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[7], explosionClip);
            AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[7]);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Explode();
        }
    }
}
