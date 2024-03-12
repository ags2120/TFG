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
    public GameObject[] panels;
    public Panel currentPanel;

    public GameObject flecha;
    private bool flechaClicked = false;

   /* private void Start()
    {
        panels = new GameObject[3];
        panels[0] = panelMenuInicial;
        panels[1] = panelFuentesDatos;
        panels[2] = panelNuevaFuente;
    }*/
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
    public void DesactivarInteractividadOtrosPaneles()
    {
        foreach (GameObject panel in panels)
        {
            
                DesactivarInteractividadPanel(panel);
            
        }
    }
    private void DesactivarInteractividadPanel(GameObject panel)
    {
        GraphicRaycaster raycaster = panel.GetComponent<GraphicRaycaster>();
        if (raycaster != null)
        {
            raycaster.enabled = false;
        }
    }

    // Método para restaurar la interactividad de un panel
    private void RestaurarInteractividadPanel(GameObject panel)
    {
        GraphicRaycaster raycaster = panel.GetComponent<GraphicRaycaster>();
        if (raycaster != null)
        {
            raycaster.enabled = true;
        }
    }
    public void RestaurarInteractividadPaneles()
    {
        foreach (GameObject panel in panels)
        {
            RestaurarInteractividadPanel(panel);
        }
    }
}
