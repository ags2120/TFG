using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class slide_metros : MonoBehaviour
{
    public TextMeshProUGUI metros;
    public Slider slider;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void changeText()
    {
        int valor = (int)slider.value;

        metros.text = "("+valor.ToString()+" de 1000 metros)";
    }
}
