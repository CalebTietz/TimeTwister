using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    void OnMouseOver()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(237f, 222f, 157f, 1f));
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }


    // called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // left click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object was the play button
                if (hit.collider.tag == "PlayButton")
                {
                    SceneManager.LoadScene("Level1");
                }

                // Check if the hit object was the quit button
                if(hit.collider.tag == "QuitButton")
                {
                    Application.Quit();
                    Application.ExternalEval("window.close();");
                }
            }
        }
    }
}
