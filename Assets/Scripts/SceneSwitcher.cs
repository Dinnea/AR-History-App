using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
   public void EnterAR() 
   {
        SceneManager.LoadScene(1);
   }

    public void ReturnFromAR() 
    { 
    }
}
