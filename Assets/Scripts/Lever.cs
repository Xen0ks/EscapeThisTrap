using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public UnityEvent onSwitch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("Switch");
            onSwitch.Invoke();
        }
    }
}
