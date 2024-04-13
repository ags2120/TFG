using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class DatoEditarFuente : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI nombre;
    private GameObject parent;
    private navegador nav;
    void Start()
    {
        
        Button button = GetComponent<Button>();
        nav = FindObjectOfType<navegador>();

        if (button != null)
        {
            // Asigna el método OnClick al evento onClick del botón
            button.onClick.AddListener(OnClick);
        }
    }

    // Update is called once per frame
    private void OnClick()
    {
        parent = transform.parent.gameObject;
        nav.OnEditarClick(parent);
    }
}
