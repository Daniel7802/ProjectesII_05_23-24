using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllSelectionMenu : MonoBehaviour
{
    Animator _animator;
    [SerializeField]
    AudioSource _audioSourceSelected;
    [SerializeField]
    AudioSource _audioSourcePressed;

    bool used = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetBool("Highlighted") && !used)
        {
            _audioSourceSelected.Play();
            used = true;
        }
        else if (_animator.GetBool("Pressed") && !used)
        {
            _audioSourcePressed.Play();
            used = true;
        }
        else
            used = false;
    }
}
