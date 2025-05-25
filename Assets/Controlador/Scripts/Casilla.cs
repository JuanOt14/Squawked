using UnityEngine;

public class Casilla : MonoBehaviour
{
    private DragAndReturn objetoActual;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que entra tiene el script DragAndReturn
        DragAndReturn draggable = collision.GetComponent<DragAndReturn>();
        if (draggable != null)
        {
            // Si esta casilla no tiene ya un objeto, lo acepta
            if (objetoActual == null)
            {
                objetoActual = draggable;

                // Asigna esta casilla al objeto
                draggable.AsignarCasilla(this);

                // Posiciona el objeto justo en el centro de la casilla
                RectTransform rectTransform = draggable.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = ((RectTransform)transform).anchoredPosition;
            }
        }
    }

    public void LiberarObjeto()
    {
        objetoActual = null;
    }
}
