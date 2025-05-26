using UnityEngine;

public class ConfetiManager : MonoBehaviour
{
    public static ConfetiManager Instance;
    public GameObject confeti; // Asigna el GameObject del confeti (ParticleSystem o UI)

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (confeti != null)
            confeti.SetActive(false);
    }

    public void MostrarConfeti(float duracion = 5f)
    {
        Debug.Log("Intentando mostrar confeti");
        if (confeti != null)
        {
            confeti.SetActive(true);
            var ps = confeti.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Debug.Log("ParticleSystem encontrado, reproduciendo confeti");
                ps.Play();
            }
            else
            {
                Debug.LogWarning("No se encontró ParticleSystem en el objeto confeti");
            }
            CancelInvoke(nameof(EsconderConfeti));
            Invoke(nameof(EsconderConfeti), duracion);
        }
        else
        {
            Debug.LogWarning("No se asignó el objeto confeti en el Inspector");
        }
    }

    public void EsconderConfeti()
    {
        if (confeti != null)
        {
            var ps = confeti.GetComponent<ParticleSystem>();
            if (ps != null) ps.Stop();
            confeti.SetActive(false);
        }
    }
}