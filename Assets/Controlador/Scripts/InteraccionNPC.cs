using System.Collections;
using UnityEngine;

public class InteraccionNPC : MonoBehaviour
{
    public AudioClip[] dialogos; // Array de clips de audio para la conversación
    public AudioSource audioSource; // Componente AudioSource para reproducir los audios
    public GameObject panelPrincipal; // Panel que se activa después de la conversación
    public GameObject iconoEncimaCabeza; // Imagen que aparece encima de la cabeza del NPC
    public GameObject iconoOtroNPC; // Imagen que aparece encima de la cabeza del otro NPC

    private bool interactuable = true;

    private void Update()
    {
        // Detecta el clic derecho del mouse
        if (Input.GetMouseButtonDown(1)) // Clic derecho
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Lanza un rayo desde la posición del mouse
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Verifica si el rayo golpea este NPC
                if (hit.collider.gameObject == gameObject && interactuable)
                {
                    Debug.Log("Interacción con NPC detectada con clic derecho.");
                    StartCoroutine(ReproducirConversacion());
                }
            }
        }
    }

    private IEnumerator ReproducirConversacion()
    {
        interactuable = false;

        // Activa el ícono encima de la cabeza del NPC actual
        if (iconoEncimaCabeza != null)
        {
            iconoEncimaCabeza.SetActive(true);
        }

        // Activa el ícono encima de la cabeza del otro NPC
        if (iconoOtroNPC != null)
        {
            iconoOtroNPC.SetActive(true);
        }

        // Reproduce los audios de la conversación
        foreach (AudioClip dialogo in dialogos)
        {
            audioSource.clip = dialogo;
            audioSource.Play();
            yield return new WaitForSeconds(dialogo.length);
        }

        // Activa el panel principal después de la conversación
        if (panelPrincipal != null)
        {
            panelPrincipal.SetActive(true);
        }

        // Desactiva la interacción para evitar que se repita la conversación
        interactuable = false;
    }
}