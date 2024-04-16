using UnityEngine;
using UnityEngine.Tilemaps;

public class Fake : MonoBehaviour
{
    public Color fakeColor;

    private void Start()
    {
        Fake fake;
        if(transform.parent != null && transform.parent.TryGetComponent<Fake>(out fake))
        {
            fakeColor = fake.fakeColor;
            fake.GetComponent<Tilemap>().color = Color.white;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.GetComponent<Tilemap>().color = fakeColor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.GetComponent<Tilemap>().color = Color.white;
        }
    }
}
