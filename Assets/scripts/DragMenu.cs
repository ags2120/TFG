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

    void Start()
    {
        gameObj = GameObject.Find("controler");
        desple = gameObj.GetComponent<Desplegable>();
        //menuTransform = transform.parent.parent.GetComponent<RectTransform>(); // Obtener el RectTransform del men� desplegable
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
            getTransform();
            initialMousePosition = eventData.position;
            initialMenuPosition = menuTransform.anchoredPosition;
       
        //pointerOffset = eventData.position - (Vector2)menuTransform.position; // Calcular el desplazamiento desde el punto de inicio del arrastre hasta el centro del men�
        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
       
        float deltaY = eventData.position.y - initialMousePosition.y;
        if (deltaY < 0)                                                                         //Para que no se pueda subir el men� arriba
        {
            menuTransform.anchoredPosition = initialMenuPosition + new Vector2(0, deltaY);
        }
       
        
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
        // Calcula la distancia total movida en el eje Y
        float deltaY = eventData.position.y - initialMousePosition.y;

        // Define una distancia m�nima para considerar como "movimiento significativo"
        float minDistance = 600; 

        // Comprueba si la distancia movida es mayor o menor que la distancia m�nima
        if (Mathf.Abs(deltaY) > minDistance)
        {
            // Realiza una acci�n si la distancia movida es mayor que la distancia m�nima
            UnityEngine.Debug.Log("Escondo men�.");
            float posY = (float)Screen.height / 3;
            menuTransform.anchoredPosition = initialMenuPosition + new Vector2(0, -2000);
            desple.menuAbierto = true;
            
        }
        else
        {
            // Realiza otra acci�n si la distancia movida es menor o igual que la distancia m�nima
            UnityEngine.Debug.Log("No escondo men�.");
            menuTransform.anchoredPosition = initialMenuPosition;
            desple.menuAbierto = false;

        }
    }
    public void getTransform()
    {
        menuTransform = transform.parent.GetComponent<RectTransform>(); // Obtener el RectTransform del men� desplegable
    }
    
}
