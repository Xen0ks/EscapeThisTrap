using UnityEngine;
using UnityEngine.Tilemaps;

public class Fake : MonoBehaviour
{
    public Color fakeColor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Tilemap>().color = fakeColor;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Tilemap>().color = Color.white;
        }
    }
}
