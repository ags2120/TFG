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

    void Start()
    {
        gameObj = GameObject.Find("controler");
        desple = gameObj.GetComponent<Desplegable>();
        //menuTransform = transform.parent.parent.GetComponent<RectTransform>(); // Obtener el RectTransform del menú desplegable
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //pointerOffset = eventData.position - (Vector2)menuTransform.position; // Calcular el desplazamiento desde el punto de inicio del arrastre hasta el centro del menú
        getTransform();
        initialMousePosition = eventData.position;
        initialMenuPosition = menuTransform.anchoredPosition;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*Vector2 pointerPosition = eventData.position;
        menuTransform.position = pointerPosition - pointerOffset; // Actualizar la posición del menú según el movimiento del ratón o el dedo*/
        float deltaY = eventData.position.y - initialMousePosition.y;
        menuTransform.anchoredPosition = initialMenuPosition + new Vector2(0, deltaY);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
        // Calcula la distancia total movida en el eje Y
        float deltaY = eventData.position.y - initialMousePosition.y;

        // Define una distancia mínima para considerar como "movimiento significativo"
        float minDistance = 600; 

        // Comprueba si la distancia movida es mayor o menor que la distancia mínima
        if (Mathf.Abs(deltaY) > minDistance)
        {
            // Realiza una acción si la distancia movida es mayor que la distancia mínima
            UnityEngine.Debug.Log("Escondo menú.");
            menuTransform.anchoredPosition = initialMenuPosition + new Vector2(0, -2000);
            desple.menuAbierto = true;
            
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
        menuTransform = transform.parent.parent.GetComponent<RectTransform>(); // Obtener el RectTransform del menú desplegable
    }
    
}
