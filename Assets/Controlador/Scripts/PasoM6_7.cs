using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasoM6_7 : MonoBehaviour
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
        // Si el objeto que entra tiene el tag "Pato"
        if (other.CompareTag("Pato"))
        {
            SceneManager.LoadScene("Mfinal");
        }
    }
}
