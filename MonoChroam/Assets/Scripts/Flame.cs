using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flame : MonoBehaviour
{
    [HideInInspector]public bool isExtinguished = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Update()
    {
        if(isExtinguished)
        {
            Destroy(gameObject);
        }
    }
}
