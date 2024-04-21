using System.Collections;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{

    [SerializeField] private GameObject arrow;

    private void Start()
    {
        Invoke("Throw", 1f);
    }

    void Throw()
    {
        GameObject instantiatedArrow = Instantiate(arrow);
        Rigidbody2D rb = instantiatedArrow.GetComponent<Rigidbody2D>();
        instantiatedArrow.transform.position = transform.position;
        rb.AddForce(-transform.right * 1000, ForceMode2D.Force);
        if (transform.rotation.z == 0)
        {
            instantiatedArrow.transform.localScale = new Vector2(-1, 1);
        }
        Invoke("Throw", 2f);
    }

}
