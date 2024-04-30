using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField]
    Slider _slider;

    TextMeshProUGUI _textMeshPro;
    void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = _slider.value * 100;
        _textMeshPro.text = ((int)value).ToString();
    }
}
