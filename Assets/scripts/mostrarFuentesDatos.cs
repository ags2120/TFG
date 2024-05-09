using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class mostrarFuentesDatos : MonoBehaviour
{
    public GestionImagenes gestionImagenes;
    public GameObject panelFuente, panelPadre;
    public List<GameObject> paneles;
    //private bool hayFuente = false;
    public TextMeshProUGUI nombreFuente, activas_totales;
    public RawImage imagen;
    public Button eliminar;
   // private RawImage[] imagenes;
    
    private GuardarFD fuentes;
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/FuentesDatos.json";
        fuentes = FindObjectOfType<GuardarFD>();

        
        if (File.Exists(path))
        {
            
            bool primeraFuente = false;
            for (int i = 0; i < fuentes.ListaFuentesDatos.Count; i++)
            {
                if (!primeraFuente)
                {
                    panelFuente.SetActive(true);
                    nombreFuente.text = fuentes.ListaFuentesDatos[i].nombre;
                    imagen.texture = MostraImagen(fuentes.ListaFuentesDatos[i].imagen);
                    
                    //imagenes = panelFuente.GetComponentsInChildren<RawImage>();
                    mostrarInactivas(i,imagen, nombreFuente);
                    primeraFuente = true;
                    

                }
                else
                {
                    //panelFuente.SetActive(false);
                    CrearInstancia(i);
                       
                }
                

            }

        }
        else
        {
            panelFuente.SetActive(false);
            Debug.Log("No hay fuentes de datos.");
        }
        comprobarActivas();
    }
    public void CrearInstancia(int i)
    {
       
        GameObject nuevoPanel = Instantiate(panelFuente, panelPadre.GetComponent<RectTransform>());
        nuevoPanel.SetActive(true);
        if (nuevoPanel != null)
        {
            nombreFuente = nuevoPanel.GetComponentInChildren<TextMeshProUGUI>();
            imagen = nuevoPanel.GetComponentInChildren<RawImage>();
            eliminar = nuevoPanel.GetComponentInChildren<Button>();
            //imagenes = nuevoPanel.GetComponentsInChildren<RawImage>(); //Guardamos las imágenes porque hay más de una en la misma capa
            //Debug.Log("tamaño array de imágenes : " + imagenes.Length);
        }
            
        if (i <= fuentes.ListaFuentesDatos.Count)
        {
            nombreFuente.text = fuentes.ListaFuentesDatos[i].nombre;
            imagen.texture = MostraImagen(fuentes.ListaFuentesDatos[i].imagen);
            


        }

        comprobarActivas();
        mostrarInactivas(i,imagen, nombreFuente);
        paneles.Add(nuevoPanel);

    }
    public Texture2D MostraImagen(int i)
    {
        foreach (var imagenAsociada in gestionImagenes.imagenesLista)
        {
            if(imagenAsociada.numero == i)
            {
                return imagenAsociada.imagen;
            }
        }
        return null; //Si no encuentra la imagen devuelve null;
    }

    public void comprobarActivas()
    {
        
        int activas = 0;
        for (int i= 0;i<fuentes.ListaFuentesDatos.Count;i++)
        {
            if (fuentes.ListaFuentesDatos[i].activo)
            {
                activas++;
            }
        }
        activas_totales.text = activas.ToString() + "/" + fuentes.ListaFuentesDatos.Count.ToString() + "  Activas/Totales";
    }
    public void mostrarInactivas(int i,RawImage icono,TextMeshProUGUI nombre)
    {
        //Color color_nombre = new Color(nombre.color.r, nombre.color.g, nombre.color.b, 0.5f);

        if (fuentes.ListaFuentesDatos[i].activo == false)
        {

            nombre.color = new Color(nombre.color.r, nombre.color.g, nombre.color.b, 0.4f);
            icono.color = new Color(icono.color.r, icono.color.g, icono.color.b, 0.4f);
            

        }
        else
        {
            nombre.color = Color.white;
            icono.color = new Color(icono.color.r, icono.color.g, icono.color.b, 1f);
           
        }
        
        
    }
    public void FuenteEditada(string nombreAntiguo,string nombreNuevo,int i)
    {
        // imagen.texture = MostraImagen(fuentes.ListaFuentesDatos[i].imagen);
        //nombreFuente.text = nombre;
        foreach (GameObject panel in paneles)
        {
            TextMeshProUGUI textoDelPanel = panel.GetComponentInChildren<TextMeshProUGUI>();
            if (textoDelPanel != null)
            {
                if (textoDelPanel.text == nombreAntiguo)
                {
                    panel.GetComponentInChildren<RawImage>().texture = MostraImagen(fuentes.ListaFuentesDatos[i].imagen);
                    panel.GetComponentInChildren<TextMeshProUGUI>().text = nombreNuevo;
                    mostrarInactivas(i, panel.GetComponentInChildren<RawImage>(), panel.GetComponentInChildren<TextMeshProUGUI>());
                }
            }
        }
        //mostrarInactivas(i, imagen, nombreFuente);
        comprobarActivas();
        
    }
    
}
