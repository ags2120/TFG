
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//                                                          POST
//  0      1      2       3         4           5     6     7      8        9               10              11          12          13
//"time","uid","value","metric","typemeter","alias","lat","lon","cota","description","description_origin","name","organizationid","origin"

public class ClienteAPI : MonoBehaviour
{
    private string apiUrl = "https://openapi.smartua.es/tag/";
    private string tokenEnergia = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MDkwMjY1OTd9.B11DGwPPOE_-IaTpv_5jQPQAXBpPv_CbXLFiZB2qkt8";
    private string tokenAgua = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MDkwMjY2NTR9._RX_9E5UXrhH-ABL8PrT1EE5RiWBU2EgCb7xHPJPwVM";
    private string apiUrlPost = "https://openapi.smartua.es/data/";
    private string flagGET = "/description_origin";
    private string accestoken = "CkNoj5thi2wGC1qG2PgeJgATnYhtRe;yPKpaB0NMitnE0Z7ghPPsEB9HbXHn7hwu88a_ZAKfm*tDE_*NRCzK9wn0zrWWQ42";
    private string tokenPruebas = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MDk5MzAzMzl9.xssxAK7sV-BJyRPKPOMS63SKZiZ7LtMQwUo4pDuDkZg";
    private GameObject sliderValue;
    private slide_metros valorMetros;
    private calcularFecha fecha;
    public TextMeshProUGUI debug;
    public Slider sliderMinutos;
    private MiGeolocalizacion geo;
    private string latitud, longitud;
    //private GestionarPost gestionarPost;


    void Start()
    {
        sliderValue = GameObject.Find("controler");
        valorMetros = sliderValue.GetComponent<slide_metros>();
        fecha = FindObjectOfType<calcularFecha>();
        geo = FindObjectOfType<MiGeolocalizacion>();
       // gestionarPost = FindObjectOfType<GestionarPost>();
        


    }
    public void MakeAPIRequestGET()
    {
        StartCoroutine(SendRequestGET());
    }
    public void MakeAPIRequestPOST()
    {
        StartCoroutine(SendRequestPOST());
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
        //latitud = geo.getLatitud().Replace(",", ".");
        //longitud = geo.getLongitud().Replace(",", ".");
        //Debug.Log("El valor del Slide es:" + valor);
        string urlWithToken = apiUrlPost + tokenEnergia;

        /***************************************PRUEBAS******************************************/
        string urlPrueba = apiUrlPost + tokenPruebas;                                            
        WWWForm formPrueba = new WWWForm();
        formPrueba.AddField("time_start", fecha.time_start);
        formPrueba.AddField("time_end", fecha.time_end);
        //formPrueba.AddField("geo_position[lat]", "38.57042");
        //formPrueba.AddField("geo_position[lon]", "-0.12439");
       // formPrueba.AddField("geo_position[radius_km]", valorfin);
       // formPrueba.AddField("limit", "100");
        //formPrueba.AddField("count", "false");


        /***************************************PRUEBAS******************************************/

        WWWForm form = new WWWForm();
        
        form.AddField("time_start", fecha.time_start);
        form.AddField("time_end", fecha.time_end);
        form.AddField("geo_position[lat]", "38.38739");
        form.AddField("geo_position[lon]", "-0.51232");
        form.AddField("geo_position[radius_km]", valorfin);
        form.AddField("limit", "100");
        form.AddField("count", "false");

        /*
        form.AddField("geo_position[lat]", "38.38739"); 38.57042
        form.AddField("geo_position[lon]", "-0.51232"); -0.12439
         */
        using (UnityWebRequest request = UnityWebRequest.Post(urlWithToken,form))
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
                //gestionarPost.GestionarRespuesta(jsonResponse);
                // Reemplaza las comas con comas seguidas de un salto de línea
                jsonResponse = jsonResponse.Replace("],", "],\n");

                Debug.Log("Response: " + request.downloadHandler.text);
                // Aquí puedes procesar los datos devueltos
                
                string filePath = Application.persistentDataPath + "/post.json";
                System.IO.File.WriteAllText(filePath, jsonResponse);
                MostrarDatosPost(jsonResponse);
                Debug.Log("Response saved to: " + filePath);
            }
        }
    }
    public void MostrarDatosPost(string json)
    {
        JObject jsonObject = JObject.Parse(json);
        JArray valuesArray = (JArray)jsonObject["result"]["values"];
        geo.ShowDatosPost(fecha.hora, valuesArray.Count(), valorMetros.slider.value, sliderMinutos.value);
    }
}

