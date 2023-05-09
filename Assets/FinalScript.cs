using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScript : MonoBehaviour
{
    // public Scripts effectuse;

   public void Play()
   {
       SceneManager.LoadScene(1);
   }
   public void Home()
   {
       SceneManager.LoadScene(0);
   }
   public void Restart()
   {
       SceneManager.LoadScene(1);               
   }
    public void QuitApp(){
        Application.Quit();
    }

   


}
