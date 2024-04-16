using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [HideInInspector] public bool isDead;
    // References
    PlayerController controller;
    Animator anim;
    Rigidbody2D rb;
    CameraFollow cam;

    // Dash
    public float dashPower = 14f;
    [HideInInspector] public bool dash = false;
    public bool hasDash = false;
    int dashCount = 0;
    public int maxDash = 2;

    // Bomb
    public Transform bombPrefab;
    public bool hasBomb = false;
    bool ableBomb = true;

    // Money
    int nuggetAmount;
    [SerializeField]
    Text nuggetText;
    [SerializeField]
    Sprite nuggetIcon;


    private void Start()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main.GetComponent<CameraFollow>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && CanDash())
        {
            Dash();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && dash)
        {
            EndDash();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && CanBomb())
        {
            ableBomb = false;
            Transform bomb = Instantiate(bombPrefab);
            bomb.position = transform.position;
            Vector3 bombScreenPosition = Camera.main.WorldToScreenPoint(bomb.position);
            Vector3 mouseScreenPosition = Input.mousePosition;

            Vector3 bombToMouseVector = (mouseScreenPosition - bombScreenPosition).normalized;

            bomb.GetComponent<Rigidbody2D>().velocity = bombToMouseVector * dashPower;

            Invoke("AbleBomb", 5f);
        }

        if (controller.IsGrounded() && !dash)
        {
            dashCount = 0;
        }
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }
        anim.SetBool("Dead", true);
        anim.SetTrigger("Die");
        isDead = true;
        controller.enabled = false;
        Transition.instance.PerformTransition();
        Invoke("Respawn", 0.67f);
    }

    public IEnumerator Teleport(Vector2 location)
    {
        Transition.instance.PerformTransition();
        controller.enabled = false;
        yield return new WaitForSeconds(0.67f);
        cam.transform.position = location;
        controller.enabled = true;
        transform.position = location;
        rb.velocity = Vector3.zero;
    }


    public void Respawn()
    {
        Vector2 respawnPoint = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().respawnPoint.position;
        transform.position = respawnPoint;
        controller.enabled = true;
        anim.SetBool("Dead", false);
        isDead = false;
        cam.transform.position = respawnPoint;
        rb.velocity = Vector3.zero;
    }

    void Dash()
    {

        dashCount++;
        dash = true;
        anim.SetBool("Walk", false);
        anim.SetBool("Dash", true);

        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseScreenPosition = Input.mousePosition;

        Vector3 playerToMouseVector = (mouseScreenPosition - playerScreenPosition).normalized;

        rb.velocity = playerToMouseVector * dashPower;

        if (mouseScreenPosition.x < playerScreenPosition.x)
        {
            controller.isFacingRight = false;
            Vector3 localScale = transform.localScale;
            localScale.x = -1f;
            transform.localScale = localScale;
        }
        else
        {
            controller.isFacingRight = true;
            Vector3 localScale = transform.localScale;
            localScale.x = 1f;
            transform.localScale = localScale;
        }
    }

    void AbleBomb()
    {
        ableBomb = true;
    }

    public bool CanDash()
    {
        return dashCount < maxDash && hasDash && !isDead;
    }

    public bool CanBomb()
    {
        return !GetComponent<Player>().isDead && hasBomb && !isDead && ableBomb;
    }

    public void EndDash()
    {
        dash = false;
        anim.SetBool("Dash", false);
    }

    public void UnlockDash()
    {
        hasDash = true;
    }

    public void UpgradeDash(int upgrade)
    {
        if (!hasDash)
        {
            Debug.Log("Doesn't have dash cant upgrade it");
            return;
        }
        maxDash = upgrade;
    }

    public void UnlockBombs()
    {
        hasBomb = true;
    }

    public void GetNugget(int amount)
    {
        nuggetAmount++;
        nuggetText.text = amount.ToString();
        NotificationSystem.instance.SmallNotif(nuggetIcon, amount.ToString());
    }
}
