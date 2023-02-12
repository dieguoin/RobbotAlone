using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    private SceneMan sceneManager;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        sceneManager = GameObject.Find("SceneMan").GetComponent<SceneMan>();
    }
    public void Restart()
    {
        Time.timeScale = 1;
        sceneManager.changeScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        Time.timeScale = 1;
        sceneManager.changeScene("Menu");
    }
    public void Resume()
    {
        gameManager.ResumeGame();
    }
}
