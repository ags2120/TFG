using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class slide_minutos : MonoBehaviour
{
    public TextMeshProUGUI metros;
    public Slider slider;
    public float changeValue = 15f;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        OnSliderValueChanged();
    }

    // Update is called once per frame
    void OnSliderValueChanged()
    {

       
            float newValue = Mathf.Round(slider.value/changeValue) * changeValue;
            slider.value = newValue;
            int valor = (int)slider.value;

            metros.text = "(" + valor.ToString() + " de 1440 minutos)";
        
    }
}
