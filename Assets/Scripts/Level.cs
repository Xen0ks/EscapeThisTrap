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
        //GetComponent<PlayableDirector>().Play();
        NextLevel();
    }

    public void NextLevel()
    {
        Transition.instance.PerformTransition();
        Invoke("Tp", 0.5f);
    }

    void Tp()
    {
        respawnPoint.position = nextLevelPoint.position;
        player.transform.position = nextLevelPoint.position;
    }


}
