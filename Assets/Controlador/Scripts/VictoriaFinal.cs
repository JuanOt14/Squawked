using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VictoriaFinal : MonoBehaviour
{
    public List<Casilla> casillas; // Asigna todas las casillas desde el inspector
    public VideoPlayer videoPlayer;
    public VideoClip videoVictoria;
    public VideoClip videoError;

    public void VerificarConexiones()
    {
        bool todasCorrectas = true;
        foreach (var casilla in casillas)
        {
            if (!casilla.EsConexionCorrecta())
            {
                todasCorrectas = false;
                break;
            }
        }

        if (todasCorrectas)
        {
            videoPlayer.clip = videoVictoria;
        }
        else
        {
            videoPlayer.clip = videoError;
        }
        videoPlayer.Play();
    }
}
