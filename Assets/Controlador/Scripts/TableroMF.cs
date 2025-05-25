using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;


public class TableroMF : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Botón izquierdo del mouse
        {
            SceneManager.LoadScene("MFTablero");
        }
    }
}
