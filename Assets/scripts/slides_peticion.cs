
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slides_peticion : MonoBehaviour, IEndDragHandler
{
    public Slider sliderRefresco;
    private Do_Post hacerPost;
    private bool timerCorriendo = true;
    private float tiempoTranscurrido = 0f;
    public int tiempoTranscurridoEntero = 0;
    // Start is called before the first frame update
    void Start()
    {
        tiempoTranscurridoEntero = 0;
        sliderRefresco = GameObject.Find("Slider_Refresco").GetComponent<Slider>();
        hacerPost = FindObjectOfType<Do_Post>();
        sliderRefresco.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    // Update is called once per frame
    public void OnEndDrag(PointerEventData eventData)
    {
        timerCorriendo = true;
        hacerPost.hacerPost();
    }
    
    void Update()
    {
        if(timerCorriendo && sliderRefresco.value > 0)
        {
            tiempoTranscurrido += Time.deltaTime;
            tiempoTranscurridoEntero = Mathf.FloorToInt(tiempoTranscurrido);
            //Debug.Log(tiempoTranscurridoEntero);
            if (tiempoTranscurridoEntero >= sliderRefresco.value * 60)
            {
                hacerPost.hacerPost();
                tiempoTranscurrido = 0f;
            }
        }
    }
    void OnSliderValueChanged()
    {
        // Reinicia el temporizador y comienza a contar si el slider se mueve
        tiempoTranscurrido = 0f;
        timerCorriendo = false;
    }
    
}
