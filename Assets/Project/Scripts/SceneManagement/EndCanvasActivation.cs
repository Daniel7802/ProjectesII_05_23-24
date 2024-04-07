using System.Collections;
using UnityEngine;

public class EndCanvasActivation : MonoBehaviour
{
    [SerializeField] private Canvas endCanvas;   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FadeInOutManager.instance.Fadein();
            StartCoroutine(End());
        }
    }
    IEnumerator End()
    {
        yield return new WaitWhile(delegate { return FadeInOutManager.instance.fadeIn; });      
        
        endCanvas.gameObject.SetActive(true);
    }
}
