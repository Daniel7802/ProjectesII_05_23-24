using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActivateArena : MonoBehaviour
{
    public GameObject arena;

    //spikes
    private float spikesSpawnSeconds = 0.3f;
    public bool spikesShowed = false;
    //public GameObject spikesParent;
    public List<GameObject> spikes = new List<GameObject>();
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
            StartCoroutine(DeleteAllSpikes());
            end = true;
        }

    }

  

    private IEnumerator ShowSpikes()
    {
      
        for(int i = 0; i< spikes.Count/2; i++)
        {
            spikes[i].SetActive(true);
            spikes[i+ (spikes.Count / 2)].SetActive(true);
            yield return new WaitForSeconds(spikesSpawnSeconds);
        }
        spikesShowed = true;
    }
    private IEnumerator DeleteHalfSpikes()
    {
       // spikes.Reverse();
        for (int i = 0; i < spikes.Count / 2; i++)
        {
            //spikes[i].SetActive(true);
            spikes[i + (spikes.Count / 2)].SetActive(false);
            yield return new WaitForSeconds(spikesSpawnSeconds);
        }

    }
    private IEnumerator DeleteAllSpikes()
    {
        // spikes.Reverse();
        for (int i = 0; i < spikes.Count / 2; i++)
        {
            spikes[i].SetActive(false);
            spikes[i + (spikes.Count / 2)].SetActive(false);
            yield return new WaitForSeconds(spikesSpawnSeconds);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&!end)
        {
            StartCoroutine(ShowSpikes());
        }
    }
}
