using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonEliminar : MonoBehaviour
{
    public GameObject panelConfirmacion;
    
    //public Canvas canvasPrincipal;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarMensaje()
    {
          
        panelConfirmacion.SetActive(true);
    }
    public void onClickConfirmarEliminacion()
    {
        //borrar la fuente de datos
    }
    public void onClickCancelarEliminacion()
    {
        panelConfirmacion.SetActive(false);
    }
}
