using UnityEngine;
using UnityEngine.UI;

public class HUDmanager : MonoBehaviour
{
    public GameObject featherIconPrefab; // Prefab de la pluma
    public Transform featherContainer;   // Contenedor donde se instancian

    private void Start()
    {
        GameManager.Instance.FindAndAssignHUD();
        UpdateHUD(); // Refresca al entrar en escena
    }


    public void UpdateHUD()
    {
        // Limpia íconos anteriores
        foreach (Transform child in featherContainer)
        {
            Destroy(child.gameObject);
        }

        // Instancia nuevas plumas según el contador actual
        int featherCount = GameManager.Instance.FeatherCount;

        for (int i = 0; i < featherCount; i++)
        {
            Instantiate(featherIconPrefab, featherContainer).SetActive(true);
        }
    }
}