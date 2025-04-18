using UnityEngine;

public class BotonRespuesta : MonoBehaviour
{
    public OpcionesNPC opcionesNPC; // Referencia al script OpcionesNPC
    public bool esCorrecta; // Define si esta respuesta es correcta

    public void OnClick()
    {
        opcionesNPC.SeleccionarRespuesta(esCorrecta);
    }
}