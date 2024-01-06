using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    public int life;
    public int score;
    public float speed;
    public float curHitDelay;
    public float maxHitDelay;

    public GameManager gameManager;
    public ObjectManager objectManager;

    public bool isRespawnTime;
    public bool isHit;

    Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();
        Reload();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, 0, v) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        /*if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            animator.SetInteger("Input", (int)h);
        }*/

        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Left");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Right");
        }

    }
    void Reload()
    {
        curHitDelay += Time.deltaTime;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
            }
        }
        else if(collision.gameObject.tag == "fish")
        {
            collision.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
            }
        }
    }

}
