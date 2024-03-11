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
    //Mensaje eliminar Fuente
    private string mensajeEliminarFuente = "¿Está seguro de que desea eliminarlo?";

    private string mensajeNombreToken = "El Nombre o Token ya han sido registrados anteriormente, por favor cambielos";

    private string mensajeVacio = "Debe rellenar los campos para continuar";
    public void mostrarMensaje(string mensaje)
    {
       
        textoMensaje.text = mensaje;
        panelMensaje.SetActive(true);
    }
    public void onClickConfirmarEliminacion()
    {
        //borrar la fuente de datos
    }
    public void onClickCancelarEliminacion()
    {
        panelMensaje.SetActive(false);
    }
    public void EliminarFuente()
    {
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
