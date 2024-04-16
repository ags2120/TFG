//using Google.XR.ARCoreExtensions.GeospatialCreator.Internal;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using Google.XR.ARCoreExtensions;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;
//using Google.XR.ARCoreExtensions;
using Google.XR.ARCoreExtensions.GeospatialCreator.Internal;

public class CrearModelosFD : MonoBehaviour
{
    //******************Cambiar a post normal cuando acabe las pruebas***********************
    string folderName = "post_pruebas";
    public GameObject prefab_Arbol;
    private TocarPrefab tocarPrefab;
    // Start is called before the first frame update
    void Start()
    {
        tocarPrefab = FindObjectOfType<TocarPrefab>();
        ComprobarJSON();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ComprobarJSON()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.json");

            Debug.Log("Número de archivos .json encontrados: " + files.Length);

            foreach (string file in files)
            {
                // Llamar a la función para crear los prefabs
                CrearModelos3D(file);
            }
        }
        else
        {
            Debug.LogWarning("La carpeta " + folderName + " no existe en PersistentDataPath.");
        }
    }
    private void CrearModelos3D(string file)
    {
        string jsonContent = File.ReadAllText(file);
        JObject jsonObject = JObject.Parse(jsonContent);
        // Obtener el array "data"
        JArray valuesArray = (JArray)jsonObject["data"];

        if(valuesArray.Count > 0 )
        {
            JArray primerDato = (JArray)valuesArray[0];

            if(primerDato.Count >= 8 ) { 
                string uid = primerDato[1].ToString();
                tocarPrefab.uid = uid;
                float latitud = (float)primerDato[6];
                float longitud = (float)primerDato[7];
                createInstancePrefab(latitud, longitud);
            }
        }
    }
    private void createInstancePrefab(float latitud, float longitud)
    {
        if(prefab_Arbol != null)
        {
            ARGeospatialCreatorAnchor anchorComponent = prefab_Arbol.GetComponent<ARGeospatialCreatorAnchor>();
            Instantiate(prefab_Arbol);
            if(anchorComponent != null )
            {
                anchorComponent.Latitude = latitud;
                anchorComponent.Longitude = longitud;
            }
        }
    }
}
