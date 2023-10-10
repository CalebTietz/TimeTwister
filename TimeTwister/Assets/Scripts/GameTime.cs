using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameTime : MonoBehaviour
{

    private Dictionary<int, Dictionary<string, Vector3>> playerTracking = new Dictionary<int, Dictionary<string, Vector3>>();
    private GameObject player;

    private int gameTime;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            gameTime -= 200;
            if(gameTime < 0) { gameTime = 0; }
            player.GetComponent<Player>().setPositionAndVelocity(playerTracking[gameTime]["pos"], playerTracking[gameTime]["vel"]);
        }
    }
    
    // FixedUpdate is called 50 times every second
    void FixedUpdate()
    {
        gameTime++;

        Dictionary<string, Vector3> data = new Dictionary<string, Vector3>();
        data.Add("pos", player.transform.position);
        data.Add("vel", player.GetComponent<Rigidbody>().velocity);
        if(playerTracking.ContainsKey(gameTime))
        {
            playerTracking.Remove(gameTime);
        }
        playerTracking.Add(gameTime, data);
    }
}
