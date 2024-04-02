using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EliminarFuente : MonoBehaviour
{
    //private GuardarFD fd;    // Start is called before the first frame update
    // public List<FuentesDatos> ListaFuentesDatos;
    private GuardarFD fd;
    private Mensaje mensaje;
    private GameObject parent;
    private TextMeshProUGUI nombre;
    
    void Start()
    {
        fd = FindObjectOfType<GuardarFD>();
        Button button = GetComponent<Button>();
        mensaje = FindObjectOfType<Mensaje>();
       
        if (button != null)
        {
            // Asigna el método OnClick al evento onClick del botón
            button.onClick.AddListener(OnClick);
        }
    }

    //Solo estoy eliminando la interfaz, pero no estoy eliminando en el json ni en la lista
    void OnClick()
    {
        // Obtén el GameObject padre del botón
        parent = transform.parent.gameObject;
        
        mensaje.EliminarFuente(parent);
        
        
        
    }
    public void EliminarFuenteDatos(GameObject parElim)
    {
        parent = parElim;
        if(parent!=null )
        {
            nombre = parent.GetComponentInChildren<TextMeshProUGUI>();
            fd.EliminarFuentePorNombre(nombre.text);
            
            // Destruye el GameObject padre
            if (parent.name == "fuente1")
                parent.SetActive(false);
            else
                Destroy(parent);

            fd.GenerarJSON();
        }
        else
        {
            Debug.Log("parent  null");
        }
        
        
    }

}
