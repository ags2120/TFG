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
    private EliminarFuente elimFuent;
    private GameObject parentElim;
    public GameObject panelADesactivar;
    public RawImage img_mostrada;
    public Texture[] text_img;
    public Button boton_eliminar,boton_cancelar_aceptar;

    private string azul = "#008BFF",verde = "#3F7F00";
    //Mensaje eliminar Fuente
    private string mensajeEliminarFuente = "¿Está seguro de que desea eliminarlo?";

    private string mensajeNombreToken = "El Nombre o Token ya han sido registrados anteriormente, por favor cambielos";

    private string mensajeVacio = "Debe rellenar los campos para continuar";

    private string guardadoCorrecto = "Fuente de Datos guardada correctamente";
    public void Start()
    {
       
        elimFuent = FindObjectOfType<EliminarFuente>();
    }
    public void mostrarMensaje(string mensaje)
    {
        
       
        textoMensaje.text = mensaje;
        panelMensaje.SetActive(true);
        Desactivar_Activar_InteraccionPanel();
        
        

    }
    public void onClickConfirmarEliminacion()
    {
        
        panelMensaje.SetActive(false);
        elimFuent.EliminarFuenteDatos(parentElim);
        Desactivar_Activar_InteraccionPanel();

    }
    public void onClickCancelarEliminacion()
    {
        panelMensaje.SetActive(false);
        Desactivar_Activar_InteraccionPanel();

    }
    public void EliminarFuente(GameObject parent)
    {
        parentElim = parent;
        mostrarMensaje(mensajeEliminarFuente);
        boton_eliminar.gameObject.SetActive(true);
        setImageAndButton(azul, "Cancelar", 0);
    }
    public void NombreOrTokenRepetido()
    {
        mostrarMensaje(mensajeNombreToken);
        boton_eliminar.gameObject.SetActive(false);
        setImageAndButton(azul,"Aceptar",0);
    }
    public void CampoVacio()
    {
       mostrarMensaje(mensajeVacio);
       boton_eliminar.gameObject.SetActive(false);
       setImageAndButton(azul, "Aceptar", 0);
    }
    public void GuardadoCorrectamente()
    {
        mostrarMensaje(guardadoCorrecto);
        boton_eliminar.gameObject.SetActive(false);
        setImageAndButton(verde, "Aceptar", 1);
    }
   
    // Función para desactivar la interactividad del panel y sus hijos
    public void Desactivar_Activar_InteraccionPanel()
    {
        GraphicRaycaster raycaster = panelADesactivar.GetComponent<GraphicRaycaster>();
        if(raycaster.enabled)
            raycaster.enabled = false;
        else
            raycaster.enabled = true;
    }
    public void setImageAndButton(string color, string texto,int indice_img)
    {
        img_mostrada.texture = text_img[indice_img];
        TextMeshProUGUI texto_boton = boton_cancelar_aceptar.GetComponentInChildren<TextMeshProUGUI>();
        texto_boton.text = texto;
        texto_boton.color = Color.white;
        Color color_boton = HexToColor(color);
        boton_cancelar_aceptar.image.color = color_boton;
    }
    
    Color HexToColor(string hex)
    {
        Color color = Color.black;
        ColorUtility.TryParseHtmlString(hex, out color); // Utilidad de Unity para convertir valores hexadecimales a Color
        return color;
    }

}
