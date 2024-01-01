using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableGoldenRoot : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    bool collected = false;
    [SerializeField] float collectedTime = 2f;
    [SerializeField] float pickUpTime = 2f;
    float collectedTimer = 0f;

    GameObject target;

    [SerializeField] float height = 1.8f;
    [SerializeField] float newScale = 0.7f;
    [SerializeField] float speed = 30f;

    [SerializeField]
    GameObject ShopManager;

    private ShopBehaviour sb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sb = ShopManager.GetComponent<ShopBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (collected)
        {
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
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.gameObject;
            collected = true;
            sb.currentRoots++;
            sb.currentRootsText.text = sb.currentRoots.ToString();
        }
    }
}
