using System.Collections;
using UnityEngine;

public class EfectoCuack : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    public AudioClip cuackSound;

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
        }

        if (animator != null)
        {
            animator.SetTrigger("isPecking");
        }
    }
}
