using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Goal : MonoBehaviour
{

    void OnTriggerStay (Collider collider)
    {
        if(collider.gameObject.layer == 6 && Mathf.Abs(gameObject.transform.position.x - collider.gameObject.transform.position.x) < 0.1f) // player
        {
            StartCoroutine(collider.gameObject.GetComponent<Player>().levelEndFade(collider.gameObject, 1f, 0.5f));
        }
    }
}
