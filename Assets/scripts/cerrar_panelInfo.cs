using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cerrar_panelInfo : MonoBehaviour
{
    public GameObject parent;
    private Mensaje msj;
    private ocultar_mostrar_Gameobject ocul;
    private Panel_Info_Datos info;
    // Start is called before the first frame update
    void Start()
    {
        
        Button button = GetComponent<Button>();
        ocul = FindObjectOfType<ocultar_mostrar_Gameobject>();
        msj = FindObjectOfType<Mensaje>();
        info = FindObjectOfType<Panel_Info_Datos>();

        if (button != null)
        {
            // Asigna el método OnClick al evento onClick del botón
            button.onClick.AddListener(OnClick);
        }
    }

    // Update is called once per frame
    public void OnClick()
    {
        msj.Desactivar_Activar_InteraccionPanel(true);
        parent = transform.parent.parent.gameObject;
        parent.SetActive(false);
        //GameObject instance = transform.parent.gameObject;
        ocul.Mostrar_Flechas_panelInfo(false);
        info.RemoveListaPanels();

        
        
    }
}
