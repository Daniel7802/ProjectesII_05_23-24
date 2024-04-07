using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Color _damageFlashColor = Color.red;
    [SerializeField] private Color _healFlashColor = Color.green;
    [SerializeField] private float flashTime = 0.25f;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private Material _material;
    private Coroutine _damageFlashCoroutine;


    private void Awake()
    {
        _material = _spriteRenderer.material;
    }

    public void CallDamageFlasher()
    {
        _damageFlashCoroutine = StartCoroutine(DamageFlashed());
    }
    public void CallHealFlasher()
    {
        _damageFlashCoroutine = StartCoroutine(HealFlashed());
    }
    private void Start()
    {
    }

    private IEnumerator DamageFlashed ()
    {
        _material.SetColor("_FlashColor", _damageFlashColor);
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flashTime) 
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / flashTime));
            SetFlashAmount(currentFlashAmount);
            yield return null;
        }
    }

    private IEnumerator HealFlashed()
    {
        _material.SetColor("_FlashColor", _healFlashColor);

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / flashTime));
            SetFlashAmount(currentFlashAmount);
            yield return null;
        }
    }

    private void SetFlashAmount(float amount)
    {
        _material.SetFloat("_FlashAmount", amount);
    }
}
