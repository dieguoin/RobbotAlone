using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMan : MonoBehaviour
{
    // Start is called before the first frame update
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Menu")
        {
            playButton = GameObject.Find("PlayButton").GetComponent<Button>();
            optionsButton = GameObject.Find("OptionsButton").GetComponent<Button>();
            exitButton = GameObject.Find("ExitButton").GetComponent<Button>();


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
                    
    private void Awake()
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
