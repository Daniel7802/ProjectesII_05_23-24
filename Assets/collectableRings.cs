using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectableRings : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _collectedSound;
    private bool collectedSoundPlayed = false;
    public bool collected = false;

    [SerializeField] float collectedTime = 2f;
    [SerializeField] float pickUpTime = 2f;
    float collectedTimer = 0f;

    GameObject target;

    [SerializeField]
    PlayerInventory _playerInvetory;

    [SerializeField]
    private Vector3 startPosition;
    private float idleVelocity = 2.0f;
    private float idleDistance = 0.2f;

    public virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (collected)
        {
            if (!collectedSoundPlayed)
            {
                _audioSource.clip = null;
                _audioSource.PlayOneShot(_collectedSound);
                collectedSoundPlayed = true;
            }

            if (collectedTimer < collectedTime)
            {
                transform.position = new Vector3(target.transform.position.x, target.transform.position.y + height, target.transform.position.z - 1);
                collectedTimer += Time.deltaTime;
            }
            else if (collectedTimer >= collectedTime && collectedTimer < collectedTime + pickUpTime)
            {
                transform.localScale *= 0.9f;
                transform.position = new Vector3(target.transform.position.x, transform.position.y - 0.09f, target.transform.position.z - 1);
                collectedTimer += Time.deltaTime;
            }
            else
            {
                collected = false;
                collectedTimer = 0f;
                Destroy(gameObject);
            }
        }
        else
        {
            float movement = Mathf.Sin(Time.time * idleVelocity) * idleDistance;
            transform.position = startPosition + new Vector3(0, movement, 0);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.gameObject;
            if (!collected)
            {
                collision.GetComponent<PlayerInventory>().AddRing();
                collected = true;
            }
        }
    }
}