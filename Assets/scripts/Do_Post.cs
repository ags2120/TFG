using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Do_Post : MonoBehaviour
{
    // Start is called before the first frame update
    private MiGeolocalizacion geo;
    private CrearModelosFD modelos;
    private ClienteAPI api;
    public Slider mins;
    private GestionarDatosPost gestionar;
    void Start()
    {
        geo = FindObjectOfType<MiGeolocalizacion>();
        api = FindObjectOfType<ClienteAPI>();
        modelos = FindObjectOfType<CrearModelosFD>();
        gestionar = FindObjectOfType<GestionarDatosPost>();
    }

    // Update is called once per frame
    public void Update()
    {
        //Hacemos el post
        if (geo.calcularDistancias() && mins.value!=0)
        {
            hacerPost();
        }
    }
    public void hacerPost()
    {
        gestionar.EliminarModelos();
        gestionar.eliminarPosts();
        gestionar.eliminarAntiguos();
        //api.MakeAPIRequestPOSTPC();
        api.MakeAPIRequestPOST();


    }

}
