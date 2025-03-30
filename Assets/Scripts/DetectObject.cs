using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class DetectObject : MonoBehaviour
{
    private BoxCollider boxCollider;
    private void OnTriggerEnter(Collider other)
    {
        boxCollider = GetComponent<BoxCollider>();
        if (other.tag == "Pato")
        {
            Debug.Log("Trae la flor");
        }
        if (other.tag == "Flor")
        {
            Debug.Log("Si esta haciendo lo que necesito");

            //boxCollider.isTrigger = false;
            Destroy(gameObject);
            SceneManager.LoadScene("02 - Mision4Tablero");
        }

    }
    
}
