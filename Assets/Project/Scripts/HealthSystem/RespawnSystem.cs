using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField]
    FadeInOutManager _fadeInOutManager;

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
        _fadeInOutManager.Fadein();
        StartCoroutine(Respawn());
    }
  IEnumerator Respawn()    
    {
        yield return new WaitWhile(delegate { return _fadeInOutManager.fadeIn; });
        transform.position = checkpointPos1;
        _healthSystem.RespawnHeal();
        canvasDead.gameObject.SetActive(true);
    }
}
