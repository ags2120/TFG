using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearPanelesFD : MonoBehaviour
{
    // Panel fuente que deseas duplicar
    public GameObject panelFuente,panelPadre;

    public void craerPanel()
    {
        // Verificar que el panel fuente no sea nulo
        if (panelFuente != null)
        {
            Debug.Log("************************************");
            // Duplicar el panel fuente
            GameObject nuevoPanel = Instantiate(panelFuente,panelPadre.GetComponent<RectTransform>());

            // Establecer el nuevo panel como hijo de alg�n otro GameObject si es necesario
            //nuevoPanel.transform.SetParent(transform);

            // Ajustar la posici�n y cualquier otra configuraci�n si es necesario
            nuevoPanel.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
        else
        {
            Debug.LogError("El panel fuente no est� asignado en el inspector.");
        }
    }
}
