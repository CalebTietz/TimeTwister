using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{

    private int gameTime;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    // FixedUpdate is called 50 times every second
    void FixedUpdate()
    {
        gameTime++;
        Debug.Log(gameTime);
    }
}
