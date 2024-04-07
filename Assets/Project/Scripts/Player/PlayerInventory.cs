using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    int numberOfRings;

    [SerializeField]
    Animator ringAnimator;

    [SerializeField]
    List<Image> ringsImage;

    [SerializeField]
    GameObject[] extraHearts;

    [SerializeField]
    GameObject[] voidHearts;

    private int keys = 2;

    private int extraheartsCounter = 0;

    Color alpha;


    [SerializeField]
    private PlayerHealthSystem phs;

    // Start is called before the first frame update
    void Start()
    {
        numberOfRings = 0;
        alpha = ringsImage[0].color;
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
            ClearRings();
        }
        else
        {
            ringAnimator.SetTrigger("show");
            Color nuevoColor = ringsImage[numberOfRings - 1].color;
            nuevoColor.a = 1f;
            ringsImage[numberOfRings - 1].color = nuevoColor;
        }
    }

    public void ClearRings()
    {
        numberOfRings = 0;
        foreach(Image img in ringsImage)
        {
            img.color = alpha;
        }
    }

    public void IncreaseKeys()
    {
        keys++;
    }

    public int CheckKeys()
    {
        return keys;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ring"))
    //    {
           
    //    }
    //}
}
