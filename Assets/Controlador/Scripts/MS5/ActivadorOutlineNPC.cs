using UnityEngine;

public class ActivadorOutlineNPC : MonoBehaviour
{
    private Outline outline; // Componente Outline del NPC
    private ControlConversacion controlador; // Referencia al script principal

    private bool conversacionTerminada = false; // Indica si la conversación ha terminado

    public bool ConversacionTerminada
    {
        get { return conversacionTerminada; }
    }

    private void Start()
    {
        // Obtén el componente Outline del NPC
        outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false; // Desactiva el Outline al inicio
        }
        else
        {
            Debug.LogWarning($"El objeto {gameObject.name} no tiene un componente Outline.");
        }

        // Encuentra el controlador principal en la escena
        controlador = FindObjectOfType<ControlConversacion>();
        if (controlador == null)
        {
            Debug.LogError("No se encontró el script ControlConversacion en la escena.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato") && controlador != null && controlador.ConversacionTerminada)
        {
            Debug.Log($"Jugador entró en el rango de {gameObject.name}.");
            if (outline != null)
            {
                outline.enabled = true; // Activa el Outline
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pato") && controlador != null && controlador.ConversacionTerminada)
        {
            Debug.Log($"Jugador salió del rango de {gameObject.name}.");
            if (outline != null)
            {
                outline.enabled = false; // Desactiva el Outline
            }
        }
    }
}