using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePageUIButton : MonoBehaviour
{
   public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
