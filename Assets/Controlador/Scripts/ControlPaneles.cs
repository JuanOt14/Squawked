using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlPaneles : MonoBehaviour
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
        panelPrincipal.SetActive(false);
        panelOpciones.SetActive(false);
        panelResultado.SetActive(false);
        botonVolverEscenario.SetActive(false);
    }

    public void MostrarPanelPrincipal()
    {
        // Activa el PanelPrincipal
        panelPrincipal.SetActive(true);
        panelOpciones.SetActive(false);
        panelResultado.SetActive(false);
    }

    public void RepetirConversacion()
    {
        // Lógica para repetir la conversación
        Debug.Log("Reproduciendo conversación nuevamente...");
        panelPrincipal.SetActive(false);

        // Simula la duración de la conversación antes de reactivar el panel principal
        Invoke(nameof(MostrarPanelPrincipal), 5f); // Ajusta el tiempo según la duración de la conversación
    }

    public void ElegirOpcion()
    {
        // Oculta el PanelPrincipal y muestra el PanelOpciones
        panelPrincipal.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void SeleccionarPato()
    {
        // Lógica para la respuesta correcta
        MostrarResultado("¡Felicidades! Has elegido correctamente.", true);
    }

    public void SeleccionarGanso()
    {
        // Lógica para la respuesta incorrecta
        MostrarResultado("Error. Has elegido incorrectamente.", false);
    }

    private void MostrarResultado(string mensaje, bool esCorrecto)
    {
        // Muestra el PanelResultado con el mensaje correspondiente
        panelOpciones.SetActive(false);
        panelResultado.SetActive(true);
        txtMensaje.text = mensaje;

        // Actualiza el estado de la misión
        mision6Desbloqueada = esCorrecto;

        // Activa el botón "Volver al Escenario" después de 4 segundos
        Invoke(nameof(ActivarBotonVolver), 4f);
    }

    private void ActivarBotonVolver()
    {
        botonVolverEscenario.SetActive(true);
    }

    public void VolverAlEscenarioAnterior()
    {
        // Carga el escenario anterior
        SceneManager.LoadScene(escenarioAnterior);
    }
}