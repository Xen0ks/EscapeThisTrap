using UnityEngine;
using UnityEngine.Events;

public class PhysicsInteract : MonoBehaviour
{
    public UnityEvent onBombDamage;


    public void BombDamage(Vector2 bomb, float amount = 500f)
    {
        Vector3 bombScreenPosition = Camera.main.WorldToScreenPoint(bomb);

        Vector3 bombToMouseVector = (transform.position - bombScreenPosition).normalized;

        GetComponent<Rigidbody2D>().velocity = bombToMouseVector * amount;
        onBombDamage.Invoke();
    }
}
