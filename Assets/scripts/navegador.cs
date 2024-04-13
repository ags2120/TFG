using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;

public enum Panel
{
    panelMenuInicial,
    panelFuentesDatos,
    panelNuevaFuente,
    panelEditarFuente,
}
public class navegador : MonoBehaviour
{
   
    public GameObject panelMenuInicial;
    public GameObject panelFuentesDatos;
    public GameObject panelNuevaFuente;
    public GameObject panelEditarFuente;
    //public GameObject[] panels;
    public Panel currentPanel;

    public GameObject boton_editar;
    public GameObject flecha;
    private bool flechaClicked = false, editarClicked = false;
    private EditarFuente editarFuente;

    void Start()
    {
        editarFuente = FindObjectOfType<EditarFuente>();
    }
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
        panelEditarFuente.SetActive(false);
        
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
                }else if(editarClicked == true)
                {
                    panelEditarFuente.SetActive(true);
                    currentPanel = Panel.panelEditarFuente;
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

            case Panel.panelEditarFuente:

                
                 panelFuentesDatos.SetActive(true);
                 currentPanel = Panel.panelFuentesDatos;
               

                break;

        }
    }
    public void OnFlechaClick()
    {
        flechaClicked = true;
        OnClickNavegador();
        flechaClicked = false;
    }
    public void OnEditarClick(GameObject parent)
    {

        TextMeshProUGUI nombre = parent.GetComponentInChildren<TextMeshProUGUI>();
        editarClicked = true;
        OnClickNavegador();
        editarFuente.MostrarDatos(nombre.text);
        editarClicked = false;
    }





}
