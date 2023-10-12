using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameTime : MonoBehaviour
{

    private Dictionary<int, List<Dictionary<string, Vector3>>> playerTracking = new Dictionary<int, List<Dictionary<string, Vector3>>>();
    private GameObject player;
    public GameObject playerClone;
    private List<GameObject> currentClones;

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
            if(gameTime < 1) { gameTime = 1; }
            player.GetComponent<Player>().setPositionAndVelocity(playerTracking[gameTime][0]["pos"], playerTracking[gameTime][0]["vel"]);
        }

        
    }
    
    // FixedUpdate is called 50 times every second
    void FixedUpdate()
    {
        gameTime++;

        Dictionary<string, Vector3> data = new Dictionary<string, Vector3>();
        data.Add("pos", player.transform.position);
        data.Add("vel", player.GetComponent<Rigidbody>().velocity);
        List<Dictionary<string, Vector3>> clones = new List<Dictionary<string, Vector3>>();
        if (playerTracking.ContainsKey(gameTime))
        {
            clones = playerTracking[gameTime];
            clones.Add(data);
            playerTracking.Remove(gameTime);



        }
        playerTracking.Add(gameTime, clones);
        List<Dictionary<string, Vector3>> cloneList = playerTracking[gameTime];
        for (int i = 1; i < cloneList.Count; i++)
        {
            Dictionary<string, Vector3> cloneData = clones[i];
            GameObject clone = Instantiate(playerClone);
            clone.transform.position = cloneData["pos"];
            clone.GetComponent<Rigidbody>().velocity = cloneData["velocity"];
        }
    }
}
