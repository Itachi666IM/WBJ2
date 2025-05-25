using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position,player.transform.position)<0.5f)
        {
            player.ResetGravity();
            player.hasReachedChainTop = true;
        }
    }
}
