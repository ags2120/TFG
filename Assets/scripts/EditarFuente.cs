using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class EditarFuente : MonoBehaviour
{
    private GuardarFD fuentes;
    private mostrarFuentesDatos mostrarFD;
    private Mensaje msj;
    public TMP_InputField nombreFuente = null, tokenFuente = null;
    private string nombreAntiguo;
    public Toggle toggle;
    public TMP_Dropdown dropdown;
    private int iterator = 0;
    private bool vacio = false;
    // Start is called before the first frame update
    void Start()
    {
        fuentes = FindObjectOfType<GuardarFD>();
        mostrarFD = FindObjectOfType<mostrarFuentesDatos>();
        msj = FindObjectOfType<Mensaje>();  
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MostrarDatos(string nombre)
    {
        
        for (int i = 0; i < fuentes.ListaFuentesDatos.Count; i++)
        {
            
            if (nombre == fuentes.ListaFuentesDatos[i].nombre)
            {
               
                nombreFuente.text = fuentes.ListaFuentesDatos[i].nombre;
                tokenFuente.text = fuentes.ListaFuentesDatos[i].token;
                toggle.isOn = fuentes.ListaFuentesDatos[i].activo;
                dropdown.SetValueWithoutNotify(fuentes.ListaFuentesDatos[i].imagen);
                iterator = i;
                nombreAntiguo = nombreFuente.text;
            }
            
        }
    }
    public void EditarFuenteDatos()
    {
        FuentesDatos nuevaFuente = new FuentesDatos();
        nuevaFuente.nombre = nombreFuente.text;
        nuevaFuente.token = tokenFuente.text;
        nuevaFuente.activo = toggle.isOn;
        nuevaFuente.imagen = dropdown.value;

        /*if (!validarInput())
        {
            fuentes.ComprobarDatos(nuevaFuente);
        }
        if(vacio)
        {
            //Debug.Log("campo Vacio");
            msj.CampoVacio();

        }
        else if (fuentes.NombreRegistrado || fuentes.TokenRegistrado)
        {

            msj.NombreOrTokenRepetido();
        }
        else
        {*/
        if (!validarInput())
        {
            if (comprobarDatos(nuevaFuente))
            {
                if (iterator >= 0 && iterator < fuentes.ListaFuentesDatos.Count)
                {
                    
                    fuentes.ListaFuentesDatos[iterator].nombre = nuevaFuente.nombre;
                    fuentes.ListaFuentesDatos[iterator].token = nuevaFuente.token;
                    fuentes.ListaFuentesDatos[iterator].activo = nuevaFuente.activo;
                    fuentes.ListaFuentesDatos[iterator].imagen = nuevaFuente.imagen;
                    mostrarFD.FuenteEditada(nombreAntiguo, nombreFuente.text, iterator);
                    fuentes.GenerarJSON();
                    
                    msj.EditadoCorrectamente();

                }
            }
            else
                msj.EditadoIncorrecto();
        }
        else
            msj.CampoVacio();
               
            
          
        

    }
    private bool validarInput()
    {
        string texto = nombreFuente.text;
        string texto_token = tokenFuente.text;
        bool inputVacio = false;
        if (string.IsNullOrWhiteSpace(texto) || string.IsNullOrWhiteSpace(texto_token))
        {
           
            vacio = true;
            inputVacio = true;
        }
        else
        {
            
            vacio = false;
        }
        return inputVacio;
    }
    private bool comprobarDatos(FuentesDatos fuente)
    {
        Debug.Log("iterador: " + iterator);

        if(fuente.nombre == nombreAntiguo)
        {
            Debug.Log("entro a nombre igual al anterior");
           //Esto bien
            return true;
        }
        for (int i = 0; i < fuentes.ListaFuentesDatos.Count; i++)
        {
            if (i != iterator)
            {
                //Debug.Log("******************1*****************");
                Debug.Log(fuentes.ListaFuentesDatos[i].nombre + fuente.nombre);
                if (fuentes.ListaFuentesDatos[i].nombre == fuente.nombre)
                {
                    Debug.Log("return false");
                    return false;
                }
                    
            }
        }
        


        return true;
    }
}
