using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSystem : MonoBehaviour
{
  public virtual void Dead()
    {
        Destroy(gameObject);
    }
}
