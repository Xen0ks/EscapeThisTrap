using UnityEngine;

public class Door : MonoBehaviour
{
    Transform tpTransform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tpTransform = collision.transform;
            Transition.instance.PerformTransition();
            collision.GetComponent<PlayerController>().enabled = false;
            if (TryGetComponent<Animator>(out Animator anim)) anim.SetTrigger("Open");
            Invoke("Tp", 0.6f);
        }
    }

    void Tp()
    {
        tpTransform.position = transform.GetChild(0).position;
        tpTransform.GetComponent<PlayerController>().enabled = true;
    }
}
