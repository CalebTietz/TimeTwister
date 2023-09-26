using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{

    public GameObject player;
    public GameObject floor;

    public Rigidbody rb;

    private Boolean canJump = true;

    private float speed = 5f;
    public int xdir = 0; // dir = 0 means not moving, dir = 1 means moving right, dir = -1 means moving left
    public float maxJumpHeight = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        Vector3 pos = transform.position;
        Vector3 velocity = rb.velocity;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
        {
            xdir = 1;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
        {
            xdir = -1;
        }
        else
        {
            xdir = 0;
        }

        if(Input.GetKeyDown(KeyCode.W) && canJump) // jump
        {
            velocity.y = 5;
        }
        if(!Input.GetKey(KeyCode.W) && !canJump && velocity.y > 0)
        {
            Debug.Log("test");
            velocity.y = 0;
        }

        pos.x += speed * xdir * Time.deltaTime;
        transform.position = pos;
        rb.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == floor)
        {
            canJump = true;
        }
    }
}
