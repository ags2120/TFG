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
    public Text InfoText;
    private IEnumerator _startLocationService = null;
    private bool _waitingForLocationService = false;
    public GeospatialPose pose;
    private string latitud,longitud;

    void Start()
    {
         
        
       
    }
    public void OnEnable()
    {
        _startLocationService = StartLocationService();
        StartCoroutine(_startLocationService);
    }


    void Update()
    {
        bool isSessionReady = ARSession.state == ARSessionState.SessionTracking &&
               Input.location.status == LocationServiceStatus.Running;
        var earthTrackingState = EarthManager.EarthTrackingState;
        pose = earthTrackingState == TrackingState.Tracking ?
            EarthManager.CameraGeospatialPose : new GeospatialPose();
        InfoPanel.SetActive(true);
        if (earthTrackingState == TrackingState.Tracking)
        {
            
            //Debug.Log("entro al if");
            InfoText.text = string.Format(
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
            
        }
        else
        {
            InfoText.text = "GEOSPATIAL POSE: NOT TRACKING";
            
        }
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

}
