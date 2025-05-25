using UnityEngine;
using UnityEngine.SceneManagement;

public class Mision6Controller : MonoBehaviour
{
    public AudioSource conversationAudio;
    public GameObject door;  // Referencia a la puerta
    private bool conversationFinished = false;

    void OnEnable()
    {
        NPCSelector.OnSelectionMade += HandleNPCSelection;
    }

    void OnDisable()
    {
        NPCSelector.OnSelectionMade -= HandleNPCSelection;
    }

    void Start()
    {
        if (door != null)
            door.SetActive(false);  // Puerta bloqueada al inicio (desactivada)
        PlayConversation();
    }

    void PlayConversation()
    {
        if (conversationAudio != null)
        {
            //conversationAudio.Play();
            Invoke(nameof(OnConversationEnded), conversationAudio.clip.length);
        }
    }

    void OnConversationEnded()
    {
        conversationFinished = true;
        Debug.Log("Conversación terminada. El pato puede seleccionar.");
    }

    void HandleNPCSelection(NPCSelector selectedNPC)
    {
        if (!conversationFinished) return;  // No aceptar selección antes de que acabe el audio
        conversationFinished = true;
        Debug.Log("NPC seleccionado: " + selectedNPC.name);

        UnlockDoor();
    }

    void UnlockDoor()
    {
        if (door != null)
            door.SetActive(true);  // Activar la puerta para permitir salir

        Debug.Log("Puerta desbloqueada");
    }

    void Update()
    {
        
    }

    void ExitBakery()
    {
        // Aquí cargas la siguiente escena o lo que desees
        SceneManager.LoadScene("NextSceneName");
    }
}
