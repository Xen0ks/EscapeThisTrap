using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField]
    private CameraFollow cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            cam.target = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            cam.target = player.transform;
        }
    }
}
