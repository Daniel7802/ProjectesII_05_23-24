using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    enum DIR { UP, DOWN, RIGHT, LEFT };

    DIR dir;
    [SerializeField] bool up;
    [SerializeField] bool down;
    [SerializeField] bool right;
    [SerializeField] bool left;

    [SerializeField] private GameObject trapBullet;

    private Vector2 direction = Vector2.zero;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip shootSound;

    [SerializeField] private float reloadingTime = 3f;
    [SerializeField] private float startTime;
    private bool activate = false;
    private float reloadingTimer = 0f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();       
    }
    private void FixedUpdate()
    {
        if (startTime < 0)
            activate = true;
        else
        startTime -= Time.deltaTime;

        if (activate)
        {
            if (reloadingTimer < reloadingTime)
            {
                reloadingTimer += Time.deltaTime;
            }
            else
            {
                if (up)
                {
                    ShootOneBullet(DIR.UP);
                }
                if (down)
                {
                    ShootOneBullet(DIR.DOWN);
                }
                if (right)
                {
                    ShootOneBullet(DIR.RIGHT);
                }
                if (left)
                {
                    ShootOneBullet(DIR.LEFT);
                }

                reloadingTimer = 0f;
            }
        }
        
    }

    void ShootOneBullet(DIR dir)
    {
        _audioSource.PlayOneShot(shootSound, 0.2f);
        switch (dir)
        {
            case DIR.UP:
                direction = transform.up;
                break;
            case DIR.DOWN:
                direction = -transform.up;
                break;
            case DIR.RIGHT:
                direction = transform.right;
                break;
            case DIR.LEFT:
                direction = -transform.right;
                break;
        }
       
        GameObject bullet = Instantiate(trapBullet);
        bullet.transform.position = transform.position;
        bullet.transform.right = direction;

    }
}
