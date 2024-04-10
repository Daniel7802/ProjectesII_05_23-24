using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    [SerializeField]
    private Torch[] torch;
    int activated = 0;

    [SerializeField]
    private bool activeMechanisme;

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
        for (int i = 0; i < torch.Length; i++)
        {
            if (torch[i].isActive)
                activated++;
        }
        if (activated == torch.Length)
        {
            activeMechanisme = true;
        }
        else
        {
            activated = 0;
        }

        if (activeMechanisme)
        {
            StartCoroutine(PlaySoundAndDestroy());
        }
    }

    IEnumerator PlaySoundAndDestroy()
    {
       if(sp!=null)sp.enabled = false;
        if (!soundPlayed)
        {
            _audioSource.PlayOneShot(_destroySound);
            soundPlayed = true;
        }
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);

    }
}


