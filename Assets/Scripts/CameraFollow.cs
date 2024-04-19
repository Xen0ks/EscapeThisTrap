using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public Transform target;
    [SerializeField] private Transform dashCinematicTarget;

    void Update()
    {
        PlayerController controller;
        Vector3 targetPosition = Vector3.zero;
        if (target.TryGetComponent<PlayerController>(out controller))
        {
            if (controller.GetComponent<Player>().isDead) smoothTime = 4f; else smoothTime = 0.5f;

            if (controller.IsGrounded() || controller.GetComponent<Player>().dash || controller.rb.velocity.y < 0 && target.position.y <= transform.position.y || target.position.y > transform.position.y +1)
            {
                targetPosition = target.position + offset;
            }
            else
            {
                targetPosition = new Vector3(target.position.x, transform.position.y, 0) + offset;
            }
            if (controller.rb.velocity.y < 0.001f && controller.rb.velocity.y > -0.001f)
            {
                if (target.localScale.x < 0f)
                {
                    offset.x = -4;
                }
                else
                {
                    offset.x = 4;
                }
            }

        }
        else
        {
            targetPosition = new Vector3(target.position.x, target.position.y, 0) + offset;
        }
        
        if(Vector2.Distance(target.position, transform.position) > 30)
        {
            targetPosition = new Vector3(target.position.x, target.position.y, 0) + offset;
            transform.position = targetPosition;
        }
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    public void DashCinematic()
    {
        StartCoroutine(SetNewTarget(dashCinematicTarget, 2f));
    }

    IEnumerator SetNewTarget(Transform target, float time)
    {
        PlayerController controller = this.target.GetComponent<PlayerController>();
        controller.enabled = false;
        this.target = target;
        yield return new WaitForSeconds(time);
        controller.enabled = true;
        this.target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
