
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;




public class ClienteAPI : MonoBehaviour
{
    private string apiUrl = "https://openapi.smartua.es/tag/";
    private string tokenEnergia = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MDkwMjY1OTd9.B11DGwPPOE_-IaTpv_5jQPQAXBpPv_CbXLFiZB2qkt8";
    private string tokenAgua = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MDkwMjY2NTR9._RX_9E5UXrhH-ABL8PrT1EE5RiWBU2EgCb7xHPJPwVM";
    private string apiUrlPost = "https://openapi.smartua.es/data/";
    private string flagGET = "/description_origin";
    private GameObject sliderValue;
    private slide_metros valorMetros;
    public TextMeshProUGUI debug;
    private MiGeolocalizacion geo;
    private string latitud, longitud;


    void Start()
    {
        sliderValue = GameObject.Find("controler");
        valorMetros = sliderValue.GetComponent<slide_metros>();

        geo = FindObjectOfType<MiGeolocalizacion>();
        


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
                jsonResponse = jsonResponse.Replace("],", "],\n");

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
        string urlWithToken = apiUrlPost + tokenAgua;
       
        
            
            //latitud = latitud.Replace(",", ".");
            //result.Item1 = result.Item1.Replace(",", ".");
            // result.Item2 = result.Item2.Replace(",", ".");
            //Debug.Log("String 1: " + result.Item1);
            //Debug.Log("String 2: " + result.Item2);
            //debug.text = latitud+ "" + longitud;
        
       
        WWWForm form = new WWWForm();
        form.AddField("time_start", "2024-02-29T05:18:38Z");
        form.AddField("time_end", "2024-03-01T05:18:38Z");
        form.AddField("geo_position[lat]", latitud);
        form.AddField("geo_position[lon]", longitud);
        form.AddField("geo_position[radius_km]", valorfin);
        form.AddField("limit", "100");
        form.AddField("count", "false");

        /*
        form.AddField("geo_position[lat]", "38.38739");
        form.AddField("geo_position[lon]", "-0.51232");
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

                // Reemplaza las comas con comas seguidas de un salto de línea
                jsonResponse = jsonResponse.Replace("],", "],\n");

                Debug.Log("Response: " + request.downloadHandler.text);
               // Aquí puedes procesar los datos devueltos
                string filePath = Application.persistentDataPath + "/post.json";
                System.IO.File.WriteAllText(filePath, jsonResponse);
                Debug.Log("Response saved to: " + filePath);
            }
        }
    }
}

