using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class GuardarFD : MonoBehaviour
{
    public List<FuentesDatos> ListaFuentesDatos;
    //private listaFuentes lista;
    public TextMeshProUGUI nombreFuente = null,tokenFuente = null;
    public Toggle fuenteActiva;
    public TMP_InputField input_nombre;
    public TMP_InputField input_token;
    private Mensaje mensaje;
    private mostrarFuentesDatos mostrarFD;
    private bool NombreRegistrado = false, TokenRegistrado = false, vacio = false;
    
    void Start()
    {
        CargarDatosDesdeJSON();
        mensaje = FindObjectOfType<Mensaje>();
        mostrarFD = FindObjectOfType<mostrarFuentesDatos>();
    }
    public void GenerarJSON() 
    {
        string path = Application.persistentDataPath + "/FuentesDatos.json";
        string json = JsonHelper.ToJson(ListaFuentesDatos.ToArray(),true);
        File.WriteAllText(path, json);
    } 
    public void GuardarFuente()
    {
        int size = ListaFuentesDatos.Count;
        FuentesDatos nuevaFuente = new FuentesDatos();
        //nuevaFuente.id = nuevoID;
        nuevaFuente.nombre = nombreFuente.text;
        nuevaFuente.token = tokenFuente.text;
        nuevaFuente.activo = fuenteActiva.isOn;
        if (!validarInput())
        {
            ComprobarDatos(nuevaFuente);
            //Debug.Log("entro a comprobar datos");
        }
            
        
        if (vacio)
        {
            //Debug.Log("campo Vacio");
            mensaje.CampoVacio();
            
        }else if (NombreRegistrado || TokenRegistrado)
        {
           // Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            mensaje.NombreOrTokenRepetido();
        }
        else
        {
            ListaFuentesDatos.Add(nuevaFuente);
            GenerarJSON();
            mostrarFD.CrearInstancia(size);
        }
        
    }
    void CargarDatosDesdeJSON()
    {
        string path = Application.persistentDataPath + "/FuentesDatos.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ListaFuentesDatos = JsonHelper.FromJson<FuentesDatos>(json).ToList();
        }
        else
        {
            Debug.LogWarning("El archivo JSON no existe. No se han cargado datos.");
        }
    }
    public List<FuentesDatos> CargarDatosDesdeJSON(List<FuentesDatos> ListaFuentes)
    {
        string path = Application.persistentDataPath + "/FuentesDatos.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ListaFuentes = JsonHelper.FromJson<FuentesDatos>(json).ToList();
        }
        else
        {
            Debug.LogWarning("El archivo JSON no existe. No se han cargado datos.");
        }
        return ListaFuentes;
    }
    void ComprobarDatos(FuentesDatos nuevaFuente)
    {
        bool encontrado = false;
        for (int i = 0;i<ListaFuentesDatos.Count;i++)
        {
            
            if(!encontrado)
            {

                if (nuevaFuente.nombre == ListaFuentesDatos[i].nombre || nuevaFuente.token == ListaFuentesDatos[i].token)
                {
                    NombreRegistrado = true;
                    TokenRegistrado = true;
                    encontrado = true;
                }
                else
                {
                    NombreRegistrado = false;
                    TokenRegistrado = false;
                }
                    
            }
            
        }
        
    }
    bool validarInput()
    {
        string texto = input_nombre.text;
        string texto_token = input_token.text;
        bool inputVacio = false;
        if (string.IsNullOrWhiteSpace(texto) || string.IsNullOrWhiteSpace(texto_token))
        {
            //Debug.Log("vacio TRUE");
            vacio = true;
            inputVacio = true;
        }
        else
        {
            //Debug.Log("Vacio FALSE" );
            //Debug.Log(nuevaFuente.nombre);
            vacio = false;
        }
        return inputVacio;
    }
    public void EliminarFuentePorNombre(string nombre)
    {
        bool encontrado = false;
        for (int i = 0; i < ListaFuentesDatos.Count && !encontrado; i++)
        {
            if (nombre == ListaFuentesDatos[i].nombre)
            {
                ListaFuentesDatos.RemoveAt(i);
                encontrado = true;
            }
        }
        
    }
    

}
