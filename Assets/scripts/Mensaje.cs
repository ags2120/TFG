using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Mensaje : MonoBehaviour
{
    public GameObject panelMensaje;
    public TextMeshProUGUI textoMensaje;
    private navegador nav;
    private EliminarFuente elimFuent;
    private GameObject parentElim;
    //Mensaje eliminar Fuente
    private string mensajeEliminarFuente = "¿Está seguro de que desea eliminarlo?";

    private string mensajeNombreToken = "El Nombre o Token ya han sido registrados anteriormente, por favor cambielos";

    private string mensajeVacio = "Debe rellenar los campos para continuar";
    public void Start()
    {
        nav = FindObjectOfType<navegador>();
        elimFuent = FindObjectOfType<EliminarFuente>();
    }
    public void mostrarMensaje(string mensaje)
    {
        
        nav.DesactivarInteractividadOtrosPaneles();
        textoMensaje.text = mensaje;
        panelMensaje.SetActive(true);
        
        

    }
    public void onClickConfirmarEliminacion()
    {
        panelMensaje.SetActive(false);
        elimFuent.EliminarFuenteDatos(parentElim);
        nav.RestaurarInteractividadPaneles();
    }
    public void onClickCancelarEliminacion()
    {
        panelMensaje.SetActive(false);
        nav.RestaurarInteractividadPaneles();
    }
    public void EliminarFuente(GameObject parent)
    {
        parentElim = parent;
        mostrarMensaje(mensajeEliminarFuente);
    }
    public void NombreOrTokenRepetido()
    {
        mostrarMensaje(mensajeNombreToken);
    }
    public void CampoVacio()
    {
       mostrarMensaje(mensajeVacio);
    }
}
