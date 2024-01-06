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
            if (gameObject.tag == "fishE")
                gameManager.StageEnd();

            if (gameObject.tag == "fish")
            {
                Player playerLogic = player.GetComponent<Player>();
                playerLogic.score += fishScore;

                gameObject.SetActive(false);
                transform.rotation = Quaternion.identity;
            }
            else if (gameObject.tag == "shark")
            {
                Player playerLogic = player.GetComponent<Player>();
                playerLogic.life--;
                gameManager.UpdateLifeIcon(playerLogic.life);
                if(playerLogic.life == 0)
                {
                    gameManager.GameOver();
                    Time.timeScale = 0f;
                }
            }
        }
    }
}