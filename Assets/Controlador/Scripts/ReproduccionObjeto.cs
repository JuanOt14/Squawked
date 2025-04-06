using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproduccionObjeto : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip pickUpSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    public void PlayPickUpSound()
    {
        if (pickUpSound != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(pickUpSound);
        }
    }
}
