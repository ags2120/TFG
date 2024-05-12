using UnityEngine;
using System.Collections.Generic;

public class GestionImagenes : MonoBehaviour
{
    [System.Serializable]
    public class ImagenesAsociadas
    {
        public int numero;           //0--> Agua  1--> Electricidad  2--> Vehiculos  3-->CO2  4-->Consumo  5--> Tráfico  6-->humedad  7--> Viento
        public Texture2D imagen;
    }

    public List<ImagenesAsociadas> imagenesLista;
}

