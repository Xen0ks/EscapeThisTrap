using UnityEngine;

public class ProgressZone : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawnPoint.position = transform.GetChild(0).position;
            Destroy(gameObject);
        }
    }
}
