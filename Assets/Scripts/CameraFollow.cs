using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    void Update()
    {
        PlayerController controller;
        Vector3 targetPosition = Vector3.zero;
        if (target.TryGetComponent<PlayerController>(out controller))
        {
            if (controller.IsGrounded() || controller.GetComponent<Player>().dash)
            {
                targetPosition = target.position + offset;
            }
            else
            {
                targetPosition = new Vector3(target.position.x, transform.position.y, 0) + offset;
            }
            if (target.localScale.x < 0f)
            {
                offset.x = -4;
            }
            else
            {
                offset.x = 4;
            }
        }
        else
        {
            targetPosition = new Vector3(target.position.x, target.position.y, 0) + offset; new Vector3(0, 0, -10f);
        }
        

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
