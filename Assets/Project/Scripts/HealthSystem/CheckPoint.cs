using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<RespawnSystem>(out RespawnSystem rS))
        {
            rS.UpdateCheckPoint(transform.position);
        }
    }
}
