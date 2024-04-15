using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool damage = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!damage) return;
        PhysicsInteract physics;
        if (collision.transform.TryGetComponent<PhysicsInteract>(out physics))
        {
            physics.BombDamage(transform.position);
        }
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
