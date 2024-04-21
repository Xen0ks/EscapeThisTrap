using UnityEngine;

public class Arrow : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<Player>(out Player p))
        {
            p.Die();
        }
        Destroy(gameObject);
    }
}
