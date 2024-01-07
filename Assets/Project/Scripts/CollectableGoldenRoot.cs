using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollectableGoldenRoot : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _idleSound;
    [SerializeField] private AudioClip _collectedSound;
    private bool collectedSoundPlayed = false;
    bool collected = false;
    [SerializeField] float collectedTime = 2f;
    [SerializeField] float pickUpTime = 2f;
    float collectedTimer = 0f;

    GameObject target;

    [SerializeField] float height = 1.8f;
    [SerializeField] float newScale = 0.7f;
    [SerializeField] float speed = 30f;

    private Vector3 startPosition;
    private float idleVelocity = 2.0f;
    private float idleDistance = 0.2f;

    [SerializeField] GameObject ShopManager;

    private ShopBehaviour sb;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sb = ShopManager.GetComponent<ShopBehaviour>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _idleSound;
        _audioSource.loop = true;
        _audioSource.volume = 0.009f;
        _audioSource.Play();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {      

        if (collected)
        {
            if(!collectedSoundPlayed)
            {
                _audioSource.clip = null;
                _audioSource.PlayOneShot(_collectedSound, 1f);
                collectedSoundPlayed = true;
            }          
         

            if (collectedTimer < collectedTime)
            {
                transform.position = new Vector3(target.transform.position.x, target.transform.position.y + height, target.transform.position.z - 1);
                transform.localScale = new Vector3(newScale, newScale, 0);

                collectedTimer += Time.deltaTime;
            }
            else if (collectedTimer >= collectedTime && collectedTimer < collectedTime + pickUpTime)
            {
                transform.localScale = transform.localScale * 0.9f;
                transform.position = new Vector3(target.transform.position.x, transform.position.y-0.09f, target.transform.position.z - 1);
               
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.gameObject;
            if (!collected)
            {
                sb.currentRoots++;
                sb.currentRootsText.text = sb.currentRoots.ToString();
            }
            collected = true;
            
        }
    }
}
