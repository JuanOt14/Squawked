using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarConversacion : MonoBehaviour
{
    public AudioClip[] conversaciones; // Array de clips de audio para las conversaciones
    public GameObject npc1; // Referencia al primer NPC
    public GameObject npc2; // Referencia al segundo NPC
    public float rangoActivacion = 3f; // Distancia máxima para activar la conversación
    public int maxReproducciones = 3; // Número máximo de reproducciones permitidas

    private int reproduccionesActuales = 0; // Contador de reproducciones realizadas
    private bool audioEnProgreso = false; // Bandera para verificar si el audio está en progreso

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !audioEnProgreso && reproduccionesActuales < maxReproducciones)
        {
            Debug.Log("Clic izquierdo detectado.");

            float distanciaNpc1 = Vector3.Distance(transform.position, npc1.transform.position);
            float distanciaNpc2 = Vector3.Distance(transform.position, npc2.transform.position);

            Debug.Log($"Distancia al NPC1: {distanciaNpc1}, Distancia al NPC2: {distanciaNpc2}");

            if (distanciaNpc1 <= rangoActivacion && distanciaNpc2 <= rangoActivacion)
            {
                Debug.Log("El pato está lo suficientemente cerca de los NPCs.");
                IniciarConversacion();
            }
            else
            {
                Debug.Log("El pato no está lo suficientemente cerca de los NPCs.");
            }
        }
    }

    void IniciarConversacion()
    {
        // Verifica que ambos NPCs tengan un AudioSource
        AudioSource audioSourceNpc1 = npc1.GetComponent<AudioSource>();
        AudioSource audioSourceNpc2 = npc2.GetComponent<AudioSource>();

        if (conversaciones.Length > 0 && audioSourceNpc1 != null && audioSourceNpc2 != null)
        {
            Debug.Log("Iniciando conversación entre los NPCs.");

            // Selecciona un clip de audio aleatorio
            AudioClip audioSeleccionado = conversaciones[Random.Range(0, conversaciones.Length)];

            // Reproduce el audio desde el NPC1
            audioSourceNpc1.clip = audioSeleccionado;
            audioSourceNpc1.Play();

            audioEnProgreso = true;
            reproduccionesActuales++;

            // Espera a que termine el audio antes de permitir otra reproducción
            StartCoroutine(EsperarFinDeAudio(audioSourceNpc1));
        }
        else
        {
            Debug.LogWarning("No hay clips de audio asignados o falta un AudioSource en uno de los NPCs.");
        }
    }

    IEnumerator EsperarFinDeAudio(AudioSource audioSource)
    {
        // Espera la duración del audio antes de permitir otra reproducción
        yield return new WaitForSeconds(audioSource.clip.length);
        audioEnProgreso = false;
        Debug.Log("La conversación ha terminado.");
    }
}
