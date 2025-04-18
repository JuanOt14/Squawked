using System.Collections;
using UnityEngine;

public class ControlConversacion : MonoBehaviour
{
    public AudioClip[] dialogos; // Array de clips de audio para la conversación
    public GameObject panelPrincipal; // Panel con los botones "Repetir" y "Elegir Opción"
    public GameObject panelOpciones; // Panel con las opciones "Pato" y "Ganso"
    public GameObject panelResultado; // Panel con el mensaje de resultado
    public AudioSource audioSource; // Componente AudioSource para reproducir los audios

    private bool conversacionTerminada = false;

    private void Start()
    {
        panelPrincipal.SetActive(false);
        panelOpciones.SetActive(false);
        panelResultado.SetActive(false);
    }

    public void IniciarConversacion()
    {
        if (!conversacionTerminada)
        {
            StartCoroutine(ReproducirConversacion());
        }
    }
    
    private IEnumerator ReproducirConversacion()
    {
        foreach (AudioClip dialogo in dialogos)
        {
            audioSource.clip = dialogo;
            audioSource.Play();
            yield return new WaitForSeconds(dialogo.length);
        }

        conversacionTerminada = true;
        panelPrincipal.SetActive(true); // Activa el panel principal al terminar la conversación
    }

    public void RepetirConversacion()
    {
        panelPrincipal.SetActive(false); // Desactiva temporalmente el panel principal
        StartCoroutine(ReproducirConversacion());
    }

    public void MostrarOpciones()
    {
        panelPrincipal.SetActive(false); // Oculta el panel principal
        panelOpciones.SetActive(true); // Muestra el panel de opciones
    }
}