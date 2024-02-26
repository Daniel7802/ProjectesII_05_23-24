using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonBullet;

   
    [SerializeField]
    public bool wait;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip shootSound;

    [SerializeField] private float reloadingTime = 3f;
    [SerializeField] private float startTime;
    private bool activate = false;
    private float reloadingTimer = 0f;

    Animator _animator;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!wait)
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

                    StartCoroutine(ShootOneBullet());


                    reloadingTimer = 0f;
                }
            }

        }

    }
    
    IEnumerator ShootOneBullet()
    {
        _animator.SetBool("Shoot", true);
        _audioSource.PlayOneShot(shootSound, 0.2f);
        yield return new WaitForSeconds(0.2f);
        GameObject bullet = Instantiate(cannonBullet);
        bullet.transform.position = transform.position;
        bullet.transform.right = transform.right;
        _animator.SetBool("Shoot", false);


    }
}
