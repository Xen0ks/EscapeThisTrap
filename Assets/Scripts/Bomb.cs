using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool damage = false;
    public bool damageEnemy = true;

    List<Collider2D> colliders = new List<Collider2D>();



    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(!damage || colliders.Contains(collision)) return;
        colliders.Add(collision);
        PhysicsInteract physics;
        if (!damageEnemy && TryGetComponent<MindMan>(out MindMan m)) return;
        if (collision.transform.TryGetComponent<PhysicsInteract>(out physics))
        {
            physics.BombDamage(new Vector2(transform.position.x, transform.position.y-0.3f));
        }
    }

    public void AutoDestroy()
    {
        colliders.Clear();
        Destroy(gameObject);
    }

    public void Damage()
    {
        damage = true;
    }

}
