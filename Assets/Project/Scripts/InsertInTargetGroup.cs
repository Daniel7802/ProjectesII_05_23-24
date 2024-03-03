using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertInTargetGroup : MonoBehaviour
{
    [SerializeField]
    BoomerangManager _boomerangManager;
    bool active = false;
    [SerializeField]
    float a, b;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !active)
        {
            _boomerangManager.m_TargetGroup.AddMember(this.transform, a, b);
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _boomerangManager.m_TargetGroup.RemoveMember(this.transform);

            active = false;
        }
    }
}
