using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();        
    
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject defeatPanel;

    void Start()
    {
        keys.Add("up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Run", KeyCode.LeftControl);
        keys.Add("Pause", KeyCode.Escape);
        keys.Add("Jump", KeyCode.Space);
        keys.Add("RightAction", KeyCode.Mouse1);
        keys.Add("LeftAction", KeyCode.Mouse0);
        keys.Add("Control", KeyCode.LeftControl);
    }

    public KeyCode GetAction(string key)
    {
        return keys[key];
    }

    private void Update()
    {
        if (Input.GetKeyDown(GetAction("Pause")))
        {
            StopGame();
        }
    }

    public void StopGame()
    {
        Time.timeScale = 0;
        startPanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);

    }
}
