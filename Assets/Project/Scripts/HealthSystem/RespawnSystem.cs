using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{
    // Start is called before the first frame update
    
   public Vector2 checkpointPos1;
    PlayerHealthSystem _healthSystem;

    private void Start()
    {
        checkpointPos1 = transform.position;    
        _healthSystem = GetComponent<PlayerHealthSystem>();   
    }
    public void UpdateCheckPoint(Vector2 pos)
    {
        checkpointPos1 = pos;
    }
    public void OnDeath ()   
    {
        Invoke("Respawn", 0f);
    }
  public void  Respawn()    
    {
        transform.position = checkpointPos1;
        _healthSystem.RespawnHeal();
    }
}
