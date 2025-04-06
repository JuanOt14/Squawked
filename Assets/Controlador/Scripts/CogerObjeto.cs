using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerObjeto : MonoBehaviour
{
    public GameObject handPoint;
    private GameObject pickedObject = null;
    private ReproduccionObjeto sonidoManager;

    void Start()
    {
        // Busca el script ReproduccionObjeto en el mismo GameObject
        sonidoManager = GetComponent<ReproduccionObjeto>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo para recoger/soltar
        {
            if (pickedObject != null) // Si ya hay un objeto en la mano, soltarlo
            {
                Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;
                pickedObject.transform.SetParent(null);
                pickedObject = null;

                // Llamar a la función que reproduce el sonido al soltar
                if (sonidoManager != null)
                {
                    sonidoManager.PlayPickUpSound();
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

                        // Llamar a la función que reproduce el sonido al recoger
                        if (sonidoManager != null)
                        {
                            sonidoManager.PlayPickUpSound();
                        }
                        break; // Detener la búsqueda después de recoger un objeto
                    }
                }
            }
        }
    }
}
