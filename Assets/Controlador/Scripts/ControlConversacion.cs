using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControlConversacion : MonoBehaviour
{
    [Header("Configuración de NPCs")]
    public GameObject npc1; // NPC 1
    public GameObject npc2; // NPC 2
    public GameObject iconoNPC1; // Ícono sobre la cabeza del NPC 1
    public GameObject iconoNPC2; // Ícono sobre la cabeza del NPC 2
    public Color colorBordeNPC1 = new Color(1f, 0.5f, 0f); // Naranja
    public Color colorBordeNPC2 = Color.blue; // Azul

    [Header("Configuración de Audio")]
    public AudioClip[] dialogos; // Clips de audio para la conversación
    public AudioSource audioSource; // Componente AudioSource para reproducir los audios
    public AudioClip sonidoAplausos; // Sonido de aplausos para éxito

    [Header("Configuración del Jugador")]
    public Transform jugador; // Transform del jugador (el pato)
    public float distanciaRaycast = 10f; // Distancia máxima del Raycast

    [Header("UI y Efectos Visuales")]
    public GameObject mensajeExito; // Imagen de éxito
    public GameObject mensajeFracaso; // Imagen de fracaso
    public GameObject efectoConfeti; // Efecto visual de confeti

    private bool enZonaDeActivacion = false; // Indica si el jugador está en la zona de activación
    private bool conversacionTerminada = false; // Indica si la conversación ha terminado
    private bool eleccionRealizada = false; // Indica si ya se hizo una elección

    public bool ConversacionTerminada
    {
        get { return conversacionTerminada; }
    }

    private void Update()
    {
        // Detecta clic derecho para iniciar la conversación si está en la zona de activación
        if (Input.GetMouseButtonDown(1) && enZonaDeActivacion && !conversacionTerminada)
        {
            Debug.Log("Clic derecho detectado. Iniciando conversación.");
            StartCoroutine(IniciarConversacion());
        }

        // Detecta clic derecho para elegir un NPC después de la conversación
        if (Input.GetMouseButtonDown(1) && conversacionTerminada && !eleccionRealizada)
        {
            DetectarNPCConRaycast();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato"))
        {
            Debug.Log("Jugador entró en la zona de activación.");
            enZonaDeActivacion = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pato"))
        {
            Debug.Log("Jugador salió de la zona de activación.");
            enZonaDeActivacion = false;
        }
    }

    private IEnumerator IniciarConversacion()
    {
        // Activa los íconos sobre los NPCs
        if (iconoNPC1 != null) iconoNPC1.SetActive(true);
        if (iconoNPC2 != null) iconoNPC2.SetActive(true);

        // Selecciona un clip de audio aleatorio
        if (dialogos.Length > 0)
        {
            AudioClip audioSeleccionado = dialogos[Random.Range(0, dialogos.Length)];
            Debug.Log($"Reproduciendo audio aleatorio: {audioSeleccionado.name}");
            
            // Reproduce el audio seleccionado
            audioSource.clip = audioSeleccionado;
            audioSource.Play();

            // Espera a que termine el audio antes de continuar
            yield return new WaitForSeconds(audioSeleccionado.length);
        }
        else
        {
            Debug.LogWarning("No hay clips de audio asignados en el array 'dialogos'.");
        }

        Debug.Log("Conversación terminada.");
        conversacionTerminada = true;

        // Activa los bordes de los NPCs
        ActivarBorde(npc1, colorBordeNPC1);
        ActivarBorde(npc2, colorBordeNPC2);
    }

    private void DetectarNPCConRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, distanciaRaycast))
        {
            if (hit.collider.gameObject == npc1)
            {
                Debug.Log("NPC 1 seleccionado. Respuesta incorrecta.");
                MostrarResultado(false); // Fracaso
            }
            else if (hit.collider.gameObject == npc2)
            {
                Debug.Log("NPC 2 seleccionado. Respuesta correcta.");
                MostrarResultado(true); // Éxito
            }

            // Desactiva los íconos y la posibilidad de volver a interactuar con los NPCs
            DesactivarInteraccion();
        }
    }

    private void MostrarResultado(bool exito)
    {
        if (exito)
        {
            // Mostrar mensaje de éxito
            if (mensajeExito != null) mensajeExito.SetActive(true);

            // Reproducir sonido de aplausos
            if (sonidoAplausos != null)
            {
                audioSource.clip = sonidoAplausos;
                audioSource.Play();
            }

            // Activar efecto de confeti
            if (efectoConfeti != null) efectoConfeti.SetActive(true);
        }
        else
        {
            // Mostrar mensaje de fracaso
            if (mensajeFracaso != null) mensajeFracaso.SetActive(true);
        }
    }

    private void ActivarBorde(GameObject npc, Color color)
    {
        Renderer renderer = npc.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Cambia el color del material del NPC
            renderer.material.color = color;
            Debug.Log($"Borde activado para {npc.name} con color {color}.");
        }
        else
        {
            Debug.LogWarning($"El objeto {npc.name} no tiene un componente Renderer.");
        }
    }

    private void DesactivarInteraccion()
    {
        // Desactiva los colliders de los NPCs para evitar más interacciones
        if (npc1.TryGetComponent<Collider>(out Collider collider1)) collider1.enabled = false;
        if (npc2.TryGetComponent<Collider>(out Collider collider2)) collider2.enabled = false;

        // Desactiva los íconos sobre los NPCs
        if (iconoNPC1 != null) iconoNPC1.SetActive(false);
        if (iconoNPC2 != null) iconoNPC2.SetActive(false);

        Debug.Log("Interacción desactivada para ambos NPCs.");
    }
}