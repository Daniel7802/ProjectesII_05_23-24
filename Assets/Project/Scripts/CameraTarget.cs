using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] BoomerangManager manager;
    [SerializeField] Transform player;
    [SerializeField] float threshold;

    Vector2 pAux, vectorDirection, vectorObjective;

    [SerializeField]
    float distance = 0;

    void Update()
    {
        if (manager._boomerangThrow.mouseHold)
        {
            if(distance <= 2)
            distance += 0.008f;
            CalculateTarger();
        }
        else
        {
            this.transform.position = player.position;
            distance = 0;
        }


    }

    void CalculateTarger()
    {
              
        pAux = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vectorDirection = (Vector3)pAux - player.position;
        vectorDirection.Normalize();
        vectorObjective = (vectorDirection) * distance + (Vector2)player.position;
        this.transform.position = vectorObjective;
    }
    
}
