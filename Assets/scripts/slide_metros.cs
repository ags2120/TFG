using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class slide_metros : MonoBehaviour
{
    public TextMeshProUGUI metros,segundos;
    
    public UnityEngine.UI.Slider slider, slideRefresco;
    public TMP_InputField input_mins, inputs_limit;

    private string slider1Key = "Slider1Value";
    private string slider2Key = "Slider2Value";
    private string input1Key = "Input1Value";
    private string input2Key = "Input2Value";


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

        if (PlayerPrefs.HasKey(input1Key))
        {
            inputs_limit.text = PlayerPrefs.GetInt(input1Key).ToString();
        }

        if (PlayerPrefs.HasKey(input2Key))
        {
            input_mins.text = PlayerPrefs.GetInt(input2Key).ToString();
        }

        // Agregar listeners para guardar los valores cuando los sliders cambien
        slider.onValueChanged.AddListener(delegate { SaveSliderValue(slider1Key, slider.value); });
        slideRefresco.onValueChanged.AddListener(delegate { SaveSliderValue(slider2Key, slideRefresco.value); });
        inputs_limit.onValueChanged.AddListener(delegate { SaveInputValue(input1Key, inputs_limit.text); });
        input_mins.onValueChanged.AddListener(delegate { SaveInputValue(input2Key, input_mins.text); });
    }
    void SaveSliderValue(string key, float value)
    {
        // Guardar el valor del slider en PlayerPrefs
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save(); // Guardar los cambios
    }
    void SaveInputValue(string key, string input)
    {
        int value;
        if (int.TryParse(input, out value))
        {
            // Aquí puedes guardar el valor donde necesites
            PlayerPrefs.SetInt(key, value);
            Debug.Log("Valor guardado: " + value);
        }
        


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
