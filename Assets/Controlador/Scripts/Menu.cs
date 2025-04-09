using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;
//este codigo es para cambiar de escena dentro del juego pasa del meniu al tutorial
// y de la escena del tutorial a la escena de juego 
public class Menu : MonoBehaviour
{
    public void Startutorial(string NombreNivel){
        SceneManager.LoadScene(NombreNivel);
    }
    public void Salir(){
        Application.Quit();
    }
}
//para cambiar de escenas con los botones de unity el script arrastrarlo encima del boton
//y usar preferiblemente la funcion on click