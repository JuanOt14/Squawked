using System.Collections;
using UnityEngine;

public class ValidarObjeto : MonoBehaviour
{
    public string objetoCorrecto = "key2"; // Nombre del objeto correcto
    public GameObject ventanaFelicidadesPrefab; // Prefab de la ventana de felicitaciones
    public GameObject ventanaErrorPrefab; // Prefab de la ventana de error
    public float duracionVentana = 7f; // Duración de las ventanas emergentes

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra al trigger es el correcto
        if (other.CompareTag("Objeto"))
        {
            if (other.gameObject.name == objetoCorrecto)
            {
                Debug.Log("Objeto correcto entregado.");
                MostrarVentana(ventanaFelicidadesPrefab);
            }
            else
            {
                Debug.Log("Objeto incorrecto entregado.");
                MostrarVentana(ventanaErrorPrefab);
            }

            // Destruye el objeto entregado
            Destroy(other.gameObject);
        }
    }

    private void MostrarVentana(GameObject ventanaPrefab)
    {
        // Instancia el prefab de la ventana
        GameObject ventana = Instantiate(ventanaPrefab, transform.position, Quaternion.identity);

        // Asegúrate de que la ventana esté en el Canvas si es necesario
        if (ventana.GetComponent<Canvas>() != null)
        {
            ventana.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }

        // Inicia la corrutina para desactivar la ventana después de un tiempo
        StartCoroutine(DesactivarVentana(ventana));
    }

    private IEnumerator DesactivarVentana(GameObject ventana)
    {
        yield return new WaitForSeconds(duracionVentana);
        Destroy(ventana); // Destruye la ventana después de la duración
    }
}