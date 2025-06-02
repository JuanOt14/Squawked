using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas

public class ValidarObjetoMS2 : MonoBehaviour
{
    [Header("Nombres de objetos correctos seg�n el audio")]
    public string objetoCorrectoAudio1;
    public string objetoCorrectoAudio2;
    public string objetoCorrectoAudio3;

    [Header("Referencias de animaci�n")]
    public CityPeople.CityPeople cityPeople; // Asigna el CityPeople desde el Inspector

    public GameObject imagenExito;
    public GameObject imagenError;
    public GameObject confeti;
    public ActConvMS2 conversacion; // Asigna el script desde el Inspector

    public bool objetoEntregadoCorrecto = false;

    [Header("Transición de escena")]
    public string siguienteEscena = "NombreDeLaSiguienteEscena"; // Escoge la escena a la que quieres pasar
    public float tiempoEsperaCambioEscena = 5f; // Tiempo de espera antes del cambio

    private void Start()
    {
        if (imagenExito != null) imagenExito.SetActive(false);
        if (imagenError != null) imagenError.SetActive(false);
        if (confeti != null) confeti.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!conversacion.conversacionTerminada)
        {
            Debug.Log("No puedes entregar objetos hasta terminar la conversaci�n.");
            return;
        }

        if (other.CompareTag("Objeto"))
        {
            // Validar el objeto seg�n el audio seleccionado
            if (conversacion.tipoAudioSeleccionado.HasValue)
            {
                bool objetoCorrectoSegunAudio = false;
                switch (conversacion.tipoAudioSeleccionado.Value)
                {
                    case ActConvMS2.AudioTipo.Audio1:
                        objetoCorrectoSegunAudio = (other.gameObject.name == objetoCorrectoAudio1);
                        break;
                    case ActConvMS2.AudioTipo.Audio2:
                        objetoCorrectoSegunAudio = (other.gameObject.name == objetoCorrectoAudio2);
                        break;
                    case ActConvMS2.AudioTipo.Audio3:
                        objetoCorrectoSegunAudio = (other.gameObject.name == objetoCorrectoAudio3);
                        break;
                }

                if (objetoCorrectoSegunAudio)
                {
                    objetoEntregadoCorrecto = true;
                    Debug.Log("�Objeto correcto entregado para el audio!");
                    // Llama a la animaci�n de festejo
                    if (cityPeople != null)
                    {
                        cityPeople.PlayCelebrationAnimation();
                    }
                    StartCoroutine(MostrarExito());
                }
                else
                {
                    Debug.Log("Objeto incorrecto entregado para el audio.");
                    // Llama a la animaci�n de error
                    if (cityPeople != null)
                    {
                        cityPeople.PlayErrorAnimation();
                    }
                    StartCoroutine(MostrarError());
                }
            }
            else
            {
                Debug.LogWarning("No se ha seleccionado ning�n audio.");
                StartCoroutine(MostrarError());
            }
            Destroy(other.gameObject);
        }
    }

    private IEnumerator MostrarExito()
    {
        if (imagenExito != null) imagenExito.SetActive(true);

        // Llama al ConfetiManager para mostrar el confeti durante 5 segundos
        ConfetiManager.Instance?.MostrarConfeti(5f);

        GameManager.Instance.AddFeather();

        yield return new WaitForSeconds(tiempoEsperaCambioEscena);

        if (imagenExito != null) imagenExito.SetActive(false);

        // Cambia de escena después de la espera
        if (!string.IsNullOrEmpty(siguienteEscena))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(siguienteEscena);
        }
    }

    private IEnumerator MostrarError()
    {
        if (imagenError != null) imagenError.SetActive(true);

        yield return new WaitForSeconds(5f);

        if (imagenError != null) imagenError.SetActive(false);
    }
}