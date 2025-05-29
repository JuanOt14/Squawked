using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    public AudioSource[] conversationAudios; // <- arrastra los 3 AudioSources en el Inspector
    private AudioSource selectedAudio;
    public Collider triggerZone;

    private bool conversationStarted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato") && !conversationStarted)
        {
            conversationStarted = true;
            //audioSource.clip = conversationAudio;
            PlayRandomConversation();
        }
    }

    void PlayRandomConversation()
    {
        if (conversationAudios.Length == 0) return;

        int index = Random.Range(0, conversationAudios.Length);
        selectedAudio = conversationAudios[index];

        selectedAudio.Play();
        Debug.Log("Reproduciendo conversación aleatoria #" + index);

        //Invoke(nameof(OnConversationEnded), selectedAudio.clip.length);
    }
}
