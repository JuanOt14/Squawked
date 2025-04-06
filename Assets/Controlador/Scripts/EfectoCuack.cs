using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoCuack : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip cuackSound;

    void Start()
    {
        // Intenta obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();

        // Si no lo tiene, agrégalo automáticamente
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Reproduce el sonido cuando se presiona el clic derecho
        if (Input.GetMouseButtonDown(1)) 
        {
            PlayCuackSound();
        }
    }

    public void PlayCuackSound()
    {
        if (cuackSound != null && audioSource != null)
        {
            audioSource.Stop(); 
            audioSource.PlayOneShot(cuackSound);
        }
    }
}
