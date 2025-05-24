using System;
using System.Collections;
using UnityEngine;

public class EfectoCuack : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    public AudioClip cuackSound;

    public static event Action OnQuack; // Evento global de graznido

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PlayCuack();
        }
    }

    public void PlayCuack()
    {
        if (cuackSound != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(cuackSound);
            OnQuack?.Invoke();
        }

        if (animator != null)
        {
            animator.SetTrigger("isPecking");
        }
    }
}
