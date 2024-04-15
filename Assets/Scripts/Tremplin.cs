using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tremplin : MonoBehaviour
{
    public float force;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb;
        if(collision.TryGetComponent<Rigidbody2D>(out rb))
        {
            rb.AddForce(transform.up * force, ForceMode2D.Impulse);
            GetComponent<Animator>().SetTrigger("Bounce");
        }
    }
}
