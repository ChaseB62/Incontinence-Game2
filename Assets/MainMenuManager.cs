using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public string levelName;
    public void Play(){
        SceneManager.LoadScene(levelName);
    }

    public void Quit(){
        Application.Quit();
    }
}
