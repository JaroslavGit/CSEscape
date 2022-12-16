using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene(1);

    }
    public void Options(){
        SceneManager.LoadScene(2);

    }
    public void Sound(){
        SceneManager.LoadScene(3);

    }
}
