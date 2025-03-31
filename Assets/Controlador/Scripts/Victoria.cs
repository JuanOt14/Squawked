using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class Victoria : MonoBehaviour
{
    // Start is called before the first frame update
    public int conexionesVictoria;
    
    public void ComprobarVictoria(){
        if(conexionesVictoria == 6){
            Destroy(this.gameObject, 1f);
            SceneManager.LoadScene("00 - Menu");
        }
    }
}
