using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndReturn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 originalPosition;
    private bool isDragging = false;
    private bool movimientoBloqueado = false;
    private Vector2 pointerOffset;

    private Casilla casillaActual; // Nueva referencia

    public Casilla CasillaActual => casillaActual; // Propiedad pública de solo lectura

    public void AsignarCasilla(Casilla casilla)
    {
        casillaActual = casilla;
    }

    public void QuitarCasilla()
    {
        casillaActual = null;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;

        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Este objeto debe estar dentro de un Canvas.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (movimientoBloqueado) return;

        isDragging = true;

        // Guardar la posición original solo si no está en una casilla
        if (casillaActual == null)
        {
            originalPosition = rectTransform.anchoredPosition;
        }

        // Si estaba en una casilla, liberarla
        if (casillaActual != null)
        {
            casillaActual.LiberarObjeto();
            QuitarCasilla();
        }

        // Calcular offset entre el punto del cursor y la posición del objeto
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out var localPoint
        );

        pointerOffset = localPoint - rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || canvas == null || movimientoBloqueado) return;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint
        );

        // Actualizar la posición del objeto para que siga al cursor
        rectTransform.anchoredPosition = localPoint - pointerOffset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (movimientoBloqueado) return;

        isDragging = false;

        // Buscar la casilla más cercana (opcional, si quieres snap)
        // Si no, dejar como está:
        // El OnTriggerEnter2D de Casilla se encargará de fijar el objeto

        if (casillaActual == null)
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    public void BloquearMovimiento(bool bloquear)
    {
        movimientoBloqueado = bloquear;
    }
}
