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
        foreach (GameObject clone in clones)
        {
            CapsuleCollider myCapsuleCollider = clone.GetComponent<CapsuleCollider>();
            Vector3 point0 = myCapsuleCollider.transform.TransformPoint(myCapsuleCollider.center + Vector3.up * (myCapsuleCollider.height * 0.5f - myCapsuleCollider.radius));
            Vector3 point1 = myCapsuleCollider.transform.TransformPoint(myCapsuleCollider.center + Vector3.down * (myCapsuleCollider.height * 0.5f - myCapsuleCollider.radius));
            float radius = myCapsuleCollider.radius;

            // Use Physicks.OverlapCapsule with the retrieved information
            Collider[] colliders = Physics.OverlapCapsule(point0, point1, radius);
            foreach (Collider collider in colliders)
            {

                if (collider.gameObject.tag == "jumpableSurface")
                {

                }
                else
                {

                }
            }
        }
        
    }

    void FixedUpdate()
    {
        
    }

    // create clone with players current position and velocity
    public void createClone(Vector3 pos, Vector3 vel)
    {
        GameObject clone = Instantiate(GO_playerClone);
        clones.Add(clone);

        if(clones.Count > maxClones)
        {
            StartCoroutine(fadeAway(clones[0]));
            clones.RemoveAt(0);
        }
        clone.transform.position = pos;
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        rb.velocity = vel;


    }

    // make a clone disappear
    private IEnumerator fadeAway(GameObject clone)
    {
        Renderer renderer = clone.GetComponent<Renderer>();
        Color color = renderer.material.color;
        float growth = 0.05f;
        Vector3 pos = clone.transform.position;
        pos.z -= 3f;
        clone.transform.position = pos;
        clone.GetComponent<Rigidbody>().useGravity = false;
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
