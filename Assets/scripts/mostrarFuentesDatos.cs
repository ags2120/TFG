using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class mostrarFuentesDatos : MonoBehaviour
{
    public GameObject panelFuente, panelPadre;
    private bool hayFuente = false;
    public TextMeshProUGUI nombreFuente;
    public List<FuentesDatos> ListaFuentesDatos;
    private GuardarFD fuentes;
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/FuentesDatos.json";
        fuentes = FindObjectOfType<GuardarFD>();

        if (fuentes != null)
        {
            //Debug.Log("Entro a he cogido bien GuardarFD.");

            ListaFuentesDatos = fuentes.CargarDatosDesdeJSON(ListaFuentesDatos);

        }
        if (File.Exists(path))
        {
            //Debug.Log("Entro a existe el json.");
            bool primeraFuente = false;
            for (int i = 0; i < ListaFuentesDatos.Count; i++)
            {
                if (!primeraFuente)
                {
                    panelFuente.SetActive(true);
                    nombreFuente.text = ListaFuentesDatos[i].nombre;
                    primeraFuente = true;
                }
                else
                {

                    CrearInstancia(i);
                       
                }
                   
            }

        }
        else
        {
            Debug.Log("No hay fuentes de datos.");
        }
       
    }
    public void CrearInstancia(int i)
    {
        ListaFuentesDatos = fuentes.CargarDatosDesdeJSON(ListaFuentesDatos);
        GameObject nuevoPanel = Instantiate(panelFuente, panelPadre.GetComponent<RectTransform>());
        if (nuevoPanel != null)
            nombreFuente = nuevoPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (i <= ListaFuentesDatos.Count)
        {
            nombreFuente.text = ListaFuentesDatos[i].nombre;
            
        }
            

    }
    
   
}
