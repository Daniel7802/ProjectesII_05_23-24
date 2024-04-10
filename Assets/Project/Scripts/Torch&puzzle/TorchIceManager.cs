using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchIceManager : MonoBehaviour
{
    [SerializeField]
    private Torch[] activeTorch;
    [SerializeField]
    private Torch[] nonActiveTorch;
    int activated = 0;
    int nonActivated = 0;

    [SerializeField]
    public bool activeMechanisme;

    [SerializeField]
    private bool hasSoundOnDestroy;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _destroySound;

    SpriteRenderer sp;
    bool soundPlayed = false;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        for (int i = 0; i < activeTorch.Length; i++)
        {
            if (activeTorch[i].isActive)
                activated++;
        }

        for (int i = 0; i < nonActiveTorch.Length; i++)
        {
            if (!nonActiveTorch[i].isActive)
                nonActivated++;
        }
        if (nonActivated == nonActiveTorch.Length && activated == activeTorch.Length)
        {
            activeMechanisme = true;
        }
        else
        {
            activated = 0;
            nonActivated = 0;
        }

        if (activeMechanisme)
        {
            StartCoroutine(PlaySoundAndDestroy());
        }


    }
    IEnumerator PlaySoundAndDestroy()
    {
        sp.enabled = false;
        if(!soundPlayed)
        {
            _audioSource.PlayOneShot(_destroySound);
            soundPlayed = true;
        }
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);

    }
}

