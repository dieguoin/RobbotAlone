using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    private SceneMan sceneManager;
    [SerializeField] private GameManager gameManager;
    private Inventory inventory;
    private InventoryManager im;

    private void Awake()
    {
        sceneManager = GameObject.Find("SceneMan").GetComponent<SceneMan>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        im = GameObject.Find("InventorySlots").GetComponent<InventoryManager>();
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
    public void Base()
    {
        Time.timeScale = 1;
        sceneManager.changeScene("InventoryMenu");
    }
    public void Explore()
    {
        Debug.Log(inventory.bodyParts.Count);
        for(int i = 0; i < inventory.bodyParts.Count; i++)
        {
            inventory.bodyParts[i] = im.GetBodyPart(i);
        }
        sceneManager.changeScene("SampleScene");
    }
    public void Resume()
    {
        gameManager.ResumeGame();
    }
}
