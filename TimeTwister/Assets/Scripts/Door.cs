using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public void open()
    {
        gameObject.SetActive(false);
    }

    public void close()
    {
        gameObject.SetActive(true);
    }
}
