using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

    public bool isSharktouch;
    bool isPushing = false;

    Animator animator;

    public Rigidbody rb;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();
        Reload();
        Attack();
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


    }
    void Reload()
    {
        curHitDelay += Time.deltaTime;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Left");
            ApplyForceToShark(-10, 0, 10);  // Apply diagonal force to the left
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPushing = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Right");
            ApplyForceToShark(10, 0, 10);  // Apply diagonal force to the right
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isPushing = false;
        }
    }
    void ApplyForceToShark(float xForce, float yForce, float zForce)
    {
        if (isSharktouch)
        {
            isPushing = true;
            // Apply diagonal force
            rb.AddForce(xForce, yForce, zForce, ForceMode.Impulse);
        }
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
        else if(collision.gameObject.tag == "shark")
        {
            isSharktouch = true;
            rb = collision.gameObject.GetComponent<Rigidbody>();
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
