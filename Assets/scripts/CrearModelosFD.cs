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
    string folderName = "fuentes_datos_separadas";
    public List<GameObject> modelos_base;
    public List<TocarPrefab> tocarPrefab;
    public List<GameObject> InstanciasPrefabs = new List<GameObject>();
    
   
    // Start is called before the first frame update
    void Start()
    {
       //tocarPrefab = FindObjectOfType<TocarPrefab>();
        foreach (GameObject modelo in modelos_base)
        {
            modelo.SetActive(false);
        }
        //uid_mod = GetComponent<uid_modelos>();
          
        //ComprobarJSON();
       
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
            //string[] subfolders = Directory.GetDirectories(folderPath);
            
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
            crearCarpetaFuentesDatosSeparadas(folderPath);
        }
    }
    private void crearCarpetaFuentesDatosSeparadas(string path)
    {
        Directory.CreateDirectory(path);
        ComprobarJSON();
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
                foreach (TocarPrefab script in tocarPrefab)
                {
                    script.uid = uid;
                }
                float latitud = (float)primerDato[6];
                float longitud = (float)primerDato[7];
                int modelo = (int)primerDato[14];
                createInstancePrefab(latitud, longitud, modelo,uid);
            }
            
        }
       
    }
    private void createInstancePrefab(float latitud, float longitud,int modelo,string uid)
    {
        
        if(modelos_base[modelo] != null)
        {

            ARGeospatialCreatorAnchor anchorComponent = modelos_base[modelo].GetComponent<ARGeospatialCreatorAnchor>();
            GameObject nuevaInstancia = Instantiate(modelos_base[modelo]);
            ARGeospatialCreatorAnchor anchorComponentNuevaInstancia = nuevaInstancia.GetComponent<ARGeospatialCreatorAnchor>();
            nuevaInstancia.SetActive(true);
            InstanciasPrefabs.Add(nuevaInstancia);
            if(anchorComponent != null )
            {
                anchorComponentNuevaInstancia.Latitude = latitud;
                anchorComponentNuevaInstancia.Longitude = longitud;
              //tocarPrefab.uid = uid;
            }
        }
        
    }
    
}
