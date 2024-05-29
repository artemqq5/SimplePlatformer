using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animation;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpen && collision.gameObject.tag == "Player")
        {
            isOpen = true;
            animation.SetTrigger("ToOpen");
        }
    }
}
