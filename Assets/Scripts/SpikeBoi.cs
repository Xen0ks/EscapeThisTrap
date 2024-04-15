using UnityEngine;

public class SpikeBoi : MonoBehaviour
{
    Animator anim;
    Player player;
    Rigidbody2D rb;
    public float speed;

    [SerializeField]
    private float triggerDistance = 4.1f;

    [SerializeField]
    private Transform[] patrolPoint;
    public Transform currentPoint;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        currentPoint = patrolPoint[Random.Range(0, patrolPoint.Length)];
    }

    private void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
            rb.velocity = new Vector2(-speed, 0f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
            rb.velocity = new Vector2(speed, 0f);
        }
        
        if(Vector2.Distance(transform.position, player.transform.position) < triggerDistance)
        {
            anim.SetBool("Walk", true);
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                currentPoint = patrolPoint[Random.Range(0, patrolPoint.Length)];
            }
        }
        else
        {
            anim.SetBool("Walk", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Player>().Die();
        }
        if(collision.transform.tag == "Bomb")
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject, 0.5f);
    }

}
