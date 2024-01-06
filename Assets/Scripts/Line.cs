using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "fishE")
            gameManager.StageEnd();
    }
}

