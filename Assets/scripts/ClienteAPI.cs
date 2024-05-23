
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//                                                          POST                                                                           este último lo añado yo despues del post
//  0      1      2       3         4           5     6     7      8        9               10              11          12          13         14
//"time","uid","value","metric","typemeter","alias","lat","lon","cota","description","description_origin","name","organizationid","origin","modelo3D"

public class ClienteAPI : MonoBehaviour
{
    private string apiUrl = "https://openapi.smartua.es/tag/";
    private string tokenEnergia = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MDkwMjY1OTd9.B11DGwPPOE_-IaTpv_5jQPQAXBpPv_CbXLFiZB2qkt8";
   // private string tokenAgua = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MDkwMjY2NTR9._RX_9E5UXrhH-ABL8PrT1EE5RiWBU2EgCb7xHPJPwVM";
    private string apiUrlPost = "https://openapi.smartua.es/data/";
    private string flagGET = "/description_origin";
    //private string accestoken = "CkNoj5thi2wGC1qG2PgeJgATnYhtRe;yPKpaB0NMitnE0Z7ghPPsEB9HbXHn7hwu88a_ZAKfm*tDE_*NRCzK9wn0zrWWQ42";
    //private string tokenPruebas = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MDk5MzAzMzl9.xssxAK7sV-BJyRPKPOMS63SKZiZ7LtMQwUo4pDuDkZg";
    private GameObject sliderValue;
    private slide_metros valorMetros;
    private calcularFecha fecha;
    private GestionarInputs inputsMenuPrincipal;
    public TextMeshProUGUI debug;
    //public Slider sliderMinutos;
    private MiGeolocalizacion geo;
    private GuardarFD fuentes;
    private string latitud, longitud;
    private GestionarDatosPost gestionarPost;
    private CrearModelosFD modelos;

