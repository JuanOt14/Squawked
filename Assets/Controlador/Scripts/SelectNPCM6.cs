using UnityEngine;

public class NPCSelector : MonoBehaviour
{
    public Outline outline; // Referencia al componente Quick Outline
    private bool isPlayerNearby = false;
    private bool isSelected = false;

    // Evento para avisar que se hizo la selección
    public delegate void OnNPCSelected(NPCSelector selectedNPC);
    public static event OnNPCSelected OnSelectionMade;

    void Start()
    {
        if (outline != null)
            outline.enabled = false;  // Borde desactivado al inicio
    }

    void OnEnable()
    {
        EfectoCuack.OnQuack += HandleQuack;
    }

    void OnDisable()
    {
        EfectoCuack.OnQuack -= HandleQuack;
    }

    void HandleQuack()
    {
        if (isPlayerNearby && !isSelected)
        {
            ConfirmSelection();
        }
    }


    private void ConfirmSelection()
    {
        isSelected = true;
        if (outline != null)
            outline.enabled = false;  // Desactivar borde al confirmar

        // Avisar a otros sistemas que ya se seleccionó este NPC
        OnSelectionMade?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato") && !isSelected)
        {
            isPlayerNearby = true;
            if (outline != null)
                outline.enabled = true;  // Activar borde
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pato") && !isSelected)
        {
            isPlayerNearby = false;
            if (outline != null)
                outline.enabled = false;  // Desactivar borde al alejarse
        }
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}
