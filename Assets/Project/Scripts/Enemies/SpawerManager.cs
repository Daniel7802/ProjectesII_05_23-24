using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SpawerManager : MonoBehaviour
{
    [SerializeField]
    private List<List<GameObject>> list;

    

    public List<List<GameObject>> List
    {
        get { return list; }
        set { list = value; }
    }

   public List<Enemy> spawns;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemiesAlive())
        {
            SpawnWave();
        }
    }

    private bool EnemiesAlive()
    {
        if (spawns.Count == 0)
        {
            return false;            
        }

        return false;
    }

    private void SpawnWave()
    {
        foreach (var item in list[list.Count]) 
        {
            item.SetActive(true);
        }
        list.RemoveAt(list.Count - 1);
    }
}
