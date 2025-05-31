using UnityEngine;

public class OutlineController : MonoBehaviour
{
    public Outline outline; // Referencia al componente Quick Outline

    void Start()
    {
        if (outline != null)
            outline.enabled = false;  // Borde desactivado al inicio
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato") )
        {
            if (outline != null)
                outline.enabled = true;  // Activar borde
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pato") )
        {
            if (outline != null)
                outline.enabled = false;  // Desactivar borde
        }
    }

}