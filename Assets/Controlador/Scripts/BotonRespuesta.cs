using UnityEngine;

public class BotonRespuesta : MonoBehaviour
{
    public ControlOpciones controlOpciones; // Referencia al script ControlOpciones
    public bool esCorrecta; // Define si esta respuesta es correcta

    public void OnClick()
    {
        // Llama al método correspondiente en ControlOpciones
        if (esCorrecta)
        {
            controlOpciones.SeleccionarPato(); // Respuesta correcta
        }
        else
        {
            controlOpciones.SeleccionarGanso(); // Respuesta incorrecta
        }
    }
}