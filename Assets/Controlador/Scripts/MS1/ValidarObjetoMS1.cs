using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas



public class ValidarObjetoMS1 : MonoBehaviour
{
    public string objetoCorrecto = "key2";
    public GameObject imagenExito;
    public GameObject imagenError;
    public GameObject confeti;
    public ActivarConversacion conversacion; // Asigna el script desde el Inspector

    public bool objetoEntregadoCorrecto = false; // <-- AGREGA ESTA LÍNEA

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
            if (other.gameObject.name == objetoCorrecto)
            {
                objetoEntregadoCorrecto = true; // <-- ¡IMPORTANTE!
                Debug.Log("¡Objeto correcto entregado!");
                StartCoroutine(MostrarExito());
            }
            else
            {
                Debug.Log("Objeto incorrecto entregado.");
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