using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    public AudioClip conversationAudio;
    public AudioSource audioSource;
    public Collider triggerZone;

    private bool conversationStarted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato") && !conversationStarted)
        {
            conversationStarted = true;
            audioSource.clip = conversationAudio;
            audioSource.Play();
        }
    }
}
