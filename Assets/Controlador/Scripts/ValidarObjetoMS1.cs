using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas

public class ValidarObjetoConCanvas : MonoBehaviour
{
    public string objetoCorrecto = "key2"; // Nombre del objeto correcto
    public string escenarioCorrecto = "CompletarMS"; // Nombre del escenario para el caso correcto
    public string escenarioIncorrecto = "IncorrectoMS"; // Nombre del escenario para el caso incorrecto
    public string escenarioMision = "Mision123"; // Nombre del escenario actual de la misión
    public float duracionEscenario = 2f; // Duración de la visualización del escenario
    public HUDmanager featherHUD;

    private void Start()
    {
        if (featherHUD == null)
        {
            featherHUD = FindObjectOfType<HUDmanager>();

            if (featherHUD == null)
                Debug.LogError("❌ No se encontró HUDmanager en la escena.");
            else
                Debug.Log("✅ HUDmanager asignado automáticamente desde la escena.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra al trigger es un objeto interactivo
        if (other.CompareTag("Objeto"))
        {
            if (other.gameObject.name == objetoCorrecto)
            {
                Debug.Log("Objeto correcto entregado.");
                FeatherManager.Instance.AddFeather();
                featherHUD.UpdateHUD(); // Refresca el HUD después de sumar la pluma
                StartCoroutine(CambiarEscenario(escenarioCorrecto, other.gameObject)); // Cambia al escenario correcto
            }
            else
            {
                Debug.Log("Objeto incorrecto entregado.");
                StartCoroutine(CambiarEscenario(escenarioIncorrecto, other.gameObject)); // Cambia al escenario incorrecto
            }
        }
    }

    private IEnumerator CambiarEscenario(string nuevoEscenario, GameObject objeto)
    {
        // Destruye el objeto entregado
        Destroy(objeto);

        // Carga el escenario correspondiente
        SceneManager.LoadScene(nuevoEscenario);

        // Espera la duración del escenario
        yield return new WaitForSeconds(duracionEscenario);

        // Regresa al escenario de la misión
        SceneManager.LoadScene(escenarioMision);
    }
}