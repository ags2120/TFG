using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Panel_Info_Datos : MonoBehaviour
{
    public GameObject panelInfo,panelPadre,panelFlechas;
    private List<GameObject> paneles = new List<GameObject>();
    public int indiceActual = 0;
    private Mensaje msj;
    public GameObject flechaIzquierda, flechaDerecha;
    // Start is called before the first frame update
    void Start()
    {
        //string path = "MLU00150001";
        panelPadre.SetActive(false);
        panelInfo.SetActive(false);
        msj = FindObjectOfType<Mensaje>();
        //string folderPath = Path.Combine(Application.persistentDataPath, path);
       // Create_Instance_Panel("MLU00150001");
        
        //MostrarPanelActual();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Create_Instance_Panel(string uid_prefab)
    {
        string file = Application.persistentDataPath + "/fuentes_datos_separadas/json_" + uid_prefab + ".json";
        string jsonContent = File.ReadAllText(file);
        JObject jsonObject = JObject.Parse(jsonContent);
        // Obtener el array "data"
        JArray valuesArray = (JArray)jsonObject["data"];

        if (valuesArray.Count > 0)
        {
            foreach(JArray datos in valuesArray)
            {
                string description_origin = datos[10].ToString();
                string time = datos[0].ToString();
                string uid = datos[1].ToString();
                string alias = datos[5].ToString();
                float valor = (float)datos[2];
                string metric = datos[3].ToString();
                string description = datos[9].ToString();
                string organizationID = datos[12].ToString();
                modificarPanel(description_origin,time,uid, alias, valor, metric,description,organizationID);
            }

            

        }
        else
        {
            Debug.Log("no entro a intanciar el panel");
        }
    }
    private void modificarPanel(string description_origin,string time, string uid, string alias,float valor, string metric, string description, string organizationID)
    {
        //flechaIzquierda.SetActive (false);
       // flechaDerecha.SetActive (false);
        GameObject nuevoPanel = Instantiate(panelInfo, panelPadre.GetComponent<RectTransform>());
        nuevoPanel.SetActive(true);
        TextMeshProUGUI[] textosTMP = nuevoPanel.GetComponentsInChildren<TextMeshProUGUI>();

        textosTMP[0].text = description_origin;
        textosTMP[2].text = uid;
        textosTMP[4].text = alias;
        textosTMP[5].text = "Consumo: "+valor.ToString() + metric;
        textosTMP[7].text = time;
        textosTMP[8].text = description;
        textosTMP[9].text = organizationID;

        paneles.Add(nuevoPanel);
        nuevoPanel.SetActive (false);
    }
    public void MostrarPanelActual()
    {
        panelFlechas.SetActive (true);
        if (indiceActual == 0)
        {
            Debug.Log("entro a borrar flecha izquierda");
            flechaIzquierda.SetActive(false);

        }
        else
            flechaIzquierda.SetActive(true);

        if (indiceActual == paneles.Count - 1)
        {
            Debug.Log("entro a borrar flecha derecha");
            flechaDerecha.SetActive(false);
        }
        else
        {
            flechaDerecha.SetActive(true);
        }
            

        msj.Desactivar_Activar_InteraccionPanel(false);
        panelPadre.SetActive(true);
        for (int i = 0; i < paneles.Count; i++)
        {
            if (i == indiceActual)
            {
                paneles[i].SetActive(true);
            }
            else
            {
                paneles[i].SetActive(false);
            }
        }
        
    }
    public void IrAlSiguientePanel()
    {
        // Incrementar el índice y mostrar el panel siguiente
        indiceActual = (indiceActual + 1) % paneles.Count;
        MostrarPanelActual();
    }

    public void IrAlPanelAnterior()
    {
        // Decrementar el índice y mostrar el panel anterior
        indiceActual = (indiceActual - 1 + paneles.Count) % paneles.Count;
        MostrarPanelActual();
    }
    public void RemoveListaPanels()
    {
        for (int i = paneles.Count - 1; i >= 0; i--)
        {
            GameObject panel = paneles[i];
            Destroy(panel); // Eliminar el objeto
            paneles.RemoveAt(i); // Eliminar el GameObject de la lista
        }
    }
    public void prueba()
    {
        Create_Instance_Panel("MLU00050001");
        MostrarPanelActual();
    }


}
