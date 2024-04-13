using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desplegable : MonoBehaviour
{
    public RectTransform desplegable;
   // public DragMenu dm;
    float posY;
    //float posinit;
    public bool menuAbierto = true;
    public float tiempo = 0.5f;
    private float posInit;
    public GameObject detector;
    public GameObject boton_menu;
    public ocultar_mostrar_Gameobject ocultar_mostrar;
   
    // Start is called before the first frame update
    void Start()
    {
        //posinit = desplegable.position.y;
        posInit = desplegable.position.x;
        posY = Screen.width / ToSingle(2);
        desplegable.position = new Vector3(-posY, desplegable.position.y, 0);
        ocultar_mostrar = FindObjectOfType<ocultar_mostrar_Gameobject>();
        //detector.SetActive(false);
        

    }

    public static float ToSingle(double value)
    {
        return (float)value;
    }
    IEnumerator Mover (float time,Vector3 posInit,Vector3 PosFin)
    {
       
        float elapsedtime = 0;
        while (elapsedtime < time)
        {
            desplegable.position = Vector3.Lerp(posInit, PosFin, (elapsedtime / time));
            elapsedtime += Time.deltaTime;
            yield return null;
        }
        desplegable.position = PosFin;
    }
    void MoverMenu(float time,Vector3 posInit,Vector3 PosFin)
    {
        
        StartCoroutine(Mover(time, posInit, PosFin));
        
    }
    public void BUTTON_Sub_Menu()
    {
      

        int signo = 1;
        if (!menuAbierto)
        {
            signo = -1;
            MoverMenu(tiempo, desplegable.position, new Vector3(signo * posY, desplegable.position.y, 0));
            ocultar_mostrar.Mostrar_Boton_Menu(true);
            ocultar_mostrar.Mostrar_Panel_Detector(false);
           // boton_menu.SetActive(true);
           // detector.SetActive(false);
        }
        else
        {
            MoverMenu(tiempo, desplegable.position, new Vector3(posInit, desplegable.position.y, 0));
           
            ocultar_mostrar.Mostrar_Boton_Menu(false);
            ocultar_mostrar.Mostrar_Panel_Detector(true);

            //boton_menu.SetActive(false);
            //detector.SetActive(true);
        }
            

        menuAbierto = !menuAbierto;
    }
    
}
