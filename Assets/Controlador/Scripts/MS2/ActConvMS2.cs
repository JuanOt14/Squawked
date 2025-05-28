using System.Collections;
using UnityEngine;

public class ActConvMS2 : MonoBehaviour
{
    public enum AudioTipo { Audio1, Audio2, Audio3 }

    [System.Serializable]
    public struct ConversacionAudio
    {
        public AudioClip clip;
        public AudioTipo tipo;
    }

    public ConversacionAudio[] conversaciones; // 3 audios posibles, asignar en el Inspector
    public AudioSource audioSource;    // AudioSource en el BoxCollider (o en un NPC)
    public int maxReproducciones = 3;  // Máximo de oportunidades

    [HideInInspector] public bool conversacionTerminada = false; // Para ValidarObjetoMS1
    private int reproduccionesActuales = 0;
    private bool audioEnProgreso = false;
    private bool jugadorDentro = false;

    // Exponer el audio y el tipo seleccionado
    [HideInInspector] public AudioClip audioSeleccionado = null;
    [HideInInspector] public AudioTipo? tipoAudioSeleccionado = null;

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
            // Selecciona el audio solo la primera vez
            if (audioSeleccionado == null)
            {
                int idx = Random.Range(0, conversaciones.Length);
                audioSeleccionado = conversaciones[idx].clip;
                tipoAudioSeleccionado = conversaciones[idx].tipo;
            }

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
