using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ocultar_mostrar_Gameobject : MonoBehaviour
{
    public GameObject boton_menu;
    public GameObject detector;
    public GameObject flechas;
    public GameObject raycaster;
    void Start()
    {
        Mostrar_Panel_Detector(false); 
        Mostrar_Flechas_panelInfo(false);
        
    }

    // Update is called once per frame
   
    public void Mostrar_Boton_Menu(bool mostrar) { boton_menu.SetActive(mostrar); }
    
    public void Mostrar_Panel_Detector(bool mostar) { detector.SetActive(mostar); }

    public void Mostrar_Flechas_panelInfo(bool mostrar) { flechas.SetActive(mostrar); }

    public void Mostrar_Panel_RayCast(bool mostrar) { raycaster.SetActive(mostrar); }

}
