using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    int numberOfRings;

    [SerializeField]
    GameObject[] extraHearts;

    [SerializeField]
    GameObject[] voidHearts;

    private int extraheartsCounter = 0;


    [SerializeField]
    private PlayerHealthSystem phs;

    // Start is called before the first frame update
    void Start()
    {
        numberOfRings = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseMaxHearts()
    {                     
        phs.MaxHealth++;
        phs.heartList.Add(extraHearts[extraheartsCounter]);
        extraHearts[extraheartsCounter].SetActive(true);
        voidHearts[extraheartsCounter].SetActive(true);
        extraheartsCounter++;
        phs.health = phs.MaxHealth;
        phs.RespawnHeal();        
    }

    public void AddRing()
    {
        numberOfRings++;
        if (numberOfRings >= 3)
        {
            IncreaseMaxHearts();
            numberOfRings = 0;
        }
    }

    public void ClearRings()
    {
        numberOfRings = 0;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ring"))
    //    {
           
    //    }
    //}
}
