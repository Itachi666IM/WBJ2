using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveDirection;
    Rigidbody2D rb;
    Animator anim;
    public float speed;
    public float jumpSpeed;

    bool isFacingRight = true;

    bool canJump;

    public Transform groundCheck;
    bool isGrounded;
    public LayerMask groundLayer;
    public LayerMask flameLayer;
    public bool isCarryingItem = false;
    [HideInInspector] public string itemName;

    bool isNearFlame;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            canJump = true;
        }
    }

    void Walk()
    {
        Vector2 playerVelocity = new Vector2(moveDirection.x,0) * speed * Time.fixedDeltaTime;

        if(Mathf.Abs(playerVelocity.x) > 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        rb.velocity = playerVelocity;
    }

    private void FixedUpdate()
    {
        Walk();
        if(canJump && isGrounded)
        {
            anim.SetTrigger("jump");
            rb.velocity = Vector2.up * jumpSpeed;
            canJump = false;
        }
    }

    void FlipSprite()
    {
        if(moveDirection.x < 0 && isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            isFacingRight = false;
        }
        else if(moveDirection.x > 0 && !isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
        }
    }

    private void Update()
    {
        FlipSprite();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f,groundLayer);
        isNearFlame = Physics2D.OverlapCircle(transform.position, 1f, flameLayer);

        if(isNearFlame && isCarryingItem)
        {
            if(itemName == "Bucket")
            {
                Flame flame = FindObjectOfType<Flame>();
                flame.isExtinguished = true;
                isCarryingItem = false;
                itemName = "";
            }
        }
    }
}
