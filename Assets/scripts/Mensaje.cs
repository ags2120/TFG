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
    public Button boton_eliminar,boton_cancelar_aceptar,botonAceptar;
    private navegador nav;

    private string azul = "#008BFF",verde = "#3F7F00";
    //Mensaje eliminar Fuente
    private string mensajeEliminarFuente = "¿Está seguro de que desea eliminarlo?";

    private string mensajeNombreToken = "El Nombre o Token ya han sido registrados anteriormente, por favor cambielos";

    private string mensajeVacio = "Debe rellenar los campos para continuar";

    private string guardadoCorrecto = "Fuente de Datos guardada correctamente";

    private string editadoCorrecto = "Fuente Editada Correctamente";

    private string NombreEditadoIncorrecto = "El nombre que desea poner ya pertenece a otra Fuente de Datos";

    private string limiteIncorrecto = "El Límite debe ser igual o mayor a 100";

    private string antiguedadIncorrecta = "El número de minutos debe estar comprendido entre 1 y 1500";
    public void Start()
    {
       
        elimFuent = FindObjectOfType<EliminarFuente>();
        nav = FindObjectOfType<navegador>();
    }
    public void mostrarMensaje(string mensaje)
    {
        
       
        textoMensaje.text = mensaje;
        panelMensaje.SetActive(true);
        Desactivar_Activar_InteraccionPanel(false);
        
        

    }
    public void onClickConfirmarEliminacion()
    {
        
        panelMensaje.SetActive(false);
        elimFuent.EliminarFuenteDatos(parentElim);
        Desactivar_Activar_InteraccionPanel(true);
        

    }
    public void onClickCancelarEliminacion()
    {
        panelMensaje.SetActive(false);
        Desactivar_Activar_InteraccionPanel(true);
        nav.navegateToFuentesDatos();

    }
    public void onClickAceptar()
    {
        panelMensaje.SetActive(false);
        Desactivar_Activar_InteraccionPanel(true);
    }
    public void EliminarFuente(GameObject parent)
    {
        parentElim = parent;
        mostrarMensaje(mensajeEliminarFuente);
        boton_eliminar.gameObject.SetActive(true);
        botonAceptar.gameObject.SetActive(false);
        setImageAndButton(azul, "Cancelar", 0);
    }
    public void NombreOrTokenRepetido()
    {
        mostrarMensaje(mensajeNombreToken);
        boton_eliminar.gameObject.SetActive(false);
        botonAceptar.gameObject.SetActive(true);
        boton_cancelar_aceptar.gameObject.SetActive(false);
        setImageAndButton(azul,"Aceptar",0);
    }
    public void CampoVacio()
    {
       mostrarMensaje(mensajeVacio);
       boton_eliminar.gameObject.SetActive(false);
       botonAceptar.gameObject.SetActive(true);
       boton_cancelar_aceptar.gameObject.SetActive(false);
        //setImageAndButton(azul, "Aceptar", 0);
    }
    public void GuardadoCorrectamente()
    {
        mostrarMensaje(guardadoCorrecto);
        boton_eliminar.gameObject.SetActive(false);
        botonAceptar.gameObject.SetActive(false);
        setImageAndButton(verde, "Aceptar", 1);
    }
    public void EditadoCorrectamente()
    {
        mostrarMensaje(editadoCorrecto);
        boton_eliminar.gameObject.SetActive(false);
        botonAceptar.gameObject.SetActive(false);
        setImageAndButton(verde, "Aceptar", 1);
    }
    public void LimiteInvalido()
    {
        mostrarMensaje(limiteIncorrecto);
        boton_eliminar.gameObject.SetActive(false);
        boton_cancelar_aceptar.gameObject.SetActive(false);
        setImageAndButton(azul, "Aceptar", 0);
    }
    public void AntiguedadInvalida()
    {
        mostrarMensaje(antiguedadIncorrecta);
        boton_eliminar.gameObject.SetActive(false);
        boton_cancelar_aceptar.gameObject.SetActive(false);
        setImageAndButton(azul, "Aceptar", 0);
    }
    public void EditadoIncorrecto()
    {
        mostrarMensaje(NombreEditadoIncorrecto);
        boton_eliminar.gameObject.SetActive(false);
        botonAceptar.gameObject.SetActive(true);
        boton_cancelar_aceptar.gameObject.SetActive(false );
        setImageAndButton(azul, "Aceptar", 0);
    }
   
    // Función para desactivar la interactividad del panel y sus hijos
    public void Desactivar_Activar_InteraccionPanel(bool activar)
    {
        GraphicRaycaster raycaster = panelADesactivar.GetComponent<GraphicRaycaster>();
        if(raycaster.enabled && !activar)
            raycaster.enabled = false;
        else if(!raycaster.enabled && activar)
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
