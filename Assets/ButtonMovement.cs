using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 5;
    public float magnitude = 5;
    RectTransform rectTransform;
    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public float SineAmount()
    {
        //Debug.Log(magnitude * Mathf.Sin(Time.time * speed));
        return  magnitude * Mathf.Sin(Time.time * speed);
    }

    public void Update()
    {
        Debug.Log(new Vector2(rectTransform.anchoredPosition.x, SineAmount()));

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, SineAmount());
    }
}
