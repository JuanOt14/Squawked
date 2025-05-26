using System.Collections;
using UnityEngine;

public class ActivarConversacion : MonoBehaviour
{
    public AudioClip[] conversaciones; // 3 audios posibles
    public AudioSource audioSource;    // AudioSource en el BoxCollider (o en un NPC)
    public int maxReproducciones = 3;  // Máximo de oportunidades

    [HideInInspector] public bool conversacionTerminada = false; // Para ValidarObjetoMS1
    private int reproduccionesActuales = 0;
    private bool audioEnProgreso = false;
    private bool jugadorDentro = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato"))
        {
            jugadorDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pato"))
        {
            jugadorDentro = false;
        }
    }

    private void Update()
    {
        if (jugadorDentro && Input.GetMouseButtonDown(1) && !audioEnProgreso && reproduccionesActuales < maxReproducciones)
        {
            IniciarConversacion();
        }
    }

    void IniciarConversacion()
    {
        if (conversaciones.Length > 0 && audioSource != null)
        {
            AudioClip audioSeleccionado = conversaciones[Random.Range(0, conversaciones.Length)];
            audioSource.clip = audioSeleccionado;
            audioSource.Play();

            audioEnProgreso = true;
            reproduccionesActuales++;

            StartCoroutine(EsperarFinDeAudio(audioSeleccionado.length));
        }
        else
        {
            Debug.LogWarning("No hay clips de audio asignados o falta el AudioSource.");
        }
    }

    IEnumerator EsperarFinDeAudio(float duracion)
    {
        yield return new WaitForSeconds(duracion);
        audioEnProgreso = false;
        conversacionTerminada = true;
        Debug.Log("La conversación ha terminado.");
    }
}
