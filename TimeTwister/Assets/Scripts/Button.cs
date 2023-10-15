using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public GameObject unpressedButton;
    public GameObject pressedButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void pressButton(GameObject button)
    {
        GameObject pressed = Instantiate(pressedButton);
        Vector3 pos = button.transform.position;
        pressed.transform.position = pos;
        Destroy(button);
    }

    private void unpressButton(GameObject button)
    {
        GameObject unpressed = Instantiate(unpressedButton);
        Vector3 pos = button.transform.position;
        unpressed.transform.position = pos;
        Destroy(button);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("enter");
        if (this.gameObject.layer == 8) return; // button pressed layer
        if(collider.gameObject.layer == 6) // player layer
        {
            pressButton(this.gameObject);
        }

        if (collider.gameObject.layer == 7) // clone layer
        {
            pressButton(this.gameObject);
            Vector3 pos = collider.gameObject.transform.position;
            pos.y = gameObject.transform.GetChild(1).gameObject.GetComponent<Collider>().bounds.max.y + collider.bounds.size.y / 2;
            collider.gameObject.transform.position = pos;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("exit");
        if (this.gameObject.layer == 9) return; // button unpressed layer
        if (collider.gameObject.layer == 6 || collider.gameObject.layer == 7) // player layer = 6, clone layer = 7
        {
            unpressButton(this.gameObject);
        }
    }
}
