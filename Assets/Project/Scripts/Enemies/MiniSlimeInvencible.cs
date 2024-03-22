using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlimeInvencible : MonoBehaviour
{
    [SerializeField] float invTime = 0.5f;
    HealthSystem hs;
    void Start()
    {
        hs = GetComponent<HealthSystem>();
        StartCoroutine(UnInvencible());
    }

    IEnumerator UnInvencible()
    {
        yield return new WaitForSecondsRealtime(invTime);
        hs.TurnInvencible(false);

    }
}

