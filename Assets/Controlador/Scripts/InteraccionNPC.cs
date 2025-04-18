using System.Collections;
using UnityEngine;

public class InteraccionNPC : MonoBehaviour
{
    public AudioClip[] dialogos; // Array de clips de audio para la conversación
    public GameObject opcionesUI; // Panel de opciones (Volver a escuchar, Elegir respuesta)
    public GameObject iconoPato; // Ícono sobre el NPC con el pato
    public GameObject iconoGanso; // Ícono sobre el NPC con el ganso

    private AudioSource audioSource;
    private bool interactuable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        opcionesUI.SetActive(false); // Asegúrate de que las opciones estén ocultas al inicio
        iconoPato.SetActive(false);
        iconoGanso.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (interactuable && Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            StartCoroutine(IniciarConversacion());
        }
    }

    private IEnumerator IniciarConversacion()
    {
        interactuable = false;

        // Reproduce los audios de la conversación
        foreach (AudioClip dialogo in dialogos)
        {
            audioSource.clip = dialogo;
            audioSource.Play();
            yield return new WaitForSeconds(dialogo.length);
        }

        // Muestra las opciones después de la conversación
        opcionesUI.SetActive(true);
        iconoPato.SetActive(true);
        iconoGanso.SetActive(true);
    }
}