using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calcularFecha : MonoBehaviour
{
    //private string fecha;
   // private string hora;
    public string time_end;
    public string time_start;
    public string hora;
    private DateTime currentDate;
    private DateTime restDate;
    private GestionarInputs valorMinutos;
    int estado = 0; // 0 - No he atrasado 1 día // 1- He atrasado 1 día // 2- Vuelvo atrás
    // Start is called before the first frame update
    void Start()
    {
        valorMinutos = FindObjectOfType<GestionarInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        currentDate = DateTime.Now;
        
        
        calcularTimeEnd();
        calcularTimeStart();
        
        
    }
    private void calcularTimeEnd()
    {
        string fecha = currentDate.ToString("yyyy-MM-dd");
        hora = currentDate.ToString("HH:mm:ss");
        time_end = fecha + "T" + hora + "Z";
    }
    private void calcularTimeStart()
    {

        float mins = valorMinutos.antiguedad;
        DateTime fechaRestada = currentDate.AddMinutes(-mins);
        string fechaRes = fechaRestada.ToString("yyyy-MM-dd");
        string horaRes = fechaRestada.ToString("HH:mm:ss");
        time_start = fechaRes + "T" + horaRes + "Z";
        
    }
}
