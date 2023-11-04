using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{
    // Start is called before the first frame update
    
   public Vector2 checkpointPos1;



    private void Start()
    {
        checkpointPos1 = transform.position;    
    }
    public void UpdateCheckPoint(Vector2 pos)
    {
        checkpointPos1 = pos;
    }
    public void OnDeath ()   
    {
        Invoke("Respawn", 1f);
    }
  public void  Respawn()
    {       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
