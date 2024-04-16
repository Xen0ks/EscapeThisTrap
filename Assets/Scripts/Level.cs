using UnityEngine;
using UnityEngine.Playables;

public class Level : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform nextLevelPoint;
    Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void EndLevel()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        respawnPoint.position = nextLevelPoint.position;
        StartCoroutine(player.Teleport(nextLevelPoint.position));
    }


}
