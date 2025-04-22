using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas

public class Pasar_MS5 : MonoBehaviour
{
    public string nombreEscenario = "Mision5"; // Nombre del escenario al que se cambiará

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el jugador entra en el área de la puerta
        if (other.CompareTag("Pato")) // Asegúrate de que el jugador tenga el tag "Pato"
        {
            Debug.Log("Jugador interactuó con la puerta. Cambiando al escenario: " + nombreEscenario);
          SceneManager.LoadScene(nombreEscenario); // Cambia al escenario especificado
        }
    }
}

