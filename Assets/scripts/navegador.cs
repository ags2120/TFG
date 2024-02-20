using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.UI;

public enum Panel
{
    panelMenuInicial,
    panelFuentesDatos,
    panelNuevaFuente,
}
public class navegador : MonoBehaviour
{
   
    public GameObject panelMenuInicial;
    public GameObject panelFuentesDatos;
    public GameObject panelNuevaFuente;
    
    public Panel currentPanel;

    public GameObject flecha;
    private bool flechaClicked = false;
 
    
    public void Update()
    {
        if (currentPanel == Panel.panelMenuInicial && flecha.activeSelf)
        {
            flecha.SetActive(false);
        }
        else if(currentPanel != Panel.panelMenuInicial && flecha.activeSelf == false)
        {
            flecha.SetActive(true);
        }
        
    }
    public void OnClickNavegador()
    {
        panelMenuInicial.SetActive(false);
        panelFuentesDatos.SetActive(false);
        panelNuevaFuente.SetActive(false);

        switch (currentPanel)
        {
            case Panel.panelMenuInicial:

                    panelFuentesDatos.SetActive(true);
                    currentPanel = Panel.panelFuentesDatos;
                   
                break;

            case Panel.panelFuentesDatos:

                if (flechaClicked == true)
                {
                    panelMenuInicial.SetActive(true);
                    currentPanel = Panel.panelMenuInicial;
                }
                else
                {
                    panelNuevaFuente.SetActive(true);
                    currentPanel = Panel.panelNuevaFuente;
                }
                break;

            case Panel.panelNuevaFuente:

                if (flechaClicked == true)
                {
                    panelFuentesDatos.SetActive(true);
                    currentPanel = Panel.panelFuentesDatos;
                }
                else
                {
                   //añadir la acción al pulsar el botón
                }

                break;
                
        }
    }
    public void OnFlechaClick()
    {
        flechaClicked = true;
        OnClickNavegador();
        flechaClicked = false;
    }
}
