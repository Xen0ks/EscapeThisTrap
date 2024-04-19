using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBloc : MonoBehaviour
{
    public void Break()
    {
        Destroy(gameObject);
    }
}
