using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string nextScene;
    IEnumerator OnTriggerStay (Collider collider)
    {
        if(collider.gameObject.layer == 6 && Mathf.Abs(gameObject.transform.position.x - collider.gameObject.transform.position.x) < 0.1f) // player
        {
            float animationTime = 0.5f;
            StartCoroutine(collider.gameObject.GetComponent<Player>().levelEndFade(collider.gameObject, 1f, animationTime));
            yield return new WaitForSeconds(animationTime + 0.25f);
            SceneManager.LoadScene(nextScene);
        }
    }
}
