using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip waterSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sfx.PlayOneShot(waterSound);
           Player player = collision.GetComponent<Player>();
            player.isCarryingItem = true;
            player.itemName = "Bucket";
            Destroy(gameObject);
        }
    }
}
