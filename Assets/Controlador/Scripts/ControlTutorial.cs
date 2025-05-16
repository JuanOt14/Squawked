using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlTutorial : MonoBehaviour
{
    // Nombre del objeto requerido para activar la puerta
    public string requiredObjectName = "Key"; 
    
    public HUDmanager featherHUD;

    private void Start()
    {
        if (featherHUD == null)
        {
            featherHUD = FindObjectOfType<HUDmanager>();

            if (featherHUD == null)
                Debug.LogError("❌ No se encontró HUDmanager en la escena.");
            else
                Debug.Log("✅ HUDmanager asignado automáticamente desde la escena.");
        }
    }

    // Método que se ejecuta cuando otro objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto que entra tiene el tag "Pato"
        if (other.CompareTag("Pato"))
        {
            Debug.Log("Trae el objeto");
        }

        // Si el objeto que entra tiene el tag "Objeto"
        if (other.CompareTag("Objeto"))
        {
            // Verifica si el objeto que entra al trigger es el requerido
            if (other.gameObject.name == requiredObjectName)
            {
                Debug.Log("Si está haciendo lo que necesito");

                // Destruye el objeto requerido
                Destroy(other.gameObject);

                // Actualiza el HUD
                GameManager.Instance.AddFeather();

                // Carga la siguiente escena
                SceneManager.LoadScene("Mision123"); // Cambia al escenario especificado
            }
            else
            {
                // Mensaje de depuración si el objeto no es el correcto
                Debug.Log("Este no es el objeto correcto");
            }
        }
    }
}
