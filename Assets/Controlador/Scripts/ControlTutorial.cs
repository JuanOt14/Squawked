using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pato")
        {
            Debug.Log("Trae el objeto");
        }
        if (other.tag == "Objeto")
        {
            Debug.Log("Si esta haciendo lo que necesito");

            Destroy(gameObject);
            SceneManager.LoadScene("02 - Mision4");
        }

    }
}
