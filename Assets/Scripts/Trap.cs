using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    bool closing = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        closing = false;
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("close");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (closing && collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Player>().isDead) return;
            collision.GetComponent<Player>().Die();
        }
    }

    public void ToggleClose()
    {
        closing = !closing;
    }
}