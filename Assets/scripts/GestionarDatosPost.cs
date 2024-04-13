using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System;
//using System.Text.Json;
//using System.Text.Json.Nodes;
//using Palmmedia.ReportGenerator.Core.Common;


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
        public string jsonFileName = "post.json";
        private void Start()
        {
            // Obtener la ruta completa del archivo JSON
            string jsonFilePath = Path.Combine(Application.persistentDataPath, jsonFileName);
            
            // Verificar si el archivo existe
            if (File.Exists(jsonFilePath))
            {
            string jsonContent = File.ReadAllText(jsonFilePath);
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
                string directoryPath = Path.Combine(Application.persistentDataPath, "post");
                if (!Directory.Exists(directoryPath))
                {
                    
                    Directory.CreateDirectory(directoryPath);
                }
                string groupedJsonFilePath = Path.Combine(directoryPath, "json_" + uid + ".json");
                File.WriteAllText(groupedJsonFilePath, groupedJsonString);

                Debug.Log("Archivo JSON creado para uid " + uid + " en: " + groupedJsonFilePath);
            }

        }
            else
            {
                Debug.LogError("El archivo JSON no se encontró en: " + jsonFilePath);
            }

        }
    }

