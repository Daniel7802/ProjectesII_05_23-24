using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class HeartsManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] hearts;

    public int counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void printHearts()
    {
        for (int i = 0; i < counter; i++)
        {
            hearts[i].SetActive(false);
        }
    }
}
