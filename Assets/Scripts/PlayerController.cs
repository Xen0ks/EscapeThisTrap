using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    [HideInInspector] public bool isFacingRight = true;
    [SerializeField] private LayerMask groundLayer;

    // Réferences
    Animator anim;
    Player player;
    Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal != 0 && IsGrounded())
        {
            if(IsGrounded()) anim.SetBool("Walk", true);
        }
        if(!IsGrounded())
        {
            anim.SetBool("Walk", false);
        }

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            GetComponent<Animator>().SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetBool("Walk", false);
        }


        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if(!player.dash) rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.08f, groundLayer);
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

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        anim.SetBool("Walk", false);
    }
}
