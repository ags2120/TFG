using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestionarInputs : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField input_limite;
    public TMP_InputField input_minutos;
    private Do_Post post;
    public int limite,antiguedad;
    private Mensaje mensaje;
    void Start()
    {
        mensaje = FindObjectOfType<Mensaje>();
        post = FindObjectOfType<Do_Post>();
        // Asegúrate de que el InputField esté asignado
        if (input_limite != null)
        {
            // Añade el listener al evento onEndEdit del InputField
            input_limite.onEndEdit.AddListener(OnInputFieldEndEdit);
            input_minutos.onEndEdit.AddListener(OnAntiguedadEndEdit);
            int.TryParse(input_limite.text, out limite);
            int.TryParse(input_minutos.text, out antiguedad);
        }
    }

    // Este método se llamará cuando el usuario termine de editar el campo de entrada
    private void OnInputFieldEndEdit(string input)
    {
        int value;
        if (int.TryParse(input, out value) && value >= 100)
        {
            limite = value;
            post.hacerPost();
            
        }
        else
        {
            mensaje.LimiteInvalido();
            input_limite.text = "100";
            int.TryParse(input_limite.text, out limite);
        }
    }
    private void OnAntiguedadEndEdit(string input)
    {
        int value;
        if (int.TryParse(input, out value) && value >= 1 && value <= 1500)
        {
            antiguedad = value;
            post.hacerPost();

        }
        else
        {
            mensaje.AntiguedadInvalida();
            input_minutos.text = "1";
            int.TryParse(input_limite.text, out antiguedad);
        }
    }
}
