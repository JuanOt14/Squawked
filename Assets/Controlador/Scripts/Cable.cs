using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public AudioClip errorSound; // 🔊 Sonido de error cuando la conexión es incorrecta
    private AudioSource audioSource;
    private Vector3 startPoint;
    private Vector3 startPosition;
    private Victoria victoria;
    private bool conectado = false; // Para evitar múltiples conexiones

    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.parent.position;
        victoria = transform.root.gameObject.GetComponent<Victoria>();

        Debug.Log("Posición inicial: " + startPoint); // ✅ Mensaje de depuración para verificar la posición inicial del cable
        Debug.Log("Posición de inicio: " + startPosition); // ✅ Mensaje de depuración para verificar la posición de inicio del cable
        
        // Configurar el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void OnMouseDrag()
    {
        if (conectado) return; // No permitir mover si ya está conectado

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, .2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                UpdateWire(collider.transform.position);

                // ✅ Verifica si el cable se conecta correctamente
                bool esCorrecto = transform.parent.name.Equals(collider.transform.parent.name);

                collider.GetComponent<Cable>()?.Conectar(esCorrecto);
                Conectar(esCorrecto);

                if (esCorrecto)
                {
                    victoria.conexionesVictoria++;
                    victoria.ComprobarVictoria();
                }
                else
                {
                    ReproducirError(); // 🔊 Reproducir sonido si es incorrecto
                }
                return;
            }
        }

        UpdateWire(mousePosition);
    }

    void Conectar(bool esCorrecto)
    {
        conectado = true;
        wireEnd.color = esCorrecto ? Color.green : Color.red; // ✅ Verde si es correcto, ❌ Rojo si es incorrecto
        //Debug.Log(esCorrecto ? "¡Conexión correcta! ✅" : "¡Conexión incorrecta! ❌");
    }

    private void OnMouseUp()
    {
        if (!conectado) 
        {
            UpdateWire(startPosition); // Si no está conectado, vuelve a la posición inicial
        }
    }

    void UpdateWire(Vector3 targetPosition)
    {
        transform.position = (startPoint + targetPosition) / 2f; // Centrar la línea
        Vector2 direction = targetPosition - startPoint; // Calcular la dirección
        
        transform.right = direction.normalized; // Asegurar que la rotación es uniforme

        float dist = direction.magnitude; // Obtener la distancia real
        wireEnd.size = new Vector2(wireEnd.size.y+dist, wireEnd.size.y); // Ajustar tamaño correctamente
    }
    
    void ReproducirError()
    {
        if (errorSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(errorSound);
        }
    }
}
