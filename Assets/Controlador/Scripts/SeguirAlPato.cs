using UnityEngine;
using UnityEngine.AI;

public class SeguirAlPato : MonoBehaviour
{
    private GameObject pato;
    private NavMeshAgent agent;
    private Animator animator;

    private bool debeSeguir = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        pato = GameObject.FindGameObjectWithTag("Pato");

        if (pato == null)
        {
            Debug.LogError("No se encontró un GameObject con el tag 'Pato'. Asegúrate de que el pato tenga ese tag.");
        }
        else
        {
            Debug.Log("Pato encontrado correctamente.");
        }
    }

    void Update()
    {
        if (!debeSeguir)
        {
            return;
        }

        if (pato == null)
        {
            Debug.LogWarning("El pato ya no existe en escena.");
            return;
        }

        // Establece destino constantemente
        agent.SetDestination(pato.transform.position);
        Debug.Log("NPC estableciendo destino hacia el pato.");

        float distancia = Vector3.Distance(transform.position, pato.transform.position);
        bool estaCaminando = distancia > agent.stoppingDistance;

        animator?.SetBool("isWalk", estaCaminando);

        if (estaCaminando)
        {
            Debug.Log("NPC caminando hacia el pato.");
        }
        else
        {
            Debug.Log("NPC está cerca del pato y detiene movimiento.");
            agent.ResetPath();
        }
    }

    public void ActivarSeguimiento()
    {
        Debug.Log("Método ActivarSeguimiento() fue llamado.");
        debeSeguir = true;
    }
}
