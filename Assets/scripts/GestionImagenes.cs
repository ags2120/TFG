using UnityEngine;
using System.Collections.Generic;

public class GestionImagenes : MonoBehaviour
{
    [System.Serializable]
    public class ImagenesAsociadas
    {
        public int numero;           //0--> Agua  1--> Electricidad  2--> Vehiculos
        public Texture2D imagen;
    }

    public List<ImagenesAsociadas> imagenesLista;
}

