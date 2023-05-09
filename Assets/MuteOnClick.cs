using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuteOnClick : MonoBehaviour
{
    public AudioSource audio;
    public GameObject menu;
    public GameObject game;
   public void mute()
   {
       audio.mute = true;
    
    }
     public void play()
   {
       audio.mute = false;
    
    }
    public void Restart()
    {
        
        
        SceneManager.LoadScene("SampleScene");
        menu.SetActive(false);

    }
}

