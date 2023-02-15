using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInitMenu : MonoBehaviour
{
    private SceneMan sceneManager;
    private void Awake()
    {
        sceneManager = GameObject.Find("SceneMan").GetComponent<SceneMan>();
    }
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void DisablePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
