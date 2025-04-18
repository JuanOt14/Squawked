using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlOpciones : MonoBehaviour
{
    public GameObject panelPrincipal; // Panel con los botones "Repetir Conversación" y "Elegir Respuesta"
    public GameObject panelOpciones; // Panel con las opciones "Pato" y "Ganso"
    public GameObject panelResultado; // Panel con el mensaje de resultado
    public GameObject botonVolverEscenario; // Botón para volver al escenario anterior
    public UnityEngine.UI.Text txtMensaje; // Texto para mostrar el mensaje de resultado
    public string escenarioAnterior = "Escenario 5"; // Nombre del escenario anterior
    public bool mision6Desbloqueada = false; // Estado de la misión 6

    private void Start()
    {
        // Asegúrate de que todos los paneles estén desactivados al inicio
        panelPrincipal.SetActive(false); // El panel principal está activo al inicio
        panelOpciones.SetActive(false);
        panelResultado.SetActive(false);
        botonVolverEscenario.SetActive(false);
    }

    public void RepetirConversacion()
    {
        // Desactiva temporalmente el panel principal
        panelPrincipal.SetActive(false);

        // Aquí puedes agregar la lógica para repetir la conversación (si tienes un sistema de audio)
        Debug.Log("Reproduciendo conversación nuevamente...");

        // Simula la duración de la conversación antes de reactivar el panel principal
        Invoke(nameof(ActivarPanelPrincipal), 5f); // Ajusta el tiempo según la duración de la conversación
    }

    public void ElegirOpcion()
    {
        // Oculta el panel principal y muestra el panel de opciones
        panelPrincipal.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void SeleccionarPato()
    {
        MostrarResultado("¡Felicidades! Has elegido correctamente.", true);
    }

    public void SeleccionarGanso()
    {
        MostrarResultado("Error. Has elegido incorrectamente.", false);
    }

    private void MostrarResultado(string mensaje, bool esCorrecto)
    {
        panelOpciones.SetActive(false); // Oculta el panel de opciones
        panelResultado.SetActive(true); // Muestra el panel de resultado
        txtMensaje.text = mensaje;

        if (esCorrecto)
        {
            mision6Desbloqueada = true;
        }
        else
        {
            mision6Desbloqueada = false;
        }

        // Activa el botón para volver después de 4–5 segundos
        Invoke(nameof(ActivarBotonVolver), 4f);
    }

    private void ActivarPanelPrincipal()
    {
        panelPrincipal.SetActive(true); // Reactiva el panel principal
    }

    private void ActivarBotonVolver()
    {
        botonVolverEscenario.SetActive(true);
    }

    public void VolverAlEscenarioAnterior()
    {
        SceneManager.LoadScene(escenarioAnterior);
    }
}