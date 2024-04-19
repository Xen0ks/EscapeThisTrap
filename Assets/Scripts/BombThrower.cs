using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
    [SerializeField]
    GameObject bomb;
    public void Throw()
    {
        GameObject throwedBomb = Instantiate(bomb);
        throwedBomb.transform.position = transform.position;
    }
}
