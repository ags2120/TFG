using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desplegable : MonoBehaviour
{
    public RectTransform desplegable;
    float posY;
    //float posinit;
    bool menuAbierto = true;
    public float tiempo = 0.5f;
   
    // Start is called before the first frame update
    void Start()
    {
        //posinit = desplegable.position.y;
        posY = Screen.height / ToSingle(3);
        desplegable.position = new Vector3(desplegable.position.x, -posY, 0);
        //Debug.Log("Entro a abrir desplegable");
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
    public static float ToSingle(double value)
    {
        return (float)value;
    }
    IEnumerator Mover (float time,Vector3 posInit,Vector3 PosFin)
    {
       // Debug.Log("Entro a abrir desplegable");
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
        //Debug.Log("Entro a la funcion");
        StartCoroutine(Mover(time, posInit, PosFin));
        
    }
    public void BUTTON_Sub_Menu()
    {
       // Debug.Log("Entro a abrir desplegable");

        int signo = 1;
        if (!menuAbierto)
            signo = -1;

        MoverMenu(tiempo, desplegable.position, new Vector3(desplegable.position.x, signo * posY, 0));
        menuAbierto = !menuAbierto;
    }
}
