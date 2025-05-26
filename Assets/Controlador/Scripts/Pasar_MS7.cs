using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pasar_MS7 : MonoBehaviour
{
    public string nombreEscenario = "01 - Mision04";
    public ValidarObjetoMS1 validador; // Asigna en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato"))
        {
            if (validador != null && validador.objetoEntregadoCorrecto)
            {
                GameManager.Instance.AddFeather();
                Debug.Log("Jugador interactuó con la puerta. Cambiando al escenario: " + nombreEscenario);
                SceneManager.LoadScene(nombreEscenario);
            }
            else
            {
                Debug.Log("No puedes pasar. Debes entregar el objeto correcto primero.");
            }
        }
    }
}
