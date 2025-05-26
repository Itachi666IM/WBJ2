using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Sprite buttonDefaultSprite;
    public Sprite buttonPushedSprite;
    public LayerMask playerLayer;
    public GameObject wall;
    private SpriteRenderer mySpriteRenderer;
    private bool canPush;
    bool once = true;

    public AudioSource sfx;
    public AudioClip pushSound;
    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        buttonDefaultSprite = mySpriteRenderer.sprite;
    }

    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position,1f,playerLayer))
        {
            canPush = true;
        }
        else
        {
            canPush= false;
        }

        if(canPush)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                sfx.PlayOneShot(pushSound);
                if (mySpriteRenderer.sprite == buttonDefaultSprite)
                {
                    mySpriteRenderer.sprite = buttonPushedSprite;
                    if(once)
                    {
                        once = false;
                        Destroy(wall);
                    }
                }
                else if (mySpriteRenderer.sprite == buttonPushedSprite)
                {
                    mySpriteRenderer.sprite = buttonDefaultSprite;
                }
            }
        }
    }
}
