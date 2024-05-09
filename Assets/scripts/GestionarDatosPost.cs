using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.Windows;



[System.Serializable]
public class postData
{
    
    public string code {  get; set; }
    public string msg { get; set; }
    public Result result { get; set; }


    [System.Serializable]
    public class Result
    {
        public string[] columns { get; set; }
        public List<List<object>> values { get; set; }

    }
}
public class GestionarDatosPost : MonoBehaviour
{
    //public string jsonFileName = "post.json";
    public CrearModelosFD modelos;
    private void Start()
    {
        eliminarAntiguos();
        eliminarPosts();
        EliminarModelos();
        //modelos = FindObjectOfType<CrearModelosFD>();

    }
    private void DesglosarJSON(string nombre)
    {
       
        // Obtener la ruta completa del archivo JSON
        string jsonFilePath = Path.Combine(Application.persistentDataPath + "/posts", nombre);

        // Verificar si el archivo existe
        if (System.IO.File.Exists(jsonFilePath))
        {
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            JObject jsonObject = JObject.Parse(jsonContent);

            // Obtener el array "values"
            JArray valuesArray = (JArray)jsonObject["result"]["values"];

            // Crear un diccionario para almacenar los datos agrupados por uid
            Dictionary<string, List<JToken>> groupedData = new Dictionary<string, List<JToken>>();
            // Iterar sobre los valores y agruparlos por uid
            foreach (JToken value in valuesArray)
            {
                string uid = value[1].ToString(); // Obtener el uid

                // Verificar si el uid ya existe en el diccionario
                if (!groupedData.ContainsKey(uid))
                {
                    // Si el uid no existe, crear una nueva lista para ese uid
                    groupedData[uid] = new List<JToken>();
                }

                // Agregar el valor actual a la lista correspondiente al uid
                groupedData[uid].Add(value);
            }

            // Crear un archivo JSON separado para cada grupo de datos
            foreach (var kvp in groupedData)
            {
                string uid = kvp.Key;
                List<JToken> dataList = kvp.Value;

                // Crear un nuevo objeto JSON con los datos agrupados
                JObject groupedJson = new JObject();
                groupedJson["uid"] = uid;
                groupedJson["data"] = new JArray(dataList);

                // Convertir el objeto JSON a una cadena
                string groupedJsonString = groupedJson.ToString();

                // Guardar la cadena JSON en un archivo separado
                string directoryPath = Path.Combine(Application.persistentDataPath, "fuentes_datos_separadas/");
                if (!System.IO.Directory.Exists(directoryPath))
                {

                    System.IO.Directory.CreateDirectory(directoryPath);
                }
                string groupedJsonFilePath = Path.Combine(directoryPath, "json_" + uid + ".json");
                System.IO.File.WriteAllText(groupedJsonFilePath, groupedJsonString);

                Debug.Log("Archivo JSON creado para uid " + uid + " en: " + groupedJsonFilePath);
            }

        }
        else
        {
            Debug.LogError("El archivo JSON no se encontró en: " + jsonFilePath);
        }
    }
    public void ComprobarArchivos()
    {
        string carpeta = Application.persistentDataPath + "/posts";
        // Obtener la lista de archivos en la carpeta
        string[] archivos = System.IO.Directory.GetFiles(carpeta, "*.json");

        // Iterar sobre cada archivo JSON y realizar una acción
        foreach (string archivo in archivos)
        {
            string nombreArchivo = Path.GetFileName(archivo);
            // Realizar la acción con el archivo JSON
            DesglosarJSON(nombreArchivo);
        }
    }
    public void eliminarAntiguos()
    {
        string directorioRuta = Application.persistentDataPath + "/fuentes_datos_separadas";
        DirectoryInfo directorio = new DirectoryInfo(directorioRuta);
        
        if (System.IO.Directory.Exists(directorioRuta))
        {
            foreach (FileInfo archivo in directorio.GetFiles())
            {
                archivo.Delete();
            }
            foreach (DirectoryInfo subDirectorio in directorio.GetDirectories())
            {
                subDirectorio.Delete(true);
            }
        }
    }
    public void eliminarPosts()
    {
        string directorioPost = Application.persistentDataPath + "/posts";
        DirectoryInfo directoryPost = new DirectoryInfo(directorioPost);
        if (System.IO.Directory.Exists(directorioPost))
        {
            foreach (FileInfo archivo in directoryPost.GetFiles())
            {
                archivo.Delete();
            }
            foreach (DirectoryInfo subDirectorio in directoryPost.GetDirectories())
            {
                subDirectorio.Delete(true);
            }
        }
    }
    public void EliminarModelos()
    {
        for (int i = 0; i < modelos.InstanciasPrefabs.Count; i++)
        {
            Destroy(modelos.InstanciasPrefabs[i]); // Destruir cada instancia en la lista
        }

        modelos.InstanciasPrefabs.Clear(); // Limpiar la lista después de destruir todas las instancias
    }

}

