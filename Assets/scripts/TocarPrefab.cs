using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;

public class TocarPrefab : MonoBehaviour
{
    public string uid;
    public GameObject panelInfoFuentesDatos;
    public RectTransform detectorRaycast;
    private Mensaje mensaje;
    private Panel_Info_Datos info;
    private Desplegable desple;
    
    
    bool touchDetected = false;
    // Start is called before the first frame update
    void Start()
    {
        mensaje = FindObjectOfType<Mensaje>();
        info = FindObjectOfType<Panel_Info_Datos>();
        desple = FindObjectOfType<Desplegable>();   
        //panelInfoFuentesDatos = GameObject.Find("PanelInfoFuenteDatos");
    }

    // Update is called once per frame
     private void Update()
     {

          if (!panelInfoFuentesDatos.activeSelf && desple.menuAbierto)
          {
             if ( !touchDetected && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
             {
                //Si esta dentro del area permitida para el raycast, quitando la parte del menú de arriba
                if(RectTransformUtility.RectangleContainsScreenPoint(detectorRaycast, Input.GetTouch(0).position))
                {
                    touchDetected = true;


                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit[] hits;





                    // Realizar el raycast y obtener todos los objetos que colisionan
                    hits = Physics.RaycastAll(ray);

                    // Variable para almacenar el objeto más cercano
                    GameObject closestObject = null;
                    float closestDistance = Mathf.Infinity;



                    // Iterar sobre todos los objetos que colisionan con el rayo
                    foreach (RaycastHit hit in hits)
                    {

                        // Verificar si el collider pertenece a este objeto
                        if (hit.collider.gameObject == gameObject)
                        {
                            // Calcular la distancia entre el punto de contacto del rayo y el objeto
                            float distance = Vector3.Distance(ray.origin, hit.point);

                            // Verificar si este objeto es el más cercano hasta ahora
                            if (distance < closestDistance)
                            {
                                closestDistance = distance;
                                closestObject = hit.collider.gameObject;
                            }
                        }
                    }

                    // Verificar si se ha encontrado un objeto cercano
                    if (closestObject != null)
                    {
                        // Realizar las acciones que deseas con el objeto más cercano
                        info.Create_Instance_Panel(uid);
                        info.MostrarPanelActual();
                        // Ocultar el prefab
                        // mensaje.EditadoCorrectamente();
                    }
                }
                    

             }
          }


     }

   
    private void LateUpdate()
    {
        touchDetected = false;
        
    }

}
