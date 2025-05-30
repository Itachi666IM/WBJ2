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
    public LayerMask chainLayer;
    public bool isCarryingItem = false;
    [HideInInspector] public string itemName;

    bool isNearFlame;
    bool isNearChain;
    float defaultGravityScale;
    public bool hasReachedChainTop;
    [HideInInspector] public bool canOpenDoor;
    [HideInInspector] public bool canTP;
    Transform tpPos;
    public LayerMask portalLayer;
    bool isNearPortal;

    AudioSource myAudio;
    public AudioSource sfx;
    public AudioClip jumpEffect;
    public AudioClip tpSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        defaultGravityScale = rb.gravityScale;
        myAudio = GetComponent<AudioSource>();
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
            myAudio.enabled = true;
        }
        else
        {
            anim.SetBool("isWalking", false);
            myAudio.enabled = false;
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
            sfx.PlayOneShot(jumpEffect);
        }

        if(hasReachedChainTop)
        {
            anim.SetBool("isClimbing", false);
            anim.SetTrigger("jump");
            rb.velocity = Vector2.up * jumpSpeed;
            hasReachedChainTop = false;
            
        }

        if(isNearChain)
        {
            
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            if (moveDirection.y > 0)
            {
                rb.velocity = Vector2.up;
            }
            else if (moveDirection.y < 0)
            {
                rb.velocity = Vector2.down;
            }

            if(Mathf.Abs(moveDirection.y) > 0)
            {
                anim.SetBool("isClimbing", true);
            }
            else
            {
                anim.SetBool("isClimbing", false);
            }
                
        }
        else
        {
            ResetGravity();
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
        isNearChain = Physics2D.OverlapCircle(transform.position, 0.2f, chainLayer);
        isNearPortal = Physics2D.OverlapCircle(transform.position, 0.2f, portalLayer);

        if (isNearFlame && isCarryingItem)
        {
            if(itemName == "Bucket")
            {
                Flame flame = FindObjectOfType<Flame>();
                flame.isExtinguished = true;
                isCarryingItem = false;
                itemName = "";
            }
        }

        if(isCarryingItem && itemName=="Key")
        {
            canOpenDoor = true;
        }

        if(canTP && Input.GetKeyDown(KeyCode.E) && isNearPortal)
        {
            sfx.PlayOneShot(tpSound);
            transform.position = tpPos.position;
        }
    }

    public void ResetGravity()
    {
        rb.gravityScale = defaultGravityScale;
    }

    public void TPToNewPos(Transform targetPos)
    {
        tpPos = targetPos;
    }

}
