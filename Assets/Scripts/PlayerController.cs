using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    public float dashPower = 16f;
    private bool isFacingRight = true;
    bool dash = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    Animator anim;

    int dashCount = 1;
    public int maxDash = 2;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal != 0)
        {
            if(IsGrounded()) anim.SetBool("Walk", true);
        }
        else
        {
            if(IsGrounded()) anim.SetBool("Walk", false);
        }

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            GetComponent<Animator>().SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetBool("Walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && CanDash())
        {
            Dash();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && dash)
        {
            EndDash();
        }


        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        if (IsGrounded())
        {
            dashCount = 0;
        }
    }

    private void FixedUpdate()
    {
        if(!dash) rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void Dash()
    {
        dashCount++;
        dash = true;
        GetComponent<Animator>().SetTrigger("Dash");
        anim.SetBool("Walk", false);
        anim.SetTrigger("Dash");

        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseScreenPosition = Input.mousePosition;

        Vector3 playerToMouseVector = (mouseScreenPosition - playerScreenPosition).normalized;

        rb.velocity = playerToMouseVector * dashPower;

        if (mouseScreenPosition.x < playerScreenPosition.x)
        {
            isFacingRight = false;
            Vector3 localScale = transform.localScale;
            localScale.x = -1f;
            transform.localScale = localScale;
        }
        else
        {
            isFacingRight = true;
            Vector3 localScale = transform.localScale;
            localScale.x = 1f;
            transform.localScale = localScale;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool CanDash()
    {
        return dashCount < maxDash;
    }

    public void EndDash()
    {
        dash = false;
    }

    void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
