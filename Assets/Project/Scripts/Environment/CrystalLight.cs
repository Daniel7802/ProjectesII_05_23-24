using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CrystalLight : MonoBehaviour
{
    [SerializeField]
    Light2D _light;
    [SerializeField]
    bool _bigCrystal = false;

    [SerializeField]
    float _valueToDark = 0;
    bool _enabled = false;
    
    void Update()
    {
        if (_enabled) 
        {
            if (_light.intensity < 2.0f)
                _light.intensity += 0.1f;
            else
                _enabled = false;
        }
        else
        {
            if (!_bigCrystal && _light.intensity > 0)
                _light.intensity -= _valueToDark;
        }
    }


    private void ActiveLight()
    {
        _enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boomerang") )
        {
            ActiveLight();
        }
    }

   
}
