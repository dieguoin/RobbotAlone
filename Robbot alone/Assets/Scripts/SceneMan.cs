using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMan : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void changeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
