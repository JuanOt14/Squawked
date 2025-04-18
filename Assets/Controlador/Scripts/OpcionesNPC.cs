using UnityEngine;
using UnityEngine.SceneManagement;

public class OpcionesNPC : MonoBehaviour
{
    public GameObject mensajeFelicitaciones; // Mensaje de felicitaciones
    public GameObject mensajeError; // Mensaje de error
    public GameObject botonVolver; // Botón para volver al escenario anterior
    public string escenarioAnterior = "Escenario 5"; // Nombre del escenario anterior
    public bool mision6Desbloqueada = false; // Estado de la misión 6

    private bool respuestaSeleccionada = false;

    private void Start()
    {
        mensajeFelicitaciones.SetActive(false);
        mensajeError.SetActive(false);
        botonVolver.SetActive(false);
    }

    public void SeleccionarRespuesta(bool esCorrecta)
    {
        if (respuestaSeleccionada) return; // Evita seleccionar múltiples respuestas

        respuestaSeleccionada = true;

        if (esCorrecta)
        {
            mensajeFelicitaciones.SetActive(true);
            mision6Desbloqueada = true;
        }
        else
        {
            mensajeError.SetActive(true);
            mision6Desbloqueada = false;
        }

        // Muestra el botón para volver después de 4–5 segundos
        Invoke(nameof(MostrarBotonVolver), 3f);
    }

    private void MostrarBotonVolver()
    {
        botonVolver.SetActive(true);
    }

    public void VolverAlEscenarioAnterior()
    {
        SceneManager.LoadScene(escenarioAnterior);
    }
}