using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    [SerializeField]
    private Torch[] torch;
    int torchesActivated = 0;

 
    public bool activated;

    [SerializeField]
    private bool destroyObjectWhenActivated;

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
                torchesActivated++;
        }
        if (torchesActivated == torch.Length)
        {
            activated = true;
        }
        else
        {
            torchesActivated = 0;
        }

        if (activated)
        {
            StartCoroutine(PlaySoundAndDestroy());
        }
    }

    IEnumerator PlaySoundAndDestroy()
    {
        if(destroyObjectWhenActivated&&sp!=null)
        {
            sp.enabled = false;
        }
        if (!soundPlayed)
        {
            _audioSource.PlayOneShot(_destroySound);
            soundPlayed = true;
        }
        yield return new WaitForSecondsRealtime(1f);
        if(destroyObjectWhenActivated)
        {
            Destroy(gameObject);
        }
       

    }
}


