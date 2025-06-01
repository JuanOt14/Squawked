using UnityEngine;

public class Casilla : MonoBehaviour
{
    private DragAndReturn objetoActual;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DragAndReturn draggable = collision.GetComponent<DragAndReturn>();
        if (draggable != null)
        {
            // Si ya hay un objeto, no aceptar otro
            if (objetoActual == null)
            {
                // Si el objeto estaba en otra casilla, liberarla
                if (draggable.CasillaActual != null && draggable.CasillaActual != this)
                {
                    draggable.CasillaActual.LiberarObjeto();
                }

                objetoActual = draggable;
                draggable.AsignarCasilla(this);

                // Snap visual al centro de la casilla
                RectTransform rectTransform = draggable.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = ((RectTransform)transform).anchoredPosition;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DragAndReturn draggable = collision.GetComponent<DragAndReturn>();
        if (draggable != null && objetoActual == draggable)
        {
            LiberarObjeto();
            draggable.QuitarCasilla();
        }
    }

    public void LiberarObjeto()
    {
        objetoActual = null;
    }

    public bool EsConexionCorrecta()
    {
        return objetoActual != null && objetoActual.gameObject.name == gameObject.name;
    }
}
