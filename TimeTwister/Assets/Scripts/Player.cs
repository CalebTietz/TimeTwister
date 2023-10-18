using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject player;
    public GameObject Prefab_playerTracker;
    private GameObject playerTracker;

    private Rigidbody rb;

    private Boolean canJump = false;
    private int secondsToTrack = 6;
    private int gameTime;
    private int pastGameTime;

    private float speed = 5f;
    private float jumpStrength = 7f;
    public int xdir = 0; // dir = 0 means not moving, dir = 1 means moving right, dir = -1 means moving left

    private Vector3[] playerTracking;

    private Boolean teleporting = false; // Boolean flag to not track positon of player if the player is teleporting

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameTime = 0;
        pastGameTime = 0;
        playerTracking = new Vector3[secondsToTrack * 50];

        playerTracker = Instantiate(Prefab_playerTracker);
        Vector3 pos = player.transform.position;
        
        for(int i = 0; i < playerTracking.Length; i++)
        {
            playerTracking[i] = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;
        Vector3 velocity = rb.velocity;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            xdir = 1;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            xdir = -1;
        }
        else
        {
            xdir = 0;
        }

        if ( (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && canJump ) // jump
        {
            velocity.y = jumpStrength;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.UpArrow) && !canJump && velocity.y > 0) // stop jump if player lets go of jump button (just dampen it)
        {
            velocity.y *= 0.99f;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && !teleporting) // time travel and create clone
        {
            GameObject.Find("playerClone").GetComponent<playerClone>().createClone(transform.position, new Vector3(xdir * speed, rb.velocity.y, 0));
            StartCoroutine(teleportWithAnimation(player, playerTracking[pastGameTime], 1f, 0.5f));
        }
        else
        { // move player
            pos.x += speed * xdir * Time.deltaTime;
            transform.position = pos;
            rb.velocity = velocity;
        }

        CapsuleCollider myCapsuleCollider = GetComponent<CapsuleCollider>();
        Vector3 point0 = myCapsuleCollider.transform.TransformPoint(myCapsuleCollider.center + Vector3.up * (myCapsuleCollider.height * 0.5f - myCapsuleCollider.radius));
        Vector3 point1 = myCapsuleCollider.transform.TransformPoint(myCapsuleCollider.center + Vector3.down * (myCapsuleCollider.height * 0.5f - myCapsuleCollider.radius));
        float radius = myCapsuleCollider.radius;

        // Use Physicks.OverlapCapsule with the retrieved information
        Collider[] colliders = Physics.OverlapCapsule(point0, point1, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "jumpableSurface" && Mathf.Abs(rb.velocity.y) < 0.1f)
            {
                canJump = true;
                break;
            }
            else
            {
                canJump = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if(!teleporting)
        {
            playerTracking[gameTime] = transform.position;
            gameTime = (gameTime + 1) % (secondsToTrack * 50); // tick up gameTime by one and wrap around when (secondsToTrack) seconds of history is recorded
            pastGameTime = gameTime - (secondsToTrack - 1) * 50;
            if (pastGameTime < 0)
            {
                pastGameTime += secondsToTrack * 50;
            }

            playerTracker.transform.position = playerTracking[pastGameTime]; // move playerTracker
        }

    }

    private IEnumerator teleportWithAnimation(GameObject player, Vector3 destination, float rise, float animationTime)
    {
        teleporting = true;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.useGravity = false;
        Vector3 pos = player.transform.position;
        float zOff = 3f;
        pos.z -= zOff;
        player.transform.position = pos;

        Renderer renderer = player.GetComponent<Renderer>();
        Color color = renderer.material.color;

        float alphaStep = color.a / (animationTime / 0.01f);
        float riseStep = rise / (animationTime / 0.01f);

        // move player up and fade out
        while (color.a > 0)
        {
            color.a -= alphaStep;
            renderer.material.color = color;

            pos.y += riseStep;
            player.transform.position = pos;
            yield return new WaitForSeconds(0.01f);
        }

        // move player to new location
        pos.x = destination.x;
        pos.y = destination.y + rise;
        player.transform.position = pos;

        // move player down and fade in
        while (color.a < 1)
        {
            color.a += alphaStep;
            renderer.material.color = color;

            pos.y -= riseStep;
            player.transform.position = pos;
            yield return new WaitForSeconds(0.01f);
        }

        // move player back in z direction
        pos.z += zOff;
        player.transform.position = pos;
        teleporting = false;
        rb.useGravity = true;
    }

    public IEnumerator levelEndFade(GameObject player, float rise, float animationTime)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.useGravity = false;
        Vector3 pos = player.transform.position;
        player.GetComponent<Collider>().enabled = false;

        Renderer renderer = player.GetComponent<Renderer>();
        Color color = renderer.material.color;

        float alphaStep = color.a / (animationTime / 0.01f);
        float riseStep = rise / (animationTime / 0.01f);

        // move player up and fade out
        while (color.a > 0)
        {
            color.a -= alphaStep;
            renderer.material.color = color;

            pos.y += riseStep;
            player.transform.position = pos;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
