using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActivateArena : MonoBehaviour
{
    public GameObject arena;

    //spikes
    private float spikesSpawnSeconds = 0.1f;
    public bool spikesShowed = false;
    //public GameObject spikesParent;
    public List<GameObject> entranceWalls = new List<GameObject>();
    public List<GameObject> exitWalls = new List<GameObject>();
    bool end = false;

    void Update()
    {
        if (spikesShowed)
        {
            arena.SetActive(true);
            spikesShowed = false;
        }
        if (arena.GetComponent<WaveSpawner>().isFinished&&!end)
        {
            StartCoroutine(DeleteAllWalls());
            end = true;
        }

    }

  

    private IEnumerator ShowAllWalls()
    {
        for (int i = 0; i < exitWalls.Count; i++)
        {
            exitWalls[i].SetActive(true);
            yield return new WaitForSeconds(spikesSpawnSeconds);
        }

        for (int i = 0; i< entranceWalls.Count; i++)
        {
            entranceWalls[i].SetActive(true);           
            yield return new WaitForSeconds(spikesSpawnSeconds);
        }
        
        spikesShowed = true;
    }
 
    private IEnumerator DeleteAllWalls()
    {
      
        for (int i = 0; i < exitWalls.Count; i++)
        {
            exitWalls[i].SetActive(false);

            yield return new WaitForSeconds(spikesSpawnSeconds);
        }
        //for (int i = 0; i < entranceWalls.Count; i++)
        //{
        //    entranceWalls[i].SetActive(false);
          
        //    yield return new WaitForSeconds(spikesSpawnSeconds);
        //}
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&!end)
        {
            StartCoroutine(ShowAllWalls());
        }
    }
}
