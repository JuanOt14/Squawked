using UnityEngine;

public class Palabra : MonoBehaviour
{
    public AudioClip errorSound;
    private AudioSource audioSource;
    private Vector3 startPoint;
    private SpriteRenderer sr;
    private Victoria victoria;

    private Transform ultimoDestino = null;
    private bool conectadoCorrectamente = false;

    void Start()
    {
        startPoint = transform.position;
        sr = GetComponent<SpriteRenderer>();
        victoria = FindObjectOfType<Victoria>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;
    }

    private void OnMouseUp()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        bool encontroDestino = false;

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                encontroDestino = true;

                bool esCorrecto = transform.name == col.transform.name;

                // Si antes estaba conectado correctamente y se mueve, se resta
                if (conectadoCorrectamente && col.transform != ultimoDestino)
                {
                    victoria.conexionesVictoria--;
                    conectadoCorrectamente = false;
                }

                if (esCorrecto)
                {
                    CambiarColor(true);
                    if (!conectadoCorrectamente)
                    {
                        victoria.conexionesVictoria++;
                        conectadoCorrectamente = true;
                        ultimoDestino = col.transform;
                    }

                    victoria.ComprobarVictoria();
                }
                else
                {
                    CambiarColor(false);
                    ReproducirError();
                    transform.position = startPoint;
                    ultimoDestino = null;
                }

                return;
            }
        }

        // Si no se soltó sobre nada, volver al inicio
        if (!encontroDestino)
        {
            if (conectadoCorrectamente)
            {
                victoria.conexionesVictoria--;
                conectadoCorrectamente = false;
            }

            transform.position = startPoint;
            CambiarColor(Color.white);
            ultimoDestino = null;
        }
    }

    void CambiarColor(Color color)
    {
        if (sr != null)
        {
            sr.color = color;
        }
    }

    void CambiarColor(bool correcto)
    {
        CambiarColor(correcto ? Color.green : Color.red);
    }

    void ReproducirError()
    {
        if (errorSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(errorSound);
        }
    }
}
