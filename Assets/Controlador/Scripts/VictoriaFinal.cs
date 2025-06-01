using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VictoriaFinal : MonoBehaviour
{
    public List<Casilla> casillas; // Asigna todas las casillas desde el inspector
    public string escenaVictoria;  // Nombre de la escena a cargar si es correcto
    public string escenaError;     // Nombre de la escena a cargar si es incorrecto

    public void VerificarConexiones()
    {
        bool todasCorrectas = true;
        foreach (var casilla in casillas)
        {
            if (!casilla.EsConexionCorrecta())
            {
                todasCorrectas = false;
                break;
            }
        }

        if (todasCorrectas)
        {
            if (!string.IsNullOrEmpty(escenaVictoria))
                SceneManager.LoadScene(escenaVictoria);
        }
        else
        {
            if (!string.IsNullOrEmpty(escenaError))
                SceneManager.LoadScene(escenaError);
        }
    }
}
