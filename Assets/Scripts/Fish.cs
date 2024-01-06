using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public string fishName;
    public float speed;
    public int fishScore;

    public GameObject player;
    public ObjectManager objectManager;
    public GameManager gameManager;

    Animator anim;

    void Update()
    {

    }

    public void Goal()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Line")
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += fishScore;

            other.gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }
}