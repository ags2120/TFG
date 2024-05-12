using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class slide_metros : MonoBehaviour
{
    public TextMeshProUGUI metros,segundos;
    
    public UnityEngine.UI.Slider slider, slideRefresco,sliderMinutos;

    private string slider1Key = "Slider1Value";
    private string slider2Key = "Slider2Value";
    private string slider3Key = "Slider3Value";


    // Start is called before the first frame update
    void Start()
    {
        // Cargar los valores guardados si existen
        if (PlayerPrefs.HasKey(slider1Key))
        {
            slider.value = PlayerPrefs.GetFloat(slider1Key);
        }

        if (PlayerPrefs.HasKey(slider2Key))
        {
            slideRefresco.value = PlayerPrefs.GetFloat(slider2Key);
        }

        if (PlayerPrefs.HasKey(slider3Key))
        {
            sliderMinutos.value = PlayerPrefs.GetFloat(slider3Key);
        }

        // Agregar listeners para guardar los valores cuando los sliders cambien
        slider.onValueChanged.AddListener(delegate { SaveSliderValue(slider1Key, slider.value); });
        slideRefresco.onValueChanged.AddListener(delegate { SaveSliderValue(slider2Key, slideRefresco.value); });
        sliderMinutos.onValueChanged.AddListener(delegate { SaveSliderValue(slider3Key, sliderMinutos.value); });
    }
    void SaveSliderValue(string key, float value)
    {
        // Guardar el valor del slider en PlayerPrefs
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save(); // Guardar los cambios
    }
    // Update is called once per frame
    public void changeText()
    {
        int valor = (int)slider.value;

        metros.text = "("+valor.ToString()+" de 1000 metros)";
    }
    public void changeRefrescoText()
    {
        int valor = (int)slideRefresco.value;

        segundos.text = "(" + valor.ToString() + " de 5 minutos)";
    }
}
