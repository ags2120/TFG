using Google.XR.ARCoreExtensions;
using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using System.Collections;
using System.IO;

public class MiGeolocalizacion : MonoBehaviour
{
    public ARSession arSession;
    public AREarthManager EarthManager;
    public GameObject InfoPanel;
    public Text InfoText, horaAc,elementos,distanciaDetec,tiempo,distanciaRefresco,estadoGeo,tiempoActualizacion,numeroModelos,prueba;
    private IEnumerator _startLocationService = null;
    private bool _waitingForLocationService = false;
    public GeospatialPose pose, poseIni;
    private string latitud,longitud;
    private bool primeraPose = true;
    private slide_metros valorMetros;
    private float distance,metrosRefresco;
    public slides_peticion sliderRefresco;
    private CrearModelosFD prefabs;
    void Start()
    {

        
        valorMetros = FindObjectOfType<slide_metros>();
        prefabs = FindObjectOfType<CrearModelosFD>();

    }
    public void OnEnable()
    {
        _startLocationService = StartLocationService();
        StartCoroutine(_startLocationService);
    }


    void Update()
    {
        
         
        int tiempo = sliderRefresco.tiempoTranscurridoEntero;
        
        
        bool isSessionReady = ARSession.state == ARSessionState.SessionTracking &&
               Input.location.status == LocationServiceStatus.Running;
        var earthTrackingState = EarthManager.EarthTrackingState;
        pose = earthTrackingState == TrackingState.Tracking ?
            EarthManager.CameraGeospatialPose : new GeospatialPose();
        //InfoPanel.SetActive(true);
        
        if (earthTrackingState == TrackingState.Tracking)
        {
            if (primeraPose)
            {
                poseIni = pose;
                primeraPose = false;
            }
            //calcularDistancias();
            //Debug.Log("entro al if");
            InfoText.text = string.Format(
            "GPS INFORMATION:\n\n" +
            "Latitude/Longitude: {1}°, {2}°{0}" +
            "Horizontal Accuracy: {3}m{0}" +
            "Altitude: {4}m{0}" +
            "Vertical Accuracy: {5}m{0}" +
            "Eun Rotation: {6}{0}" +
            "Orientation Yaw Accuracy: {7}°",
            Environment.NewLine,
            pose.Latitude.ToString("F5"),
            pose.Longitude.ToString("F5"),
            pose.HorizontalAccuracy.ToString("F5"),
            pose.Altitude.ToString("F2"),
            pose.VerticalAccuracy.ToString("F2"),
            pose.EunRotation.ToString("F1"),
            pose.OrientationYawAccuracy.ToString("F1"));
            latitud = pose.Latitude.ToString("F5");
            longitud = pose.Longitude.ToString("F5");
            estadoGeo.text = "Posición GeoEspacial cogida correctamente";
            distanciaRefresco.text = "Se han recorrido: " + distance.ToString("F1") + " de " + metrosRefresco + " metros para actualizar";
            tiempoActualizacion.text = "Actualización de datos: " + sliderRefresco.tiempoTranscurridoEntero + " seg. de "+ sliderRefresco.sliderRefresco.value + " mins. para actualizar";

        }
        else
        {
            InfoText.text = "GEOSPATIAL POSE: NOT TRACKING";
            estadoGeo.text = "La Posición GeoEspacial no se está cogiendo";
        }
    }
    public void ShowDatosPost(string hora, int num_elementos, float distancia, float minutos)
    {
        horaAc.text = "Ultima petición: "+hora.ToString() + " (hh/mm/ss)";
        elementos.text = "Elementos Recuperados: "+num_elementos.ToString() + " elementos";
        distanciaDetec.text = "Distancia de Detección: "+distancia.ToString() + " m";
        tiempo.text= "Tiempo de Antigüedad: " + minutos.ToString() + " mins";
        //numeroModelos.text = "Modelos creados actualmente: " + prefabs.InstanciasPrefabs.Count;
       
        
    }
    private IEnumerator StartLocationService()
    {
        _waitingForLocationService = true;
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Debug.Log("Requesting the fine location permission.");
            Permission.RequestUserPermission(Permission.FineLocation);
            yield return new WaitForSeconds(3.0f);
        }
#endif

        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location service is disabled by the user.");
            _waitingForLocationService = false;
            yield break;
        }

        Debug.Log("Starting location service.");
        Input.location.Start();

        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return null;
        }

        _waitingForLocationService = false;
        if (Input.location.status != LocationServiceStatus.Running)
        {
            Debug.LogWarningFormat(
                "Location service ended with {0} status.", Input.location.status);
            Input.location.Stop();
        }
    }
    public string getLatitud()
    {

        return latitud;
    }
    public string getLongitud()
    {

        return longitud;
    }
    
    public bool calcularDistancias()
    {
        bool superada = false;
        // Convertir las coordenadas de latitud y longitud a radianes
        float lat1Rad = (float)poseIni.Latitude * Mathf.Deg2Rad;
        float lon1Rad = (float)poseIni.Longitude * Mathf.Deg2Rad;
        float lat2Rad = (float)pose.Latitude * Mathf.Deg2Rad;
        float lon2Rad = (float)pose.Longitude * Mathf.Deg2Rad;

        /*//PRUEBA
        float lat1Rad = (float)38.57032 * Mathf.Deg2Rad;
        float lon1Rad = (float)-0.12450 * Mathf.Deg2Rad;
        float lat2Rad = (float)38.57085 * Mathf.Deg2Rad;
        float lon2Rad = (float)-0.12388 * Mathf.Deg2Rad;*/
        // Radio de la Tierra en metros
        float earthRadius = 6371000; // metros

        // Calcular la diferencia de latitud y longitud
        float dLat = lat2Rad - lat1Rad;
        float dLon = lon2Rad - lon1Rad;

        // Calcular la distancia utilizando la fórmula del haversine
        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                  Mathf.Cos(lat1Rad) * Mathf.Cos(lat2Rad) *
                  Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        distance = earthRadius * c;

        superada = DistanciaSuperada(distance, superada);
        //Debug.Log("Distancia entre los dos puntos: " + distance.ToString("F2") + " metros");
        return superada;

    }
    public bool DistanciaSuperada(float distanceCalulate, bool superada)
    {
        

        metrosRefresco =  valorMetros.slider.value/10;
        if(metrosRefresco <= distanceCalulate)
        {
            poseIni = pose;
            superada = true;
        }
        return superada;
    }
    
    public void mostrarError(string mensaje)
    {
        prueba.text = mensaje;
    }

}
