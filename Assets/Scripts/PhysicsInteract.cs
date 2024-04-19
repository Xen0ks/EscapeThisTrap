using UnityEngine;
using UnityEngine.Events;

public class PhysicsInteract : MonoBehaviour
{
    public UnityEvent onBombDamage;

    [HideInInspector] public bool kb;


    public void BombDamage(Vector3 bomb, float amount = 13f) 
    {
        kb = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 moveDirection = transform.position - bomb;
        rb.AddForce(moveDirection.normalized * amount, ForceMode2D.Impulse);
        Invoke("StopKb", 0.2f);
        onBombDamage.Invoke();
    }

    void StopKb()
    {
        kb = false;
    }
}
