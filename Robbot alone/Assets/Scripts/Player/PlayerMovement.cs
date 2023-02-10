using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>;
    private float speed = 0;
    private int speedMultiplayer = 1;

    // Start is called before the first frame update
    void Start()
    {
        keys.Add("up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Run", KeyCode.LeftControl);
        keys.Add("Pause", KeyCode.Escape);
        keys.Add("Jump", KeyCode.Space);
        keys.Add("Shoot", KeyCode.Insert);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keys["right"]))
        {

        }
    }
}
