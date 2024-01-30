using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutManager : MonoBehaviour
{
    public CanvasGroup canvasgroup;
    public bool fadeIn = false;
    public bool fadeOut = false;

    public float timeToFade;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn == true)
        {
            if(canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += timeToFade * Time.deltaTime;
                if(canvasgroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut == true)
        {
            
            if (canvasgroup.alpha >= 0)
            {
                canvasgroup.alpha -= timeToFade * Time.deltaTime;
                if (canvasgroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    public void Fadein ()
    {
        fadeIn = true;
    }

    public void Fadeout()
    {
        fadeOut = true;
    }
}
