using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int FeatherCount { get; private set; } = 0;
    private HUDmanager hud;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindAndAssignHUD(); // Intentamos encontrar el HUD inicial
    }

    public void AddFeather()
    {
        FeatherCount++;
        Debug.Log("Pluma añadida. Total: " + FeatherCount);
        hud?.UpdateHUD(); // Actualiza si el HUD está disponible
    }

    public void FindAndAssignHUD()
    {
        hud = FindObjectOfType<HUDmanager>();
        if (hud == null)
        {
            Debug.LogWarning("HUDmanager no encontrado en la escena actual.");
        }
    }
}
