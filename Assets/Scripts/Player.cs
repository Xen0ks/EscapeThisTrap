using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector] public bool isDead;
    // References
    PlayerController controller;
    Animator anim;
    Rigidbody2D rb;

    // Dash
    public float dashPower = 14f;
    [HideInInspector] public bool dash = false;
    public bool hasDash = false;
    int dashCount = 0;
    public int maxDash = 2;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

        if (controller.IsGrounded() && !dash)
        {
            dashCount = 0;
        }
    }

    public void Die()
    {
        isDead = true;
        Transition.instance.PerformTransition();
        Invoke("Respawn", 0.7f);
    }

    public void Respawn()
    {
        isDead = false;
        transform.position = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().respawnPoint.position; ;
    }

    void Dash()
    {

        dashCount++;
        dash = true;
        GetComponent<Animator>().SetTrigger("Dash");
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

    public bool CanDash()
    {
        return dashCount < maxDash && !GetComponent<Player>().isDead && hasDash;
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
}
