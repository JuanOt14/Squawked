using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNPC : MonoBehaviour
{
    public bool esNPCCorrecto = false; // Marca si este NPC es el correcto
    private bool jugadorDentro = false;
    private ControlConversacion controlador;

    private void Start()
    {
        controlador = FindObjectOfType<ControlConversacion>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato") && controlador != null && controlador.ConversacionTerminada)
        {
            jugadorDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pato"))
        {
            jugadorDentro = false;
        }
    }

    private void Update()
    {
        if (jugadorDentro && controlador != null && controlador.ConversacionTerminada && !controlador.EleccionRealizada)
        {
            if (Input.GetMouseButtonDown(1))
            {
                controlador.RealizarSeleccion(esNPCCorrecto);
            }
        }
    }
}
