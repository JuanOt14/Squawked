using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CogerObjeto : MonoBehaviour
{
    public GameObject handPoint;
    private GameObject pickedObject = null;
    private ReproduccionObjeto sonidoManager;
    private Animator animator;

    void Start()
    {
        // Obtiene los componentes necesarios
        sonidoManager = GetComponent<ReproduccionObjeto>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo para recoger/soltar
        {
            if (pickedObject != null)
            {
                // SOLO permitir soltar si NO estamos en la escena "Mision3"
                if (SceneManager.GetActiveScene().name != "Mision3")
                {
                    Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
                    rb.useGravity = true;
                    rb.isKinematic = false;
                    pickedObject.transform.SetParent(null);
                    pickedObject = null;

                    // Reproducir sonido de soltar
                    if (sonidoManager != null)
                    {
                        sonidoManager.PlayPickUpSound();
                    }
                }
                else
                {
                    Debug.Log("No se puede soltar el objeto en la misión 3.");
                }
            }
            else // Si no hay objeto en la mano, intentar recoger
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 1f); // Detecta objetos cercanos
                foreach (Collider other in colliders)
                {
                    if (other.CompareTag("Objeto") || other.CompareTag("Flor"))
                    {
                        Rigidbody rb = other.GetComponent<Rigidbody>();
                        rb.useGravity = false;
                        rb.isKinematic = true;
                        other.transform.position = handPoint.transform.position;
                        other.transform.SetParent(handPoint.transform);
                        pickedObject = other.gameObject;

                        // Reproducir animación de recogida
                        if (animator != null)
                        {
                            animator.SetTrigger("isPicking");
                        }

                        // Reproducir sonido de recoger
                        if (sonidoManager != null)
                        {
                            sonidoManager.PlayPickUpSound();
                        }
                        Debug.Log("Objeto recogido: " + other.name);
                        // Si el nombre del objeto es exactamente "gafasrojas", activar el seguimiento
                        if (other.name == "gafasrojas")
                        {
                            Debug.Log("Objeto recogido: gafasrojas → intentando activar seguimiento.");

                            GameObject girl = GameObject.Find("girl_NPC");
                            if (girl != null)
                            {
                                SeguirAlPato seguidor = girl.GetComponent<SeguirAlPato>();
                                if (seguidor != null)
                                {
                                    Debug.Log("Script 'SeguirAlPato' encontrado. Ejecutando ActivarSeguimiento().");
                                    seguidor.ActivarSeguimiento();
                                }
                                else
                                {
                                    Debug.LogWarning("No se encontró el componente SeguirAlPato en girl.");
                                }
                            }
                            else
                            {
                                Debug.LogWarning("No se encontró el objeto 'girl' en la jerarquía.");
                            }
                        }


                        break; // Detener la búsqueda después de recoger un objeto
                    }
                }
            }
        }
    }
}
