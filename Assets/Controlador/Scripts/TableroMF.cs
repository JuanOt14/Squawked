using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TableroMF : MonoBehaviour
{
    private bool mouseDentro = false;

    void Update()
    {
        if (mouseDentro && Input.GetMouseButtonDown(1)) // Botón derecho del mouse
        {
            SceneManager.LoadScene("MFTablero");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato")) // Cambia "Player" por el tag adecuado si es necesario
        {
            mouseDentro = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pato")) // Cambia "Player" por el tag adecuado si es necesario
        {
            mouseDentro = false;
        }
    }
}
