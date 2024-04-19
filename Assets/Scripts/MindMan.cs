using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MindMan : MonoBehaviour
{
    int state = 0;
    bool active;
    Vector3 targetPoint;
    [SerializeField] int throwForce = 14;
    bool chasing = false;
    private Vector3 velocity = Vector3.zero;

    int health = 4;
    bool damaged = false;

    // References
    Animator anim;
    Player player;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform[] patrolPoints;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        active = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ChangeState(-1);
        StartCoroutine(StateChange());
    }

    private void Update()
    {
        active = Vector2.Distance(player.transform.position, transform.position) < 20f;
        if (!active) return;
        transform.rotation= Quaternion.identity;

        if (chasing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPoint, ref velocity, 200 * Time.deltaTime);
        }

        if(targetPoint.x < transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }

        Debug.Log(active);
    }

    IEnumerator StateChange()
    {
        while (!damaged)
        {
            targetPoint = new Vector2(player.transform.position.x, player.transform.position.y + Random.Range(2, 3));
            yield return new WaitForSeconds(1.5f);

            if(state == 0)
            {
                ChangeState(1);
            }
            if (player.transform.position.x - transform.position.x < 2f && player.transform.position.x - transform.position.x > -2f)
            {
                ChangeState(2);
            }
            else
            {
                ChangeState(-1);
            }
        }
    }

    IEnumerator Suffer()
    {
        Debug.Log("Suffering");
        // Damages animations
        damaged = true;
        transform.AddComponent<Rigidbody2D>();
        yield return new WaitForSeconds(4f);
        StopDamage();
    }

    void StopDamage()
    {
        Destroy(transform.GetComponent<Rigidbody2D>());
        damaged = false;
    }
    

    public void ChangeState(int newState)
    {
        if(newState < 0)
        {
            state = Random.Range(0, 2);
        }else
        {
            state = newState;
        }
        Debug.Log(state);
        switch (state)
        {
            case 0:
                anim.SetBool("Move", false);
                break;
            case 1:
                ChasePlayer();
                break;
            case 2:
                StartCoroutine(ThrowBomb());
                break;
        }
    }

    void ChasePlayer()
    {
        anim.SetBool("Move", true);
        chasing = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player") && !damaged)
        {
            if (player.dash)
            {
                StartCoroutine(Suffer());
            }
            else
            {
                health = 4;
                player.Die();
            }
        }

        if(damaged && collision.transform.CompareTag("Bomb"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        health--;

        if(health <= 0)
        {
            Die();
        }
        else
        {
            StopDamage();
        }
    }

    void Die()
    {
        Debug.Log("Die");
    }

    IEnumerator ThrowBomb()
    {
        chasing = false;
        while(state == 2)
        {
            anim.SetBool("Move", false);
            yield return new WaitForSeconds(0.3f);
            GameObject bomb = Instantiate(bombPrefab);
            bomb.transform.position = transform.position;
            Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
            bombRb.AddForce((player.transform.position - transform.position).normalized * throwForce, ForceMode2D.Impulse);
        }
    }
}
