using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;


    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Vector3.zero;
        if (target.GetComponent<PlayerController>().IsGrounded())
        {
            targetPosition = target.position + offset;
        }
        else
        {
            targetPosition = new Vector3(target.position.x, transform.position.y, 0) + offset;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
