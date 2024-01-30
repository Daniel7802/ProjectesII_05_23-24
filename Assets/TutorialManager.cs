using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    FadeInOutManager _fadeInOutManager;
    void Start()
    {
        _fadeInOutManager = GetComponent<FadeInOutManager>();
        _fadeInOutManager.Fadeout();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
