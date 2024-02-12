using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class navegador : MonoBehaviour
{
   
    public GameObject panelMenuInicial;
    public GameObject panelFuentesDatos;
    public GameObject panelNuevaFuente;
    // Start is called before the first frame update
   
    public void OnClickFuentesDatos() {

        panelMenuInicial.SetActive(false);
        panelFuentesDatos.SetActive(true);
    }
    public void OnClickNuevaFuente()
    {

        panelFuentesDatos.SetActive(false);
        panelNuevaFuente.SetActive(true);
    }
}
