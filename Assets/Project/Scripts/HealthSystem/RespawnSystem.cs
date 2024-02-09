using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{

    [SerializeField]
    Canvas canvasDead;

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
        //Time.fixedDeltaTime = Time.timeScale * 0.5f; 
        FadeInOutManager.instance.Fadein();
        StartCoroutine(Respawn());
    }
  IEnumerator Respawn()    
    {
        yield return new WaitWhile(delegate { return FadeInOutManager.instance.fadeIn; });
        //Time.fixedDeltaTime = Time.timeScale * 2;

        transform.position = checkpointPos1;
        _healthSystem.RespawnHeal();
        canvasDead.gameObject.SetActive(true);
    }
}
