using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragMenu : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform menuTransform;
    //private Vector2 pointerOffset;
    private Vector2 initialMousePosition;
    private Vector2 initialMenuPosition;
    
    public Desplegable desple;
    public GameObject gameObj;
    public GameObject flecha;

    public ocultar_mostrar_Gameobject oculultar_mostrar;

    void Start()
    {
        gameObj = GameObject.Find("controler");
        desple = gameObj.GetComponent<Desplegable>();
        oculultar_mostrar = FindObjectOfType<ocultar_mostrar_Gameobject>();
        //menuTransform = transform.parent.parent.GetComponent<RectTransform>(); // Obtener el RectTransform del menú desplegable
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
            getTransform();
            initialMousePosition = eventData.position;
            initialMenuPosition = menuTransform.anchoredPosition;
       
        //pointerOffset = eventData.position - (Vector2)menuTransform.position; // Calcular el desplazamiento desde el punto de inicio del arrastre hasta el centro del menú
        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
       
        float deltaY = eventData.position.x - initialMousePosition.x;
        if (deltaY < 0)                                                                         //Para que no se pueda subir el menú arriba
        {
            menuTransform.anchoredPosition = initialMenuPosition + new Vector2(deltaY, 0);
        }
       
        
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
        // Calcula la distancia total movida en el eje Y
        float deltaY = eventData.position.x - initialMousePosition.x;

        // Define una distancia mínima para considerar como "movimiento significativo"
        float minDistance = 400; 

        // Comprueba si la distancia movida es mayor o menor que la distancia mínima
        if (Mathf.Abs(deltaY) > minDistance)
        {
            // Realiza una acción si la distancia movida es mayor que la distancia mínima
            UnityEngine.Debug.Log("Escondo menú.");
            
            menuTransform.anchoredPosition = initialMenuPosition + new Vector2(-1000, 0);
            desple.menuAbierto = true;
            //boton_menu.SetActive(true);
            //detector.SetActive(false);
            oculultar_mostrar.Mostrar_Panel_Detector(false);
            oculultar_mostrar.Mostrar_Boton_Menu(true);
            oculultar_mostrar.Mostrar_Boton_Salir(true);

        }
        else
        {
            // Realiza otra acción si la distancia movida es menor o igual que la distancia mínima
            UnityEngine.Debug.Log("No escondo menú.");
            menuTransform.anchoredPosition = initialMenuPosition;
            desple.menuAbierto = false;

        }
    }
    public void getTransform()
    {
        menuTransform = transform.GetComponent<RectTransform>(); // Obtener el RectTransform del menú desplegable
    }
    
}
