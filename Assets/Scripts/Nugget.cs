using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nugget : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        player.GetNugget(1);
        // Play nugget sound
        Destroy(gameObject);
    }
}
