using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ocultar_mostrar_Gameobject : MonoBehaviour
{
    public GameObject boton_menu;
    public GameObject detector;
    void Start()
    {
        Mostrar_Panel_Detector(false); 
    }

    // Update is called once per frame
   
    public void Mostrar_Boton_Menu(bool mostrar) { boton_menu.SetActive(mostrar); }
    
    public void Mostrar_Panel_Detector(bool mostar) { detector.SetActive(mostar); }


}
