using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarConversacion : MonoBehaviour
{
    public AudioSource audioSource; // Componente AudioSource para reproducir el audio
    public AudioClip conversacion1A; // Clip de audio de la conversación entre los NPCs
    public GameObject npc1; // Referencia al primer NPC
    public GameObject npc2; // Referencia al segundo NPC
    public float rangoActivacion = 3f; // Distancia máxima para activar la conversación
    public int maxReproducciones = 3; // Número máximo de reproducciones permitidas

    private int reproduccionesActuales = 0; // Contador de reproducciones realizadas
    private bool audioEnProgreso = false; // Bandera para verificar si el audio está en progreso

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !audioEnProgreso && reproduccionesActuales < maxReproducciones)
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
        if (conversacion1A != null && audioSource != null)
        {
            Debug.Log("Iniciando conversación entre los NPCs.");
            audioSource.clip = conversacion1A;
            audioSource.Play();
            audioEnProgreso = true;
            reproduccionesActuales++;

            StartCoroutine(EsperarFinDeAudio());
        }
        else
        {
            Debug.LogWarning("AudioSource o AudioClip no asignado.");
        }
    }

    IEnumerator EsperarFinDeAudio()
    {
        // Espera la duración del audio antes de permitir otra reproducción
        yield return new WaitForSeconds(audioSource.clip.length);
        audioEnProgreso = false;
        Debug.Log("La conversación ha terminado.");
    }
}
