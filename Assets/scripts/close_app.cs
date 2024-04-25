using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_app : MonoBehaviour
{
    // Start is called before the first frame update
    public void CloseApplication()
    {
        // Comprobamos si estamos en el editor de Unity para evitar cerrarlo accidentalmente
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
