using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 respawnPoint;

    private void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().respawnPoint.position;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    public void Die()
    {
        Transition.instance.PerformTransition();
        Invoke("Respawn", 0.7f);
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
    }
}