    void Start()
    {
        sliderValue = GameObject.Find("controler");
        valorMetros = sliderValue.GetComponent<slide_metros>();
        fecha = FindObjectOfType<calcularFecha>();
        geo = FindObjectOfType<MiGeolocalizacion>();
        fuentes = FindObjectOfType<GuardarFD>();
        gestionarPost = FindObjectOfType<GestionarDatosPost>();
        modelos = GetComponent<CrearModelosFD>();
        inputsMenuPrincipal = FindObjectOfType<GestionarInputs>();



    }
    public void MakeAPIRequestGET()
    {
        StartCoroutine(SendRequestGET());
    }
    public void MakeAPIRequestPOST()
    {
        StartCoroutine(SendRequestPOST());
    }
    public void MakeAPIRequestPOSTPC()
    {
        StartCoroutine(SendRequestPOSTPC());
    }
    IEnumerator SendRequestGET()
    {
        string urlWithToken = apiUrl + tokenEnergia + flagGET;
        Debug.Log(urlWithToken);
        using (UnityWebRequest request = UnityWebRequest.Get(urlWithToken))
        {
            // Agrega el encabezado de autenticación
           

            // Envía la solicitud
            yield return request.SendWebRequest();

            // Maneja la respuesta
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;

                // Reemplaza las comas con comas seguidas de un salto de línea
               // jsonResponse = jsonResponse.Replace("],", "],\n");

                Debug.Log("Response: " + request.downloadHandler.text);
                // Aquí puedes procesar los datos devueltos
                string filePath = Application.persistentDataPath + "/response.json";
                System.IO.File.WriteAllText(filePath, jsonResponse);
                Debug.Log("Response saved to: " + filePath);
            }
        }
    }
   
    IEnumerator SendRequestPOST()
    {

        string valor = (valorMetros.slider.value / 1000).ToString();
        string valorfin = valor.Replace(",", ".");
        latitud = geo.getLatitud().Replace(",", ".");
        longitud = geo.getLongitud().Replace(",", ".");
        //Debug.Log("El valor del Slide es:" + valor);
        WWWForm form = new WWWForm();

        form.AddField("time_start", fecha.time_start);
        form.AddField("time_end", fecha.time_end);
        form.AddField("geo_position[lat]", latitud);
        form.AddField("geo_position[lon]", longitud);
        form.AddField("geo_position[radius_km]", valorfin);
        form.AddField("limit", inputsMenuPrincipal.limite);
        form.AddField("count", "false");
        /*
                form.AddField("geo_position[lat]", "38.38739"); 38.57042 --> mi casa
                form.AddField("geo_position[lon]", "-0.51232"); -0.12439 -->
        */
        for (int i=0; i < fuentes.ListaFuentesDatos.Count; i++)
        {
            if (fuentes.ListaFuentesDatos[i].activo == true)
            {
                Debug.Log("entro " + i);
                string urlWithToken = apiUrlPost + fuentes.ListaFuentesDatos[i].token;
                /* Pruebas
                /"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""/
               // string urlPrueba = apiUrlPost + tokenPruebas;                                            
               // WWWForm formPrueba = new WWWForm();
               // formPrueba.AddField("time_start", fecha.time_start);
               // formPrueba.AddField("time_end", fecha.time_end);
                //formPrueba.AddField("geo_position[lat]", "38.57042");
                //formPrueba.AddField("geo_position[lon]", "-0.12439");
               // formPrueba.AddField("geo_position[radius_km]", valorfin);
               // formPrueba.AddField("limit", "100");
                //formPrueba.AddField("count", "false");


                /***************************************PRUEBAS******************************************/



                
                using (UnityWebRequest request = UnityWebRequest.Post(urlWithToken, form))
                {

                    yield return request.SendWebRequest();

                    // Maneja la respuesta
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError("Error: " + request.error);
                    }
                    else
                    {
                        string jsonResponse = request.downloadHandler.text;

                        // Reemplaza las comas con comas seguidas de un salto de línea
                        jsonResponse = jsonResponse.Replace("],", "],\n");
                       
                        Debug.Log("Response: " + request.downloadHandler.text);
                        // Aquí puedes procesar los datos devueltos
                        string directoryPath = Application.persistentDataPath + "/posts";
                        if (!Directory.Exists(directoryPath))
                        {

                            Directory.CreateDirectory(directoryPath);
                        }
                        string filePath = Application.persistentDataPath + "/posts/post" + fuentes.ListaFuentesDatos[i].nombre + ".json";
                        jsonResponse = anyadirDatoJson(jsonResponse, fuentes.ListaFuentesDatos[i].imagen);
                        System.IO.File.WriteAllText(filePath, jsonResponse);
                        MostrarDatosPost(jsonResponse);
                        
                        Debug.Log("Response saved to: " + filePath);
                    }
                }
            }
            
        }
        //Desglosamos el post en los json distintos para mostrar su información posteriormente
        gestionarPost.ComprobarArchivos();
        modelos.ComprobarJSON();

    }
    public void MostrarDatosPost(string json)
    {
        JObject jsonObject = JObject.Parse(json);
        JArray valuesArray = (JArray)jsonObject["result"]["values"];
        geo.ShowDatosPost(fecha.hora, valuesArray.Count(), valorMetros.slider.value, inputsMenuPrincipal.antiguedad);
    }
    IEnumerator SendRequestPOSTPC()
    {

        string valor = (valorMetros.slider.value / 1000).ToString();
        string valorfin = valor.Replace(",", ".");
        WWWForm form = new WWWForm();

        form.AddField("time_start", fecha.time_start);
        form.AddField("time_end", fecha.time_end);
        form.AddField("geo_position[lat]", "38.57042");
        form.AddField("geo_position[lon]", "-0.12439");
        form.AddField("geo_position[radius_km]", valorfin);
        form.AddField("limit", inputsMenuPrincipal.limite);
        form.AddField("count", "false");


        /*
                form.AddField("geo_position[lat]", "38.38739"); 38.57042 --> mi casa
                form.AddField("geo_position[lon]", "-0.51232"); -0.12439 -->*/
                Debug.Log("time_start: " + fecha.time_start);
                Debug.Log("time_end: " + fecha.time_end);
                Debug.Log("limit: " + inputsMenuPrincipal.limite);
        
        for (int i = 0; i < fuentes.ListaFuentesDatos.Count; i++)
        {
            if (fuentes.ListaFuentesDatos[i].activo == true)
            {
                //Debug.Log("entro " + i);
                string urlWithToken = apiUrlPost + fuentes.ListaFuentesDatos[i].token;

                using (UnityWebRequest request = UnityWebRequest.Post(urlWithToken, form))
                {

                    yield return request.SendWebRequest();

                    // Maneja la respuesta
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError("Error: " + request.error);
                    }
                    else
                    {
                        string jsonResponse = request.downloadHandler.text;

                        // Reemplaza las comas con comas seguidas de un salto de línea
                        jsonResponse = jsonResponse.Replace("],", "],\n");
                        
                        Debug.Log("Response: " + request.downloadHandler.text);
                        // Aquí puedes procesar los datos devueltos
                        string directoryPath = Application.persistentDataPath + "/posts";
                        if (!Directory.Exists(directoryPath))
                        {

                            Directory.CreateDirectory(directoryPath);
                        }
                        string filePath = Application.persistentDataPath + "/posts/post" + fuentes.ListaFuentesDatos[i].nombre + ".json";
                        jsonResponse = anyadirDatoJson(jsonResponse, fuentes.ListaFuentesDatos[i].imagen);
                        System.IO.File.WriteAllText(filePath, jsonResponse);
                        MostrarDatosPost(jsonResponse);

                        Debug.Log("Response saved to: " + filePath);
                    }
                }
            }

        }
        //Desglosamos el post en los json distintos para mostrar su información posteriormente
        gestionarPost.ComprobarArchivos();
        modelos.ComprobarJSON();

    }

    private string anyadirDatoJson(string json, int i)
    {
        JObject jsonObject = JObject.Parse(json);
        /*jsonObject.Add("modelo", 0);*/
       
        if (jsonObject["result"] != null && jsonObject["result"]["columns"] != null)
        {
            // Accedemos a la estructura jsonObject["result"]["values"]
            JArray valuesObject = (JArray)jsonObject["result"]["columns"];

            // Agregamos la nueva propiedad "modelo" con valor 0 dentro de jsonObject["result"]["values"]
            valuesObject.Add("modelo3D");
        }
        if (jsonObject["result"] != null && jsonObject["result"]["values"] != null)
        {
            // Accedemos a la estructura jsonObject["result"]["values"]
            JArray valuesObject = (JArray)jsonObject["result"]["values"];
            foreach (JArray value in valuesObject) {


                value.Add(i);
            }
            
            
        }

        string jsonResult = jsonObject.ToString(); 
        return jsonResult;
    }
}

