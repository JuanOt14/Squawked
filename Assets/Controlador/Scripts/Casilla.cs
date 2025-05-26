using UnityEngine;

public class Casilla : MonoBehaviour
{
    private DragAndReturn objetoActual;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DragAndReturn draggable = collision.GetComponent<DragAndReturn>();
        if (draggable != null)
        {
            if (objetoActual == null)
            {
                objetoActual = draggable;
                draggable.AsignarCasilla(this);

                RectTransform rectTransform = draggable.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = ((RectTransform)transform).anchoredPosition;
            }
        }
    }

    public void LiberarObjeto()
    {
        objetoActual = null;
    }

    // Nueva función: ¿el objeto es el correcto?
    public bool EsConexionCorrecta()
    {
        return objetoActual != null && objetoActual.gameObject.name == gameObject.name;
    }
}
