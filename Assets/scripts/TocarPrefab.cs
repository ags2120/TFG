using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TocarPrefab : MonoBehaviour
{

    private Mensaje mensaje;
    // Start is called before the first frame update
    void Start()
    {
        mensaje = FindObjectOfType<Mensaje>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Verificar si se ha tocado la pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Verificar si el toque ha comenzado
            if (touch.phase == TouchPhase.Began)
            {
                // Convertir la posición del toque a un rayo en el mundo
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Verificar si el rayo intersecta con un collider
                if (Physics.Raycast(ray, out hit))
                {
                    // Verificar si el collider pertenece a este objeto
                    if (hit.collider.gameObject == gameObject)
                    {
                        // Ocultar el prefab
                        mensaje.EditadoCorrectamente();
                    }
                }
            }
        }
    }
}
