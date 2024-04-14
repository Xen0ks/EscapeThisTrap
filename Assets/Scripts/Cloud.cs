using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    float speed;

    private void Start()
    {
        GetNewTarget();
    }
    void FixedUpdate()
    {
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
    }

    void GetNewTarget()
    {
        speed = Random.Range(-0.1f, 0.1f);
        Invoke("GetNewTarget", Random.Range(5, 15));
    }
}
