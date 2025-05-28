using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas

public class ValidarObjetoMS2 : MonoBehaviour
{
    [Header("Nombres de objetos correctos según el audio")]
    public string objetoCorrectoAudio1;
    public string objetoCorrectoAudio2;
    public string objetoCorrectoAudio3;

    public GameObject imagenExito;
    public GameObject imagenError;
    public GameObject confeti;
    public ActConvMS2 conversacion; // Asigna el script desde el Inspector

    public bool objetoEntregadoCorrecto = false;

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
            Debug.Log("No puedes entregar objetos hasta terminar la conversación.");
            return;
        }

        if (other.CompareTag("Objeto"))
        {
            // Validar el objeto según el audio seleccionado
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
                    Debug.Log("¡Objeto correcto entregado para el audio!");
                    StartCoroutine(MostrarExito());
                }
                else
                {
                    Debug.Log("Objeto incorrecto entregado para el audio.");
                    StartCoroutine(MostrarError());
                }
            }
            else
            {
                Debug.LogWarning("No se ha seleccionado ningún audio.");
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

        yield return new WaitForSeconds(5f);

        if (imagenExito != null) imagenExito.SetActive(false);
    }

    private IEnumerator MostrarError()
    {
        if (imagenError != null) imagenError.SetActive(true);

        yield return new WaitForSeconds(5f);

        if (imagenError != null) imagenError.SetActive(false);
    }
}