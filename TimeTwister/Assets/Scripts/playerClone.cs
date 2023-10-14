using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerClone : MonoBehaviour
{
    public GameObject GO_playerClone;
    private int maxClones = 2;
    private List<GameObject> clones = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }

    public void createClone(Vector3 pos, Vector3 vel)
    {
        GameObject clone = Instantiate(GO_playerClone);
        clones.Add(clone);

        if(clones.Count > maxClones)
        {
            Debug.Log("test");
            StartCoroutine(fadeAway(clones[0]));
            clones.RemoveAt(0);
        }
        clone.transform.position = pos;
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        rb.velocity = vel;


    }

    private IEnumerator fadeAway(GameObject clone)
    {
        Debug.Log("heyo");
        Renderer renderer = clone.GetComponent<Renderer>();
        Color color = renderer.material.color;
        float growth = 0.05f;
        clone.GetComponent<Rigidbody>().isKinematic = true;
        clone.GetComponent<CapsuleCollider>().enabled = false;
        while(color.a > 0)
        {
            color.a -= 0.1f;
            clone.transform.localScale = new Vector3(clone.transform.localScale.x + growth, clone.transform.localScale.y + growth, clone.transform.localScale.z + growth);
            renderer.material.color = color;
            yield return new WaitForSeconds(0.01f);
        }

        clone.GetComponent<CapsuleCollider>().enabled = true;
        Destroy(clone);
    }
}
