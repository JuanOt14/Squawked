using UnityEngine;

public class FeatherManager : MonoBehaviour
{
    public static FeatherManager Instance;

    public int FeatherCount { get; private set; } = 0;

    private void Awake()
    {
        // Singleton: asegúrate de que solo haya uno
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

    public void AddFeather()
    {
        FeatherCount++;
        Debug.Log("Pluma añadida. Total: " + FeatherCount);
    }

    public int GetFeatherCount()
    {
        return FeatherCount;
    }
}
