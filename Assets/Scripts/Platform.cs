using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField]
    int platformType; // 0 : Normal 1: Allez retour 2: Falling

    [SerializeField]
    Transform platformPrefab;

    // Allez Retour
    Transform currentPoint;
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField]
    Transform[] points;

    // Falling
    [HideInInspector] public Vector2 originalPos;

    private void Start()
    {
        originalPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        if(platformType == 1) currentPoint = points[0];
    }

    private void Update()
    {
        if (platformType != 1) return;
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f || currentPoint == null)
        {
            currentPoint = points[Random.Range(0, points.Length)];
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player") collision.transform.SetParent(transform);
        if(platformType == 2 && collision.transform.tag != "Platform")
        {
            Invoke("Fall", 0.8f);
            Invoke("Instantiate", 3f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player") collision.transform.parent = null;
        if (platformType == 2 && collision.transform.tag != "Platform")
        {
            Invoke("Fall", 0.8f);
            Invoke("Instantiate", 3f);
        }
    }

    void Fall()
    {
        rb.gravityScale = 2f;

        Destroy(gameObject, 2.3f);
    }

    void Instantiate()
    {
        Transform newPlatform = Instantiate(platformPrefab);
        newPlatform.GetComponent<Rigidbody2D>().gravityScale = 0;
        newPlatform.position = originalPos;
        Platform newPlat = newPlatform.GetComponent<Platform>();
        newPlat.platformType = platformType;
        newPlat.points = points;
        newPlat.originalPos = originalPos;
    }


}
