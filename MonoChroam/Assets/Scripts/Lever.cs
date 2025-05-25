using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    public Sprite leverLeft;
    private Sprite leverRight;

    bool canPull;
    public LayerMask playerLayer;

    public GameObject fan;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        leverRight = mySpriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        canPull = Physics2D.OverlapCircle(transform.position,0.5f,playerLayer);

        if(canPull )
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(mySpriteRenderer.sprite == leverRight)
                {
                    mySpriteRenderer.sprite = leverLeft;
                    fan.SetActive(false);
                }
                else if(mySpriteRenderer.sprite == leverLeft)
                {
                    mySpriteRenderer.sprite = leverRight;
                    fan.SetActive(true);
                }
            }
        }
    }
}
