using UnityEngine;

public class Door : MonoBehaviour
{
    Transform tpTransform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tpTransform = collision.transform;
        if (collision.CompareTag("Player"))
        {
            Transition.instance.PerformTransition();
            collision.GetComponent<PlayerController>().enabled = false;
        }
        GetComponent<Animator>().SetTrigger("Open");
        Invoke("Tp", 0.6f);
    }

    void Tp()
    {
        tpTransform.position = transform.GetChild(0).position;
        tpTransform.GetComponent<PlayerController>().enabled = true;
    }
}
