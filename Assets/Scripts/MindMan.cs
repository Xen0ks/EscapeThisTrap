using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class MindMan : MonoBehaviour
{
    public int state = 0;
    bool active;
    Vector3 targetPoint;
    [SerializeField] int throwForce = 14;
    public bool chasing = false;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] Color transparent;

    int health = 4;
    public bool damaged = false;
    bool canTakeDamage;

    // References
    Animator anim;
    Player player;
    [SerializeField] private GameObject bombPrefab;

    [SerializeField] private PlayableDirector cinematic;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        active = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ChangeState(-1);
        StartCoroutine(StateChange());
        canTakeDamage = true;
    }

    private void Update()
    {
        targetPoint = new Vector2(player.transform.position.x, player.transform.position.y + Random.Range(2, 3));
        active = Vector2.Distance(player.transform.position, transform.position) < 35;
        if (!active)
        {
            health = 4;
            return;
        }
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
    }

    IEnumerator StateChange()
    {
        while (!damaged || chasing)
        {
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

            if(state == 2)
            {
                ChangeState(-1);
            }
        }
    }

    IEnumerator Suffer()
    {
        anim.SetBool("Move", false);
        anim.SetTrigger("Sleep");
        damaged = true;
        transform.AddComponent<Rigidbody2D>();
        yield return new WaitForSeconds(4f);
        StopDamage();
        ChangeState(1);
    }

    void StopDamage()
    {
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) Destroy(rb);
        damaged = false;
    }
    

    public void ChangeState(int newState)
    {
        if (damaged) return;
        if(newState < 0)
        {
            state = Random.Range(0, 2);
        }else
        {
            state = newState;
        }
        switch (state)
        {
            case 0:
                chasing = false;
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
                if(canTakeDamage) StartCoroutine(Suffer());
            }
            else
            {
                health = 4;
                player.Die();
                ChangeState(1);
            }
        }
    }

    public void TakeDamage()
    {
        GetComponent<SpriteRenderer>().color = transparent;
        if (damaged)
        {
            health--;
            canTakeDamage = false;

            if (health <= 0)
            {
                Transition.instance.PerformTransition();
                Invoke("Die", 0.7f);
            }
            else
            {
                StopDamage();
            }
            StartCoroutine(StateChange());
            Invoke("Vulnerable", 10f);
        }
    }

    void Die()
    {
        cinematic.Play();
        Destroy(gameObject);
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
            bomb.GetComponent<Bomb>().damageEnemy = false;
            Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
            bombRb.AddForce((player.transform.position - transform.position).normalized * throwForce, ForceMode2D.Impulse);
        }
    }

    void Vulnerable()
    {
        canTakeDamage = true;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 35);
    }
}
